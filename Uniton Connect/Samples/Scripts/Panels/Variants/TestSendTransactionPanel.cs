using UnityEngine;
using TMPro;
using TonSdk.Connect;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private const string CREATOR_WALLET_ADDRESS = 
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";

        private const float START_TON_AMOUNT = 0.01f;

        private void OnEnable()
        {
            UnitonConnectSDK.Instance.UpdateTonBalance();

            UnitonConnectSDK.Instance.OnTransactionSendingFinished += TransactionSendingFinished;
            UnitonConnectSDK.Instance.OnTonBalanceClaimed += TonBalanceClaimed;
        }

        private void OnDisable()
        {
            UnitonConnectSDK.Instance.OnTransactionSendingFinished -= TransactionSendingFinished;
            UnitonConnectSDK.Instance.OnTonBalanceClaimed -= TonBalanceClaimed;
        }

        public void Init()
        {
            SetAmountBar(START_TON_AMOUNT);
            SetTargetAddress(CREATOR_WALLET_ADDRESS);
            SetTonBalance(UnitonConnectSDK.Instance.TonBalance);
        }

        private void SetAmountBar(float amount)
        {
            _amountBar.text = $"{amount}";
        }

        private void SetTargetAddress(string address)
        {
            _targetWalletAddress.Set(address);
        }

        private void SetTonBalance(decimal balance)
        {
            _balanceBar.text = $"Balance: {balance} TON";
        }

        private void TonBalanceClaimed(decimal tonBalance)
        {
            SetTonBalance(tonBalance);
        }

        private void TransactionSendingFinished(
            SendTransactionResult? result, bool isSuccess)
        {
            if (!isSuccess || !result.HasValue)
            {
                UnitonConnectLogger.LogError("Failed to send transaction for possible reasons:" +
                    " not enough funds or unsuccessful connection to the wallet");

                return;
            }
        }
    }
}