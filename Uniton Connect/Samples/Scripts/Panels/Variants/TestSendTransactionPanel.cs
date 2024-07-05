using UnityEngine;
using TMPro;
using TonSdk.Connect;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TextMeshProUGUI _amountBar;
        [SerializeField] private TestWalletAddressBarView _targetWalletAddress;

        private const string CREATOR_WALLET_ADDRESS = 
            "EQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4UrpH_";

        private const float START_TON_AMOUNT = 0.01f;

        private void OnEnable()
        {
            UnitonConnectSDK.Instance.OnTransactionSendingFinished += TransactionSendingFinished;
        }

        private void OnDisable()
        {
            UnitonConnectSDK.Instance.OnTransactionSendingFinished -= TransactionSendingFinished;
        }

        public void Init()
        {
            SetAmountBar(START_TON_AMOUNT);
            SetTargetAddress(CREATOR_WALLET_ADDRESS);
        }

        private void SetAmountBar(float amount)
        {
            _amountBar.text = $"{amount} Ton";
        }

        private void SetTargetAddress(string address)
        {
            _targetWalletAddress.Set(address);
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

            Close();
        }
    }
}