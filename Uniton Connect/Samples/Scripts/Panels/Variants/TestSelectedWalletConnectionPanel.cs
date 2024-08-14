using UnityEngine;
using UnityEngine.UI;
using TonSdk.Connect;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSelectedWalletConnectionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _userInterfaceAdapter;
        [SerializeField, Space] private RawImage _qrCodeImage;
        [SerializeField, Space] private TestOpenDeepLinkWalletConnectionButton _deepLinkButton;

        private Texture2D _qrCodeForConnect;

        private WalletConfig _currentConfig;

        private string _connectionUrl;

        private UnitonConnectSDK _unitonConnect => _userInterfaceAdapter.UnitonSDK;

        private void OnEnable()
        {
            _unitonConnect.OnWalletConnectionFinished += WalletConnectionFinished;
        }

        private void OnDisable()
        {
            _unitonConnect.OnWalletConnectionFinished -= WalletConnectionFinished;

            _deepLinkButton.RemoveListeners();
        }

        private async void LoadConnectWalletContent()
        {
            _connectionUrl = await _unitonConnect.GenerateConnectURL(_currentConfig);

            UnitonConnectLogger.Log($"Generated connect link {_connectionUrl} " +
                $"for wallet: {_currentConfig.Name}");

            _qrCodeForConnect = WalletVisualUtils.GetQRCodeFromUrl(_connectionUrl);

            _deepLinkButton.SetListener(Connect);

            _qrCodeImage.texture = _qrCodeForConnect;
        }

        private void WalletConnectionFinished(Wallet wallet)
        {
            Close();
        }

        public async void SetOptions(WalletConfig connectionConfig)
        {
            _currentConfig = connectionConfig;

            if (_unitonConnect.IsWalletConnected)
            {
                Debug.LogWarning($"The wallet named {connectionConfig.Name} is already connected," +
                    $" the process of disconnecting it from the session begins");

                await _unitonConnect.DisconnectWallet();

                return;
            }

            LoadConnectWalletContent();
        }

        private async void Connect()
        {
            if (WalletConnectUtils.HasHttpBridge(_currentConfig))
            {
                _unitonConnect.ConnectHttpBridgeWalletViaDeepLink(
                    _currentConfig, _connectionUrl);
            }
            else if (WalletConnectUtils.HasJSBridge(_currentConfig) &&
                _unitonConnect.IsUseWebWallets)
            {
                await _unitonConnect.ConnectJavaScriptBridgeWalletViaDeeplink(_currentConfig);
            }
        }
    }
}