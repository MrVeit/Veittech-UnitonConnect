using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using AOT;
using TMPro;
using Newtonsoft.Json;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.ThirdParty.TonAPI;

namespace UnitonConnect.Core
{
    public class TestUnitonConnectSDK : MonoBehaviour
    {
        private static TestUnitonConnectSDK _instance;

        [SerializeField, Space] private Button _connectButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField, Space] private Button _sendTonButton;
        [SerializeField, Space] private TextMeshProUGUI _dataBar;
        [SerializeField] private TextMeshProUGUI _addressBar;

        private string _latestTransactionHash;

        public static Action<bool> OnInitialized;

        public static Action<bool> OnModalWindowOpened;
        public static Action<bool> OnModalWindowClosed;

        public static Action<string> OnWalletConnected;
        public static Action<string> OnWalletDisconnected;
        public static Action<bool> OnWalletConnectionRestored;

        public static Action<string> OnTonTransactionSended;

        private readonly string MANIFEST_URL = "https://mrveit.github.io/Veittech-UnitonConnect/dAppData.json";

        private ConnectedWalletConfigData _walletConfig;

        [DllImport("__Internal")]
        private static extern void Init(string manifestUrl, 
            Action<int> onInitialized);

        [DllImport("__Internal")]
        private static extern void InitTonWeb();

        [DllImport("__Internal")]
        private static extern void OpenModal(
            Action<int> onModalWindowOpened);

        [DllImport("__Internal")]
        private static extern void CloseModal(Action<int> onModalWindowClosed);

        [DllImport("__Internal")]
        private static extern void Disconnect(
            Action<string> onWalletDisconnected);

        [DllImport("__Internal")]
        private static extern void SendTransaction(string nanoTons,
            string recipientAddress, Action<string> onTransactionSended);

        [DllImport("__Internal")]
        private static extern void SendTransactionWithMessage(string nanoTons,
            string recipientAddress, string message, Action<string> onTransactionSended);

        [DllImport("__Internal")]
        private static extern void SubscribeToStatusChange(
            Action<string> onWalletConnected);

        [DllImport("__Internal")]
        private static extern void UnSubscribeToStatusChange();

        [DllImport("__Internal")]
        private static extern void SubscribeToRestoreConnection(
            Action<int> onConnectionRestored);

        [DllImport("__Internal")]
        private static extern void SubscribeToTransactionEvents(
            Action<string> onTransactionSigned, Action<string> onTransactionSignFailed);

