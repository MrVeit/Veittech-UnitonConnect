using System;
using UnityEngine;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField] private TMP_InputField _messageBar;
        [SerializeField, Space] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private UnitonConnectSDK _unitonConnect => _interfaceAdapter.UnitonSDK;

        private string _latestTransactionHash;

        private const string START_MESSAGE = "Made by Uniton Connect";
        private const string CREATOR_WALLET_ADDRESS = 
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";

        private const float START_TON_AMOUNT = 0.01f;

        private void OnEnable()
        {
            _unitonConnect.OnTonBalanceClaimed += TonBalanceClaimed;

            _unitonConnect.OnTonTransactionSended += TransactionSendingFinished;
            _unitonConnect.OnTonTransactionConfirmed += TonTransactionConfirmed;

            _unitonConnect.LoadBalance();
        }

        private void OnDisable()
        {
            _unitonConnect.OnTonBalanceClaimed -= TonBalanceClaimed;

            _unitonConnect.OnTonTransactionSended -= TransactionSendingFinished;
            _unitonConnect.OnTonTransactionConfirmed -= TonTransactionConfirmed;
        }

        public void Init()
        {
            SetAmountBar(START_TON_AMOUNT);
            SetTargetAddress(CREATOR_WALLET_ADDRESS);
            SetMessageBar(START_MESSAGE);

            SetTonBalance(_unitonConnect.TonBalance);
        }

        private void SetAmountBar(float amount)
        {
            _amountBar.text = $"{amount}";
        }

        private void SetMessageBar(string text)
        {
            _messageBar.text = text;
        }

        private void SetTargetAddress(string address)
        {
            _targetWalletAddress.Set(address);
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