using UnityEngine;
using TMPro;
using UnitonConnect.Core.Common;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TMP_InputField _gasFeeBar;
        [SerializeField] private TMP_InputField _amountBar;
        [SerializeField] private TMP_InputField _commentBar;
        [SerializeField, Space] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField] private TestWalletAddressBarView _masterAddressView;
        [SerializeField, Space] private TestSelectedJettonBar _selectedJettonView;
        [SerializeField, Space] private TestBalanceWidgetsPanel _widgetsPanel;

        private const string START_MESSAGE = "Made by Uniton Connect";
        private const string CREATOR_ADDRESS =
            "0:cfc0493e72741711f15a68d557cb0a5b280094330551ba99205a386bfdfe14ae";

        private const float START_AMOUNT = 0.005f;
        private const float START_FEE = 0.018f;

        public UserAssets.Jetton JettonWallet => _interfaceAdapter.JettonModule;

        public string Amount => _amountBar.text;
        public string GasFee => _gasFeeBar.text;

        public string RecipientAddress => _targetWalletAddress.FullAddress;
        public string JettonAddress => _masterAddressView.FullAddress;
        public string Comment => _commentBar.text;

        public JettonTypes SelectedJetton => _selectedJettonView.CurrentJetton;

        public void Init()
        {
            _commentBar.text = START_MESSAGE;
            _amountBar.text = START_AMOUNT.ToString();
            _gasFeeBar.text = START_FEE.ToString();

            _targetWalletAddress.Set(CREATOR_ADDRESS);
            _widgetsPanel.Init();
        }
    }
}