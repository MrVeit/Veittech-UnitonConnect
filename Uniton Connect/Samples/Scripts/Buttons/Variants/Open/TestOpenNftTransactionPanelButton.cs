using UnityEngine;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestOpenNftTransactionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendNftTransactionPanel _sendNftPanel;

        private UserAssets.NFT _nftStorage;

        private string _nftItemName;
        private string _nftAddress;

        public void Init(UserAssets.NFT nftStorage,
            string nftName, string nftAddress)
        {
            _nftStorage = nftStorage;

            _nftItemName = nftName;
            _nftAddress = nftAddress;
        }

        public sealed override void OnClick()
        {
            _sendNftPanel.Init(_nftStorage, 
                _nftItemName, _nftAddress);
            _sendNftPanel.Open();
        }
    }
}