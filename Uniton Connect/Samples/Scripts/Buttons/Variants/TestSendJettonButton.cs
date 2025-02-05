using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        [SerializeField, Space] private TMP_InputField _gasFeeBar;
        [SerializeField] private TMP_InputField _amountBar;
        [SerializeField] private TMP_InputField _commentBar;
        [SerializeField, Space] private TestWalletAddressBarView _walletAddressView;
        [SerializeField] private TestWalletAddressBarView _masterAddressView;
        [SerializeField, Space] private TestSelectedJettonBar _selectedJettonBar;

        private UnitonConnectSDK _unitonConnect;

        private UserAssets.Jetton _jettonWallet;

        private decimal _amount;
        private decimal _gasFee;

        private string _recipient;
        private string _masterAddress;
        private string _transactionComment;

        private long _lastTransactionQuery;

        protected sealed override void OnEnable()
        {
            base.OnEnable();

            Init();

            _jettonWallet.OnTransactionSended += JettonTransactionSended;
            _jettonWallet.OnTransactionSendFailed += JettonTransactionSendFailed;

            _jettonWallet.OnLastTransactionsLoaded += LatestTransactionsLoaded;
        }

        protected sealed override void OnDisable()
        {
            base.OnDisable();

            _jettonWallet.OnTransactionSended -= JettonTransactionSended;
            _jettonWallet.OnTransactionSendFailed -= JettonTransactionSendFailed;

            _jettonWallet.OnLastTransactionsLoaded -= LatestTransactionsLoaded;
        }

        private void Init()
        {
            _unitonConnect = UnitonConnectSDK.Instance;

            _jettonWallet = _unitonConnect.Assets.Jettons;
        }

        public sealed override void OnClick()
        {
            _amount = GetTransactionAmount(_amountBar.text);
            _gasFee = GetTransactionAmount(_gasFeeBar.text);

            _recipient = _walletAddressView.FullAddress;
            _masterAddress = _masterAddressView.FullAddress;
            _transactionComment = _commentBar.text;

            Debug.Log($"Transaction data for send, fee: {_gasFee}, " +
                $"amount: {_amount}, recipient address: {_recipient}");

            var selectedJetton = _selectedJettonBar.CurrentJetton;

            if (selectedJetton == JettonTypes.Custom)
            {
                _jettonWallet.SendTransaction(_masterAddress, _recipient,
                    _amount, _gasFee, _transactionComment);

                return;
            }

            _jettonWallet.SendTransaction(selectedJetton, _recipient,
                _amount, _gasFee, _transactionComment);
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

        private void JettonTransactionSendFailed(
            string masterAddress, string errorMessage)
        {
            Debug.LogError($"Failed to send jetton " +
                $"transaction {masterAddress}, reason: {errorMessage}");
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