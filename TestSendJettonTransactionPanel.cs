using UnityEngine;
using TMPro;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonTransactionPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TMP_InputField _gasFeeBar;
        [SerializeField] private TMP_InputField _amountBar;
        [SerializeField, Space] private TestWalletAddressBarView _targetWalletAddress;
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private UnitonConnectSDK _unitonConnect => _interfaceAdapter.UnitonSDK;

        private string _transactionHash;

        private const string CREATOR_TON_WALLET_ADDRESS =
            "UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6";

        private const float START_AMOUNT = 0.005f;
        private const float START_FEE = 0.02f;

        public void Init()
        {
            _amountBar.text = START_AMOUNT.ToString();
            _gasFeeBar.text = START_FEE.ToString();

            _targetWalletAddress.Set(CREATOR_TON_WALLET_ADDRESS);
        }
    }
}