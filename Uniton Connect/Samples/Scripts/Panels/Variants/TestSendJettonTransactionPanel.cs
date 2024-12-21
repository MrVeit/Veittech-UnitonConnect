using UnityEngine;
using TMPro;
using UnitonConnect.DeFi;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TMP_InputField _gasFeeBar;
        [SerializeField] private TMP_InputField _amountBar;
        [SerializeField, Space] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private UserAssets.Jetton _jettonModule => _interfaceAdapter.JettonModule;

        private const string CREATOR_TON_WALLET_ADDRESS =
            "0:cfc0493e72741711f15a68d557cb0a5b280094330551ba99205a386bfdfe14ae";

        private const float START_AMOUNT = 0.005f;
        private const float START_FEE = 0.018f;

        private void OnEnable()
        {
            _jettonModule.OnBalanceLoaded += JettonBalanceClaimed;

            _jettonModule.GetBalance(JettonTypes.USDT);
        }

        private void OnDisable()
        {
            _jettonModule.OnBalanceLoaded -= JettonBalanceClaimed;
        }

        public void Init()
        {
            _amountBar.text = START_AMOUNT.ToString();
            _gasFeeBar.text = START_FEE.ToString();

            _targetWalletAddress.Set(CREATOR_TON_WALLET_ADDRESS);
        }

        private void JettonBalanceClaimed(decimal balance, 
            string jettonName, string masterAddress)
        {
            Debug.Log($"Loaded {jettonName} balance, available: {balance}");

            _balanceBar.text = $"{balance} USDT";
        }
    }
}