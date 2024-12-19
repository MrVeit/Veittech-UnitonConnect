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
            var amount = ParseAmountFromBar(_amountBar.text);
            var recipient = _addressBar.FullAddress;
            var message = _messageBar.text;

            Debug.Log($"Start creating transaction with ton amount: " +
                $"{amount} by recipient: {recipient} with message {message}");

            _userInterfaceAdapter.UnitonSDK.SendTransaction(
                ClassicTokenTypes.Toncoin, recipient, amount, message);
        }

        private decimal ParseAmountFromBar(string amountFromBar)
        {
            var parsedAmount = amountFromBar.Replace(" ", "").Replace("TON", "");

            return decimal.Parse(parsedAmount);
        }
    }
}