using UnityEngine;
using TMPro;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TMP_InputField _gasFeeBar;
        [SerializeField] private TMP_InputField _forwardFeeBar;
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField, Space] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private UnitonConnectSDK _unitonConnect => _interfaceAdapter.UnitonSDK;

        private string _transactionHash;

        private const string CREATOR_TON_WALLET_ADDRESS =
            "0:cfc0493e72741711f15a68d557cb0a5b280094330551ba99205a386bfdfe14ae";

        private const float START_AMOUNT = 0.005f;
        private const float START_FEE = 0.018f;
        private const decimal START_FORWARD_FEE = (decimal)0.000000001f;

        public void Init()
        {
            _amountBar.text = START_AMOUNT.ToString();
            _gasFeeBar.text = START_FEE.ToString();
            _forwardFeeBar.text = START_FORWARD_FEE.ToString();

            _targetWalletAddress.Set(CREATOR_TON_WALLET_ADDRESS);
        }
    }
}