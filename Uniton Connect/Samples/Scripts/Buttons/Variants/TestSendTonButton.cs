using UnityEngine;
using TMPro;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTonButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _userInterfaceAdapter;
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField] private TestWalletAddressBarView _addressBar;

        public sealed override async void OnClick()
        {
            var latestWallet = _userInterfaceAdapter.LatestAuthorizedWallet;

            await _userInterfaceAdapter.UnitonSDK.SendTon(latestWallet,
                _addressBar.FullAddress, ParseAmountFromBar(_amountBar.text));
        }

        private double ParseAmountFromBar(string amountFromBar)
        {
            var parsedAmount = amountFromBar.Replace(" ", "").Replace("Ton", "");

            return double.Parse(parsedAmount);
        }
    }
}