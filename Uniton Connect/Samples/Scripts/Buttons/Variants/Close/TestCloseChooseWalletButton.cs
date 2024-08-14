using UnityEngine;
using UnitonConnect.Core.Demo;

namespace UnitonConnect.Core.Data
{
    public sealed class TestCloseChooseWalletButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TestChooseWalletPanel _panel;

        public sealed override void OnClick()
        {
            _interfaceAdapter.UnitonSDK.PauseConnection();

            _panel.Close();
        }
    }
}
