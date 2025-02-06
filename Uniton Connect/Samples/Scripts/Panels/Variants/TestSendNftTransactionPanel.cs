using UnityEngine;
using TMPro;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendNftTransactionPanel: TestBasePanel
    {
        [SerializeField, Space] private TextMeshProUGUI _headerBar;
        [SerializeField] private TMP_InputField _gasFeeBar;
        [SerializeField, Space] private TestWalletAddressBarView _recipientAddressBar;

        private const float START_GAS_FEE = 0.015f;

        private const string CREATOR_ADDRESS =
            "0:cfc0493e72741711f15a68d557cb0a5b280094330551ba99205a386bfdfe14ae";

        public UserAssets.NFT NftStorage { get; private set; }

        public string NftItemAddress { get; private set; }

        public string GasFee => _gasFeeBar.text;
        public string RecipientAddress => _recipientAddressBar.FullAddress;

        public void Init(UserAssets.NFT nftStorage, 
            string itemName, string nftAddress)
        {
            _headerBar.text = $"ITEM: <color=yellow>{itemName}</color>";
            _gasFeeBar.text = START_GAS_FEE.ToString();

            _recipientAddressBar.Set(CREATOR_ADDRESS);

            NftStorage = nftStorage;
            NftItemAddress = nftAddress;
        }
    }
}