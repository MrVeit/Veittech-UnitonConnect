using UnityEngine;
using TMPro;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTonButton : TestBaseButton
    {
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField] private TestWalletAddressBarView _addressBar;

        public sealed override async void OnClick()
        {
            var latestWallet = TestWalletInterfaceAdapter.Instance.LatestAuthorizedWallet;

            await UnitonConnectSDK.Instance.SendTon(latestWallet,
                _addressBar.FullAddress, ParseAmountFromBar(_amountBar.text));
        }

        private double ParseAmountFromBar(string amountFromBar)
        {
            var parsedAmount = amountFromBar.Replace(" ", "").Replace("Ton", "");

            return double.Parse(parsedAmount);
        }
    }
}