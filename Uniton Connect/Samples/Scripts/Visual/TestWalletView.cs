using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TonSdk.Connect;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Core.Data.Common;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestWalletView : MonoBehaviour
    {
        [SerializeField, Space] private TextMeshProUGUI _header;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _connectButton;

        private TestWalletInterfaceAdapter _interfaceAdapter;

        private TestSelectedWalletConnectionPanel _connectPanel;

        private WalletConfig _javascriptConfig;
        private WalletConfig _httpConfig;

        private WalletConfig _targetConfig;

        private string _name;

        public void SetView(TestWalletInterfaceAdapter interfaceAdapter, 
            string appName, Texture2D icon, TestSelectedWalletConnectionPanel connectPanel)
        {
            _interfaceAdapter = interfaceAdapter;

            _name = appName;

            _header.text = _name;
            _icon.sprite = WalletVisualUtils.GetSpriteFromTexture(icon);

            _connectPanel = connectPanel;

            _targetConfig = WalletConnectUtils.GetTargetWalletConfigWithoutSecondBridge(
                WalletConfigComponents.SSE, _name, _interfaceAdapter.LoadedWallets);

            StartConnect();
        }

        private void StartConnect()
        {
            var loadedWallets = _interfaceAdapter.LoadedWallets;

            _connectButton.onClick.AddListener(() =>
            {
                InitializeConnectPanel(loadedWallets);
            });
        }

        private void InitializeConnectPanel(List<WalletConfig> walletConfigs)
        {
            if (WalletConnectUtils.HasMultipleBridgeTypes(_name, walletConfigs))
            {
                _javascriptConfig = WalletConnectUtils.GetTargetWalletConfigWithoutSecondBridge(
                    WalletConfigComponents.JAVA_SCRIPT, _name, walletConfigs);
                _httpConfig = WalletConnectUtils.GetTargetWalletConfigWithoutSecondBridge(
                    WalletConfigComponents.SSE, _name, walletConfigs);
            }

            _targetConfig = GetBridgeConfiguration(_targetConfig);

            _connectPanel.SetOptions(_targetConfig);
            _connectPanel.Open();
        }

        private WalletConfig GetBridgeConfiguration(WalletConfig config)
        {
            WalletConfig targetConfig;

            if (IsUseWebWallets() && WalletConnectUtils.HasJSBridge(config))
            {
                targetConfig = _javascriptConfig;
            }
            
            if (!IsUseWebWallets() && WalletConnectUtils.HasHttpBridge(config))
            {
                targetConfig = _httpConfig;
            }

            return targetConfig;
        }

        private bool IsUseWebWallets()
        {
            return _interfaceAdapter.UnitonSDK.IsUseWebWallets;
        }
    }
}