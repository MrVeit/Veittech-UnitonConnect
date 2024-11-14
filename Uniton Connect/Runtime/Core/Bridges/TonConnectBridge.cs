using System;
using System.Runtime.InteropServices;
using AOT;
using Newtonsoft.Json;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core
{
    internal static class TonConnectBridge
    {
#region NATIVE_BRIDGE
        [DllImport("__Internal")]
        private static extern void Init(string manifestUrl, Action<int> onInitialized);

        [DllImport("__Internal")]
        private static extern void InitTonWeb();

        [DllImport("__Internal")]
        private static extern void OpenModal(Action<int> onModalWindowOpened);

        [DllImport("__Internal")]
        private static extern void CloseModal(Action<int> onModalWindowClosed);

        [DllImport("__Internal")]
        private static extern void Disconnect(Action<string> onWalletDisconnected);

        [DllImport("__Internal")]
        private static extern void SendTransaction(string nanoTons,
            string recipientAddress, Action<string> onTransactionSended);

        [DllImport("__Internal")]
        private static extern void SendTransactionWithMessage(string nanoTons,
            string recipientAddress, string message, Action<string> onTransactionSended);

        [DllImport("__Internal")]
        private static extern void SubscribeToStatusChange(Action<string> onWalletConnected);

        [DllImport("__Internal")]
        private static extern void UnSubscribeToStatusChange();

        [DllImport("__Internal")]
        private static extern void SubscribeToRestoreConnection(Action<int> onConnectionRestored);

        [DllImport("__Internal")]
        private static extern void SubscribeToTransactionEvents(
            Action<string> onTransactionSigned, Action<string> onTransactionSignFailed);

        [DllImport("__Internal")]
        private static extern void UnSubscribeToTransactionEvents();
        #endregion

#region NATIVE_CALLBACKS
        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnInitialize(int statusCode)
        {
            var isSuccess = IsSuccess(statusCode);

            OnInitialized?.Invoke(isSuccess);

            if (isSuccess)
            {
                return;
            }

            UnitonConnectLogger.LogError($"Failed to initialize Uniton Connect sdk, something wrong...");
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnModalWindowOpen(int statusCode)
        {
            var isSuccess = IsSuccess(statusCode);

            OnModalWindowOpened?.Invoke(isSuccess);

            if (isSuccess)
            {
                UnitonConnectLogger.Log("Connect window successfully opened");

                return;
            }

            UnitonConnectLogger.LogWarning("Failed to open connect window");
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnModalWindowClose(int statusCode)
        {
            var isSuccess = IsSuccess(statusCode);

            OnModalWindowClosed?.Invoke(isSuccess);

            if (isSuccess)
            {
                UnitonConnectLogger.Log("Connect window closed");

                return;
            }

            UnitonConnectLogger.LogWarning("Failed to close connect window");
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnWalletConnectionRestore(int statusCode)
        {
            var isSuccess = IsSuccess(statusCode);

            OnWalletConnectionRestored?.Invoke(isSuccess);

            if (isSuccess)
            {
                UnitonConnectLogger.Log($"Wallet connection restored with status: {isSuccess}");

                return;
            }

            UnitonConnectLogger.LogWarning($"Wallet connection was not restored");
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWalletConnect(string walletInfo)
        {
            if (string.IsNullOrEmpty(walletInfo) || 
                walletInfo == CONNECT_FAILED_ERROR)
            {
                var message = "Failed to connect current wallet, something wrong";

                UnitonConnectLogger.LogError(message);

                OnWalletConnectFailed?.Invoke(message);

                return;
            }

            var walletConfig = JsonConvert.DeserializeObject<
                NewWalletConfig>(walletInfo);

            if (walletConfig == null)
            {
                var message = "Failed to fetch wallet config";

                UnitonConnectLogger.LogError(message);

                OnWalletConnectFailed?.Invoke(message);

                return;
            }

            OnWalletSuccessfullyConnected?.Invoke(walletConfig);

            UnitonConnectLogger.Log($"Wallet successfully connected, " + 
                $"address: {WalletConnectUtils.GetNonBounceableAddress(walletConfig.Address)}");
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWalletDisconnect(string statusCode)
        {
            bool isSuccess = false;

            if (statusCode == SUCCESSFUL_DISCONNECT)
            {
                isSuccess = true;

                OnWalletDisconnected?.Invoke(isSuccess);

                UnitonConnectLogger.Log("Wallet successfully disconnected");

                return;
            }

            OnWalletDisconnected?.Invoke(isSuccess);

            UnitonConnectLogger.LogWarning("Failed to disconnect wallet, something wrong...");
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSend(string parsedHash)
        {
            if (string.IsNullOrEmpty(parsedHash))
            {
                var message = $"Failed to send transaction, something wrong...";

                UnitonConnectLogger.LogError(message);

                OnTonTransactionSendFailed?.Invoke(message);

                CloseModal(OnModalWindowClose);

                return;
            }

            if (parsedHash == EMPTY_BOC_ERROR)
            {
                var message = $"Transaction successfully sended, but no returned Boc";

                UnitonConnectLogger.LogError(message);

                OnTonTransactionSendFailed?.Invoke(message);

                CloseModal(OnModalWindowClose);

                return;
            }

            UnitonConnectLogger.Log($"Transaction successfully sended," +
                $"parsed hash: {parsedHash}");

            OnTonTransactionSended?.Invoke(parsedHash);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSuccessfullySign(string eventData)
        {
            UnitonConnectLogger.Log($"Transaction successfully signed with data: {eventData}");

            CloseModal(OnModalWindowClose);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSignFail(string eventData)
        {
            UnitonConnectLogger.LogWarning($"Transaction failed to sign with data: {eventData}");

            CloseModal(OnModalWindowClose);
        }
#endregion

        private static readonly string SUCCESSFUL_DISCONNECT = "200";

        private static readonly string CONNECT_FAILED_ERROR = "CONNECT_FAILED";
        private static readonly string EMPTY_BOC_ERROR = "EMPTY_BOC";

        private static Action<bool> OnInitialized;

        private static Action<bool> OnModalWindowOpened;
        private static Action<bool> OnModalWindowClosed;

        private static Action<bool> OnWalletDisconnected;
        private static Action<bool> OnWalletConnectionRestored;

        private static Action<NewWalletConfig> OnWalletSuccessfullyConnected;
        private static Action<string> OnWalletConnectFailed;

        private static Action<string> OnTonTransactionSended;
        private static Action<string> OnTonTransactionSendFailed;

        private static Action<SuccessTransactionData> OnTonTransctionConfirmed;

        internal static void UnSubscribe()
        {
            UnSubscribeToStatusChange();
            UnSubscribeToTransactionEvents();
        }

        internal static void Init(string manifestUrl, 
            Action<bool> sdkInitialized, Action<bool> connectionRestored)
        {
            OnInitialized = sdkInitialized;
            OnWalletConnectionRestored = connectionRestored;

            Init(manifestUrl, OnInitialize);
            InitTonWeb();

            SubscribeToRestoreConnection(OnWalletConnectionRestore);
        }

        internal static void Connect(
            Action<NewWalletConfig> walletConnected,
            Action<string> walletConnectFailed)
        {
            OnWalletSuccessfullyConnected = walletConnected;
            OnWalletConnectFailed = walletConnectFailed;

            SubscribeToStatusChange(OnWalletConnect);
            OpenModal(OnModalWindowOpen);
        }

        internal static void Disconnect(
            Action<bool> walletDisconnected)
        {
            OnWalletDisconnected = walletDisconnected;

            Disconnect(OnWalletDisconnect);
        }

        internal static void SendTon(string recipientAddress, 
            decimal tonAmount, string message, Action<string> transactionSended,
            Action<string> transactionSendFailed)
        {
            SendTonByParams(recipientAddress, tonAmount, 
                message, transactionSended, transactionSendFailed);
        }

        internal static void SendTon(string recipientAddress,
            decimal tonAmount, Action<string> transactionSended,
            Action<string> transactionSendFailed)
        {
            SendTonByParams(recipientAddress, tonAmount, 
                null, transactionSended, transactionSendFailed);
        }

        private static void SendTonByParams(string recipientAddress,
            decimal tonAmount, string message, Action<string> transactionSended, 
            Action<string> transactionSendFailed)
        {
            OnTonTransactionSended = transactionSended;
            OnTonTransactionSendFailed = transactionSendFailed;

            SubscribeToTransactionEvents(OnTransactionSuccessfullySign,
                OnTransactionSignFail);

            var targetAddress = WalletConnectUtils.GetHEXAddress(recipientAddress);
            var tonInNanotons = UserAssetsUtils.ToNanoton(tonAmount).ToString();

            if (string.IsNullOrEmpty(message))
            {
                SendTransaction(tonInNanotons, targetAddress, OnTransactionSend);

                return;
            }

            SendTransactionWithMessage(tonInNanotons, 
                targetAddress, message, OnTransactionSend);
        }

        private static bool IsSuccess(int statusCode)
        {
            if (statusCode == 1)
            {
                return true;
            }

            return false;
        }
    }
}