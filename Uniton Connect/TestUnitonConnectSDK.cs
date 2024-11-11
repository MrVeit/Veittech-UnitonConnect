using System;
using System.Runtime.InteropServices;
using AOT;
using Newtonsoft.Json;
using TMPro;
using TonSdk.Connect;
using TonSdk.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.View;
using UnityEngine;
using UnityEngine.UI;

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

        public static Action<int> OnInitialized;
        public static Action<int> OnModalWindowOpened;

        public static Action<string> OnWalletConnected;
        public static Action<string> OnWalletDisconnected;
        public static Action<int> OnWalletConnectionRestored;

        public static Action<string> OnTransactionSended;

        private readonly string MANIFEST_URL = "https://mrveit.github.io/Veittech-UnitonConnect/dAppData.json";
        private readonly string DAPP_LINK = "https://t.me/UnitonConnect_bot";

        private ConnectedWalletConfigData _walletConfig;

        [DllImport("__Internal")]
        private static extern void Init(string manifestUrl, 
            string dAppUrl, Action<int> onInitialized);

        [DllImport("__Internal")]
        private static extern void OpenModal(
            Action<int> onModalWindowOpened);

        [DllImport("__Internal")]
        private static extern void Disconnect(
            Action<string> onWalletDisconnected);

        [DllImport("__Internal")]
        private static extern void SendTransaction(string nanoTons,
            string recipientAddress, Action<string> onTransactionSended);

        [DllImport("__Internal")]
        private static extern void SubscribeToStatusChange(
            Action<string> onWalletConnected);

        [DllImport("__Internal")]
        private static extern void UnSubscribeToStatusChange();

        [DllImport("__Internal")]
        private static extern void SubscribeToRestoreConnection(string manifestUrl, 
            string dAppUrl, Action<int> onConnectionRestored);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnTransactionSend(string result)
        {
            OnTransactionSended?.Invoke(result);

            var message = "[UNITON CONNECT] Transaction sended";

            if (string.IsNullOrEmpty(result))
            {
                message = $"[UNITON CONNECT] Transaction sended with eror: {result}";

                Debug.LogError(message);

                _instance._dataBar.text = message;

                return;
            }

            message = $"[UNITON CONNECT] Transaction successfully sended, boc: {result}";

            Debug.Log(message);

            _instance._dataBar.text = result;
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnInitialize(int statusCode)
        {
            OnInitialized?.Invoke(statusCode);

            var message = "[UNITON CONNECT] Sdk successfully initialized";

            Debug.Log($"Claimed init status code: {statusCode}");

            if (statusCode == 1)
            {
                Debug.Log(message);

                _instance._dataBar.text = message;

                return;
            }

            message = "[UNITON CONNECT] Failed to initialize sdk";

            Debug.LogError(message);

            _instance._dataBar.text = message;
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnModalWindowOpen(int statusCode)
        {
            OnModalWindowOpened?.Invoke(statusCode);

            var message = string.Empty;

            Debug.Log($"Claimed status code for opened modal: {statusCode}");

            if (statusCode == 1)
            {
                message = "[UNITON CONNECT] Modal window for connect opened";

                Debug.Log(message);

                _instance._dataBar.text = message;

                return;
            }

            message = "[UNITON CONNECT] Failed to open modal window";

            Debug.LogError(message);

            _instance._dataBar.text = message;
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        private static void OnWalletConnectionRestor(int statusCode)
        {
            OnWalletConnectionRestored?.Invoke(statusCode);

            var message = string.Empty;

            Debug.Log($"Claimed status code for restore connection: {statusCode}");

            if (statusCode == 1)
            {
                message = "[UNITON CONNECT] Wallet connection restored";

                Debug.Log(message);

                _instance._dataBar.text = message;

                _instance._disconnectButton.interactable = true;
                _instance._connectButton.interactable = false;
                _instance._sendTonButton.interactable = true;

                return;
            }

            message = "[UNITON CONNECT] Wallet connection was not restored";

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
                message = "[UNITON CONNECT] Wallet is not connected";

                Debug.LogWarning(message);

                _instance._dataBar.text = message;

                _instance._disconnectButton.interactable = false;
                _instance._connectButton.interactable = true;
                _instance._sendTonButton.interactable = false;

                return;
            }

            message = $"[UNITON CONNECT] Wallet successfully connected";

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
                message = "[UNITON CONNECT] Wallet successfully disconnected";

                Debug.Log(message);
            }
            else if (statusCode == "500")
            {
                message = "[UNITON CONNECT] Failed to disconnect wallet";

                Debug.LogError(message);
            }

            _instance._dataBar.text = message;
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
        }

        private void SendTon()
        {
            var recepientAddress = "UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6";
            var bouceableAddress = WalletConnectUtils.GetBounceableAddress(recepientAddress);

            decimal amount = (decimal)0.001f;

            var tonInNanotons = UserAssetsUtils.ToNanoton(amount).ToString();

            Debug.Log($"Data for transaction: recepient: {bouceableAddress}, amount: {tonInNanotons}");

            SendTransaction(tonInNanotons, bouceableAddress, OnTransactionSend);

            Debug.Log($"Transaction sended");
        }

        public void Init()
        {
            _instance = this;

            _connectButton.onClick.AddListener(ConnectWallet);
            _disconnectButton.onClick.AddListener(DisconnectWallet);
            _sendTonButton.onClick.AddListener(SendTon);

            if (!IsSupportedPlatform())
            {
                return;
            }

            Init(MANIFEST_URL, DAPP_LINK, OnInitialize);

            SubscribeToStatusChange(OnWalletConnect);
            SubscribeToRestoreConnection(MANIFEST_URL, DAPP_LINK, OnWalletConnectionRestor);
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

        private bool IsSupportedPlatform()
        {
#if UNITY_EDITOR
            Debug.LogWarning("Unsupported platform, please build WebGL for check options");

            return false;
#endif

            return true;
        }
    }
}