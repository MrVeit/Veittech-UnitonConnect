using UnityEngine;
using TMPro;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTonButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _userInterfaceAdapter;
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField] private TMP_InputField _messageBar;
        [SerializeField, Space] private TestWalletAddressBarView _addressBar;

        public sealed override void OnClick()
        {
            _userInterfaceAdapter.UnitonSDK.SendTransaction(ClassicTokenTypes.Toncoin,
                _addressBar.FullAddress, ParseAmountFromBar(_amountBar.text), _messageBar.text);
        }

        private decimal ParseAmountFromBar(string amountFromBar)
        {
            var parsedAmount = amountFromBar.Replace(" ", "").Replace("TON", "");

            return decimal.Parse(parsedAmount);
        }
    }
}