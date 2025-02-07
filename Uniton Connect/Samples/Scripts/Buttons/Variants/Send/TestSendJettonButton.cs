using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendJettonTransactionPanel _transactionPanel;

        private UserAssets.Jetton _jettonWallet => _transactionPanel.JettonWallet;

        private long _lastTransactionQuery;

        protected sealed override void OnEnable()
        {
            base.OnEnable();

            _jettonWallet.OnTransactionSended += JettonTransactionSended;
            _jettonWallet.OnLastTransactionsLoaded += LatestTransactionsLoaded;
        }

        protected sealed override void OnDisable()
        {
            base.OnDisable();

            _jettonWallet.OnTransactionSended -= JettonTransactionSended;
            _jettonWallet.OnLastTransactionsLoaded -= LatestTransactionsLoaded;
        }

        public sealed override void OnClick()
        {
            var amount = GetTransactionAmount(_transactionPanel.Amount);
            var gasFee = GetTransactionAmount(_transactionPanel.GasFee);

            var recipient = _transactionPanel.RecipientAddress;
            var masterAddress = _transactionPanel.JettonAddress;
            var transactionComment = _transactionPanel.Comment;
            var selectedJetton = _transactionPanel.SelectedJetton;

            Debug.Log($"Jetton transaction data for send, fee: {gasFee}, " +
                $"amount: {amount}, recipient address: " +
                $"{recipient}, selected jetton: {selectedJetton}," +
                $" comment: {transactionComment}");

            if (selectedJetton == JettonTypes.Custom)
            {
                _jettonWallet.SendTransaction(masterAddress,
                    recipient, amount, gasFee, transactionComment);

                return;
            }

            _jettonWallet.SendTransaction(selectedJetton,
                recipient, amount, gasFee, transactionComment);
        }

        private decimal GetTransactionAmount(string textBar)
        {
            return decimal.Parse(textBar);
        }

        private void JettonTransactionSended(string masterAddress,
            SuccessTransactionData transactionData)
        {
            _lastTransactionQuery = transactionData.OutMessages[0].DecodedBody.QueryId;

            Debug.Log($"Jetton transaction successfully " +
                $"founded, query id: {_lastTransactionQuery}");

            var recipientAddress = transactionData.OutMessages[0].DecodedBody.RecipientAddress;
            var recipientBouceable = WalletConnectUtils.GetHEXAddress(recipientAddress);

            _jettonWallet.GetLastTransactions(
                TransactionTypes.Received, 10, recipientBouceable);
        }

        private void LatestTransactionsLoaded(TransactionTypes type, 
            List<JettonTransactionData> transactions)
        {
            Debug.Log($"Loaded transactions with type: {type}");

            var lastSendedTransaction = transactions.FirstOrDefault(transaction => 
                transaction.QueryId == _lastTransactionQuery.ToString());

            if (lastSendedTransaction != null)
            {
                Debug.Log($"Target transaction loaded: " +
                    $"{JsonConvert.SerializeObject(lastSendedTransaction)}");

                Debug.Log("Sent transaction successfully " +
                    "confirmed, recipient received the sent jettons!");
            }
        }
    }
}