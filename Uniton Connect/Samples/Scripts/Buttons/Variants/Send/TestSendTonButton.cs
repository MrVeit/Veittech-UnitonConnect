using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTonButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendTonTransactionPanel _transactionPanel;

        public sealed override void OnClick()
        {
            var amount = ParseAmountFromBar(
                _transactionPanel.Amount);

            var recipient = _transactionPanel.Recipient;
            var message = _transactionPanel.Comment;

            Debug.Log($"Start creating transaction with ton amount: " +
                $"{amount} by recipient: {recipient} with message {message}");

            _transactionPanel.UnitonConnect.SendTransaction(recipient, amount, message);
        }

        private decimal ParseAmountFromBar(string amountFromBar)
        {
            var parsedAmount = amountFromBar.Replace(" ", "").Replace("TON", "");

            return decimal.Parse(parsedAmount);
        }
    }
}