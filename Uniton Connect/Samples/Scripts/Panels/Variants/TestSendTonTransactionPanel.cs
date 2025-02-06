using System;
using UnityEngine;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTonTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField] private TMP_InputField _messageBar;
        [SerializeField, Space] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private string _latestTransactionHash;

        private const string START_MESSAGE = "Made by Uniton Connect";
        private const string CREATOR_WALLET_ADDRESS = 
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";

        private const float START_TON_AMOUNT = 0.01f;

        public UnitonConnectSDK UnitonConnect => _interfaceAdapter.UnitonSDK;

        public string Amount => _amountBar.text;
        public string Comment => _messageBar.text;
        public string Recipient => _targetWalletAddress.FullAddress;

        private void OnEnable()
        {
            UnitonConnect.OnTonBalanceClaimed += TonBalanceClaimed;

            UnitonConnect.OnTonTransactionSended += TransactionSendingFinished;
            UnitonConnect.OnTonTransactionConfirmed += TonTransactionConfirmed;
        }

        private void OnDisable()
        {
            UnitonConnect.OnTonBalanceClaimed -= TonBalanceClaimed;

            UnitonConnect.OnTonTransactionSended -= TransactionSendingFinished;
            UnitonConnect.OnTonTransactionConfirmed -= TonTransactionConfirmed;
        }

        public void Init()
        {
            _amountBar.text = START_TON_AMOUNT.ToString();
            _messageBar.text = START_MESSAGE;

            SetTonBalance(UnitonConnect.TonBalance);

            _targetWalletAddress.Set(CREATOR_WALLET_ADDRESS);

            UnitonConnect.LoadBalance();
        }

        private void SetTonBalance(decimal balance)
        {
            _balanceBar.text = $"Balance: {Math.Round(balance, 4)} TON";
        }

        private void TonBalanceClaimed(decimal tonBalance)
        {
            SetTonBalance(tonBalance);
        }

        private void TransactionSendingFinished(string transactionHash)
        {
            _latestTransactionHash = transactionHash;

            Debug.Log($"Claimed transaction hash: {transactionHash}");
        }

        private void TonTransactionConfirmed(SuccessTransactionData transactionData)
        {
            SetTonBalance(transactionData.EndBalance.FromNanoton());

            Debug.Log($"Ton transaction {_latestTransactionHash} " +
                $"confirmed with status: {transactionData.IsSuccess}");
        }
    }
}