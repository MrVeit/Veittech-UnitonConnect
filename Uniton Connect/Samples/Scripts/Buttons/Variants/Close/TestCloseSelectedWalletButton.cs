using UnityEngine;
using UnitonConnect.Core.Demo;

namespace UnitonConnect.Core.Data
{
    public sealed class TestCloseSelectedWalletButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TestSelectedWalletConnectionPanel _panel;

        public sealed override void OnClick()
        {
            _interfaceAdapter.UnitonSDK.PauseConnection();

            _panel.Close();
        }
    }
}