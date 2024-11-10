using AOT;
using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnitonConnect.Core
{
    public class TestUnitonConnectSDK : MonoBehaviour
    {
        private static TestUnitonConnectSDK _instance;

        [SerializeField, Space] private Button _connectButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField, Space] private TextMeshProUGUI _dataBar;

        public static Action<int> OnInitialized;
        public static Action<int> OnModalWindowOpened;

        public static Action<string> OnWalletConnected;
        public static Action<string> OnWalletDisconnected;

        [DllImport("__Internal")]
        private static extern void Init(string manifestUrl,
            string dAppUrl, Action<int> onInitialized);

        [DllImport("__Internal")]
        private static extern void OpenModal(Action<int> onModalWindowOpened);

        [DllImport("__Internal")]
        private static extern void Disconnect(Action<string> callback);

        [DllImport("__Internal")]
        private static extern void SubscribeToStatusChange(Action<string> callback);

        [DllImport("__Internal")]
        private static extern void UnSubscribeToStatusChange();

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

                return;
            }

            message = $"[UNITON CONNECT] Wallet successfully connected, data: {walletInfo}";

            Debug.Log(message);

            _instance._dataBar.text = message;
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

            if (!IsSupportedPlatform())
            {
                return;
            }

            UnSubscribeToStatusChange();
        }

        public void Init()
        {
            _instance = this;

            _connectButton.onClick.AddListener(ConnectWallet);
            _disconnectButton.onClick.AddListener(DisconnectWallet);

            if (!IsSupportedPlatform())
            {
                return;
            }

            Init("https://mrveit.github.io/Veittech-UnitonConnect/dAppData.json",
                "https://t.me/UnitonConnect_bot", OnInitialize);

            SubscribeToStatusChange(OnWalletConnect);
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