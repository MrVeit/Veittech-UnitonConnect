using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnitonConnect.Core
{
    public class TestUnitonConnectSDK : MonoBehaviour
    {
        [SerializeField, Space] private Button _connectButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField, Space] private TextMeshProUGUI _dataBar;

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

            SubscribeToStatusChange((walletData) =>
            {
                OnWalletStatusChanged(walletData);
            });

            Init("https://mrveit.github.io/Veittech-UnitonConnect/dAppData.json",
                "https://t.me/UnitonConnect_bot", OnInitialized);
        }

        public void ConnectWallet()
        {
            if (!IsSupportedPlatform())
            {
                return;
            }

            OpenModal(OnModalWindowOpened);
        }

        public void DisconnectWallet()
        {
            if (!IsSupportedPlatform())
            {
                return;
            }

            Disconnect(OnWalletDisconnected);
        }

        private bool IsSupportedPlatform()
        {
#if UNITY_EDITOR
            Debug.LogWarning("Unsupported platform, please build WebGL for check options");

            return false;
#endif

            return true;
        }

        private void OnInitialized(int statusCode)
        {
            var message = "[UNITON CONNECT] Sdk successfully initialized";

            if (statusCode == 1)
            {
                Debug.Log(message);

                _dataBar.text = message;

                return;
            }

            message = "[UNITON CONNECT] Failed to initialize sdk";

            Debug.LogError(message);

            _dataBar.text = message;
        }

        private void OnModalWindowOpened(int statusCode)
        {
            var message = "[UNITON CONNECT] Modal window for connect opened";

            if (statusCode == 1)
            {
                Debug.Log(message);

                _dataBar.text = message;

                return;
            }

            Debug.LogError("[UNITON CONNECT] Failed to open modal window");
        }

        private void OnWalletDisconnected(string status)
        {
            if (status == "200")
            {
                Debug.Log("[UNITON CONNECT] Wallet successfully disconnected");
            }
            else if (status == "500")
            {
                Debug.LogError("[UNITON CONNECT] Failed to disconnect wallet");
            }
        }

        private void OnWalletStatusChanged(string walletInfo)
        {
            if (string.IsNullOrEmpty(walletInfo) || walletInfo == "0")
            {
                Debug.Log("[UNITON CONNECT] Wallet is not connected");

                return;
            }

            Debug.Log($"[UNITON CONNECT] Wallet successfully connected, data: {walletInfo}");
        }
    }
}