using UnityEngine;
using UnitonConnect.Core.Demo;

namespace UnitonConnect.Core.Data
{
    public sealed class TestOpenChooseWalletPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;

        public sealed override void OnClick()
        {
            _interfaceAdapter.UnitonSDK.Connect();
        }
    }
}