        [DllImport("__Internal")]
        private static extern void UnSubscribeToTransactionEvents();

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnInitialize(int statusCode)
        {
            OnInitialized?.Invoke(_instance.IsSuccess(statusCode));

            var message = "Sdk successfully initialized";

            if (_instance.IsSuccess(statusCode))
            {
                Debug.Log(message);

                _instance._dataBar.text = message;

                return;
            }

            message = "Failed to initialize sdk";

            Debug.LogError(message);

            _instance._dataBar.text = message;
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnModalWindowOpen(int statusCode)
        {
            OnModalWindowOpened?.Invoke(_instance.IsSuccess(statusCode));

            var message = string.Empty;

            if (_instance.IsSuccess(statusCode))
            {
                message = "Modal window for connect opened";

                Debug.Log(message);

                _instance._dataBar.text = message;

                return;
            }

            message = "Failed to open modal window";

            Debug.LogWarning(message);

            _instance._dataBar.text = message;
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnModalWindowClose(int statusCode)
        {
            OnModalWindowClosed?.Invoke(_instance.IsSuccess(statusCode));

            var message = string.Empty;

            if (_instance.IsSuccess(statusCode))
            {
                message = "Modal window closed";

                Debug.Log(message);

                return;
            }

            Debug.LogWarning(message);
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnWalletConnectionRestor(int statusCode)
        {
            OnWalletConnectionRestored?.Invoke(_instance.IsSuccess(statusCode));

            var message = string.Empty;

            if (_instance.IsSuccess(statusCode))
            {
                message = "Wallet connection restored";

                Debug.Log(message);

                _instance._dataBar.text = message;

                _instance._disconnectButton.interactable = true;
                _instance._connectButton.interactable = false;
                _instance._sendTonButton.interactable = true;

                return;
            }

            message = "Wallet connection was not restored";

            Debug.LogWarning(message);

            _instance._dataBar.text = message;

            _instance._disconnectButton.interactable = false;
            _instance._connectButton.interactable = true;
            _instance._sendTonButton.interactable = false;
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWalletConnect(string walletInfo)
        {
            OnWalletConnected?.Invoke(walletInfo);

            var message = string.Empty;

            if (string.IsNullOrEmpty(walletInfo) || walletInfo == "0")
            {
                message = "Wallet is not connected";

                Debug.LogWarning(message);

                _instance._dataBar.text = message;

                _instance._disconnectButton.interactable = false;
                _instance._connectButton.interactable = true;
                _instance._sendTonButton.interactable = false;

                return;
            }

            message = $"Wallet successfully connected";

            Debug.Log(message);

            _instance._dataBar.text = message;

            _instance._disconnectButton.interactable = true;
            _instance._connectButton.interactable = false;
            _instance._sendTonButton.interactable = true;

            _instance._walletConfig = JsonConvert.DeserializeObject<ConnectedWalletConfigData>(walletInfo);

            if (_instance._walletConfig == null)
            {
                Debug.LogWarning("Wallet config is null after connect");
            }

            Debug.Log($"Parsed address: {_instance._walletConfig.Address}");

            var address = WalletConnectUtils.GetBounceableAddress(_instance._walletConfig.Address);

            Debug.Log($"Parsed connected wallet address: {address}");

            _instance._addressBar.text = address;

            var shortWalletAddress = WalletVisualUtils.ProcessWalletAddress(address, 6);

            Debug.Log($"Parsed short address: {shortWalletAddress}");

            _instance._addressBar.text = shortWalletAddress;
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWalletDisconnect(string statusCode)
        {
            OnWalletDisconnected?.Invoke(statusCode);

            var message = string.Empty;

            if (statusCode == "200")
            {
                message = "Wallet successfully disconnected";

                Debug.Log(message);
            }
            else if (statusCode == "500")
            {
                message = "Failed to disconnect wallet";

                Debug.LogError(message);
            }

            _instance._dataBar.text = message;
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSend(string parsedHash)
        {
            OnTonTransactionSended?.Invoke(parsedHash);

            var message = "Transaction sended...";

            if (string.IsNullOrEmpty(parsedHash))
            {
                message = $"Transaction sended with error: {parsedHash}";

                Debug.LogError(message);

                _instance._dataBar.text = message;

                CloseModal(OnModalWindowClose);

                return;
            }

            message = $"[UNITON CONNECT] Transaction successfully sended," +
                $" parsed transaction hash: {parsedHash}";

            Debug.Log(message);

            _instance._dataBar.text = $"Parsed trx hash: {parsedHash}";
            _instance._latestTransactionHash = parsedHash;

            _instance.Invoke(nameof(ValidateTransaction), 3f);
        }

        private void ValidateTransaction()
        {
            _instance.StartCoroutine(TonApiBridge.GetTransactionData(
                _latestTransactionHash, (transactionData) =>
            {
                var status = transactionData.IsSuccess;
                var newBalance = UserAssetsUtils.FromNanoton(
                    transactionData.EndBalance).ToString();
                var fee = UserAssetsUtils.FromNanoton(
                    transactionData.TotalFees).ToString();
                var sendedAmount = UserAssetsUtils.FromNanoton(
                    transactionData.OutMessages[0].Value).ToString();
                var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
                var message = transactionData.OutMessages[0].DecodedBody.MessageText;

                _instance._dataBar.text = $"Loaded transaction data: \n" +
                    $"STATUS: {transactionData.IsSuccess},\n" +
                    $"HASH: {_latestTransactionHash},\n" +
                    $"NEW BALANCE: {newBalance} TON,\n" +
                    $"FEE: {fee} TON,\n" +
                    $"SENDED AMOUNT: {sendedAmount} TON,\n" +
                    $"RECIPIENT ADDRESS: {WalletConnectUtils.GetNonBounceableAddress(recipientAddress)},\n" +
                    $"MESSAGE: {message}";
            },
            (errorMessage) =>
            {
                var message = $"Failed to fetch transaction data, reason: {errorMessage}";

                Debug.LogError(message);

                _instance._dataBar.text = message;

                if (errorMessage == "entity not found")
                {
                    Debug.LogWarning("Transaction data is processing");

                    ValidateTransaction();
                }
            }));
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSuccessfullySign(string eventData)
        {
            Debug.Log($"Transaction successfully signed with data: {eventData}");

            CloseModal(OnModalWindowClose);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSignFail(string eventData)
        {
            Debug.LogWarning($"Transaction failed to sign with data: {eventData}");

            CloseModal(OnModalWindowClose);
        }

        private void TestTransactionDataFetch(string hash)
        {
            _instance.StartCoroutine(TonApiBridge.GetTransactionData(
                hash, (transactionData) =>
            {
                var status = transactionData.IsSuccess;
                var newBalance = UserAssetsUtils.FromNanoton(
                    transactionData.EndBalance).ToString();
                var fee = UserAssetsUtils.FromNanoton(
                    transactionData.TotalFees).ToString();
                var sendedAmount = UserAssetsUtils.FromNanoton(
                    transactionData.OutMessages[0].Value).ToString();
                var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
                var message = transactionData.OutMessages[0].DecodedBody.MessageText;

                _instance._dataBar.text = $"Loaded transaction data: \n" +
                    $"STATUS: {transactionData.IsSuccess},\n" +
                    $"HASH: {hash},\n" +
                    $"NEW BALANCE: {newBalance} TON,\n" +
                    $"FEE: {fee} TON,\n" +
                    $"SENDED AMOUNT: {sendedAmount} TON,\n" +
                    $"RECIPIENT ADDRESS: {WalletConnectUtils.GetNonBounceableAddress(recipientAddress)},\n" +
                    $"MESSAGE: {message}";
            },
            (errorMessage) =>
            {
                var message = $"Failed to fetch transaction data, reason: {errorMessage}";

                Debug.LogError(message);

                _instance._dataBar.text = message;
            }));
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Start()
        {
            Init();
        }

        public void Dispose()
        {
            _connectButton.onClick.RemoveListener(ConnectWallet);
            _disconnectButton.onClick.RemoveListener(DisconnectWallet);
            _sendTonButton.onClick.RemoveListener(SendTon);

            if (!IsSupportedPlatform())
            {
                return;
            }

            UnSubscribeToStatusChange();
            UnSubscribeToTransactionEvents();
        }

        public void Init()
        {
            _instance = this;

            _connectButton.onClick.AddListener(ConnectWallet);
            _disconnectButton.onClick.AddListener(DisconnectWallet);
            _sendTonButton.onClick.AddListener(SendTon);

            //TestTransactionDataFetch("YZPUPwyUnjtHefF18U3VDVJsSVvwKwWFEykcfjkGEkc=");

            if (!IsSupportedPlatform())
            {
                _instance._dataBar.text = "Unsupported platform, " +
                    "please build WebGL for check options";

                return;
            }

            Init(MANIFEST_URL, OnInitialize);
            InitTonWeb();

            SubscribeToRestoreConnection(OnWalletConnectionRestor);
            SubscribeToStatusChange(OnWalletConnect);
            SubscribeToTransactionEvents(OnTransactionSuccessfullySign,
                OnTransactionSignFail);
        }

        public void ConnectWallet()
        {
            if (!IsSupportedPlatform())
            {
                return;
            }

            OpenModal(OnModalWindowOpen);
        }

        public void DisconnectWallet()
        {
            if (!IsSupportedPlatform())
            {
                return;
            }

            Disconnect(OnWalletDisconnect);
        }

        public void SendTon()
        {
            var recepientAddress = "UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6";
            var bouceableAddress = WalletConnectUtils.GetHEXAddress(recepientAddress);

            decimal amount = (decimal)0.001f;
            var tonInNanotons = UserAssetsUtils.ToNanoton(amount).ToString();

            SendTransactionWithMessage(tonInNanotons, bouceableAddress, 
                "GOOOOOOL", OnTransactionSend);
        }

        private bool IsSuccess(int statusCode)
        {
            if (statusCode == 1)
            {
                return true;
            }

            return false;
        }

        private bool IsSupportedPlatform()
        {
#if UNITY_EDITOR || !UNITY_WEBGL
            Debug.LogWarning("Unsupported platform, please build WebGL for check options");

            return false;
#endif

            return true;
        }
    }
}