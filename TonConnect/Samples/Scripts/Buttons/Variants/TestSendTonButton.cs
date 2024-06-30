using UnityEngine;
using TMPro;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTonButton : TestBaseButton
    {
        [SerializeField, Space] private TextMeshProUGUI _amountBar;
        [SerializeField] private TestWalletAddressBarView _addressBar;

        public sealed override async void OnClick()
        {
            await UnitonConnectSDK.Instance.SendTransaction(
                _addressBar.FullAddress, ParseAmountFromBar(_amountBar.text));
        }

        private double ParseAmountFromBar(string amountFromBar)
        {
            var parsedAmount = amountFromBar.Replace(" ", "").Replace("Ton", "");

            return double.Parse(parsedAmount);
        }
    }
}