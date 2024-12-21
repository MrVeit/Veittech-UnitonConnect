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
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField, Space] private TestWalletAddressBarView _walletAddressView;

        private UnitonConnectSDK _unitonConnect;

        private UserAssets.Jetton _jettonWallet;

        private decimal _amount;
        private decimal _gasFee;

        private string _recipientAddress;

        private string _lastTransactionQuery;

        protected sealed override void OnEnable()
        {
            base.OnEnable();

            Init();

            _jettonWallet.OnTransactionSended += JettonTransactionSended;
            _jettonWallet.OnTransactionSendFailed += JettonTransactionSendFailed;

            _jettonWallet.OnLastTransactionsLoaded += LatestTransactionLoaded;
        }

        protected sealed override void OnDisable()
        {
            base.OnDisable();

            _jettonWallet.OnTransactionSended -= JettonTransactionSended;
            _jettonWallet.OnTransactionSendFailed -= JettonTransactionSendFailed;

            _jettonWallet.OnLastTransactionsLoaded -= LatestTransactionLoaded;
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

            _recipientAddress = _walletAddressView.FullAddress;

            Debug.Log($"Transaction data for send, fee: {_gasFee}, " +
                $"amount: {_amount}, recipient address: {_recipientAddress}");

            _jettonWallet.SendTransaction(JettonTypes.USDT, 
                _recipientAddress, _amount, _gasFee);
        }

        private decimal GetTransactionAmount(string textBar)
        {
            return decimal.Parse(textBar);
        }

        private void JettonTransactionSended(string masterAddress,
            SuccessTransactionData transactionData)
        {
            _lastTransactionQuery = transactionData.InMessage.DecodedBody.QueryId;

            Debug.Log($"Jetton transaction successfully founded, query id: {_lastTransactionQuery}");

            var recipientAddress = transactionData.InMessage.Recipient.Address;
            var recipientBouceable = WalletConnectUtils.GetBounceableAddress(recipientAddress);

            _jettonWallet.GetLastTransactions(TransactionTypes.Received, 10, recipientBouceable);
        }

        private void JettonTransactionSendFailed(
            string masterAddress, string errorMessage)
        {
            Debug.LogError($"Failed to send jetton transaction {masterAddress}, reason: {errorMessage}");
        }

        private void LatestTransactionLoaded(TransactionTypes type, 
            List<JettonTransactionData> transactions)
        {
            Debug.Log($"Loaded transactions with type: {type}");

            var lastSendedTransaction = transactions.FirstOrDefault(
                transaction => transaction.QueryId == _lastTransactionQuery);

            if (lastSendedTransaction != null)
            {
                Debug.Log($"Target transaction loaded: {JsonConvert.SerializeObject(lastSendedTransaction)}");

                Debug.Log("Sent transaction successfully confirmed, recipient received the sent jettons!");
            }
        }
    }
}