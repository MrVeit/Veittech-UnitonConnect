using UnityEngine;
using UnitonConnect.Core.Demo;

namespace UnitonConnect.Core.Data
{
    public sealed class TestDisconnectButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;

        public sealed override void OnClick()
        {
            Debug.Log("The disconnecting process of the previously connected wallet has been started");

            _interfaceAdapter.UnitonSDK.Disconnect();

            Debug.Log("Success disconnect");

        }
    }
}