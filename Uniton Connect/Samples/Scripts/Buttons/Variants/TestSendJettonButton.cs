using UnityEngine;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;
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

        private string _senderAddress;
        private string _recipientAddress;

        private void Start()
        {
            _unitonConnect = UnitonConnectSDK.Instance;

            _jettonWallet = _unitonConnect.Assets.Jettons;
        }

        protected sealed override void OnEnable()
        {
            base.OnEnable();

            _jettonWallet.OnTransactionSended += JettonTransactionSended;
            _jettonWallet.OnTransactionSendFailed += JettonTransactionSendFailed;
        }

        protected sealed override void OnDisable()
        {
            base.OnDisable();

            _jettonWallet.OnTransactionSended -= JettonTransactionSended;
            _jettonWallet.OnTransactionSendFailed -= JettonTransactionSendFailed;
        }

        public sealed override void OnClick()
        {
            _gasFee = GetTransactionAmount(_gasFeeBar.text);
            _amount = GetTransactionAmount(_amountBar.text);

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
            Debug.Log("Jetton transaction successfully founded and signed from blockchain!");

        }

        private void JettonTransactionSendFailed(
            string masterAddress, string errorMessage)
        {
            Debug.LogError($"Failed to send jetton transaction, reason: {errorMessage}");
        }
    }
}