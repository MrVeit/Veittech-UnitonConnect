using UnityEngine;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSignMessageButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;

        public sealed override void OnClick()
        {
            var sdk = _interfaceAdapter.UnitonSDK;

            var message = new SignMessageData(
                SignWalletDataTypes.text)
            {
                Text = "Message from Uniton Connect",
                From = sdk.Wallet.ToHex()
            };

            sdk.SignData(message);
        }
    }
}