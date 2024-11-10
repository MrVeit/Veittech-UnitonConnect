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

            var message = "[UNITON CONNECT] Modal window for connect opened";

            if (statusCode == 1)
            {
                Debug.Log(message);

                _instance._dataBar.text = message;

                return;
            }

            Debug.LogError("[UNITON CONNECT] Failed to open modal window");
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWalletConnect(string walletInfo)
        {
            OnWalletConnected?.Invoke(walletInfo);

            if (string.IsNullOrEmpty(walletInfo) || walletInfo == "0")
            {
                Debug.Log("[UNITON CONNECT] Wallet is not connected");

                return;
            }

            Debug.Log($"[UNITON CONNECT] Wallet successfully connected, data: {walletInfo}");
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
            _connectButton.onClick.AddListener(ConnectWallet);
            _disconnectButton.onClick.AddListener(DisconnectWallet);

            if (!IsSupportedPlatform())
            {
                return;
            }

            SubscribeToStatusChange(OnWalletConnect);

            Init("https://mrveit.github.io/Veittech-UnitonConnect/dAppData.json",
                "https://t.me/UnitonConnect_bot", OnInitialize);
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