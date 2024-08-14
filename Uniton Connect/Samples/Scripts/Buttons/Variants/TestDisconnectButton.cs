using UnityEngine;
using UnitonConnect.Core.Demo;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Data
{
    public sealed class TestDisconnectButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;

        public sealed override async void OnClick()
        {
            UnitonConnectLogger.Log("The disconnecting process of the previously connected wallet has been started");

            await _interfaceAdapter.UnitonSDK.DisconnectWallet();

            UnitonConnectLogger.Log("Success disconnect");

        }
    }
}