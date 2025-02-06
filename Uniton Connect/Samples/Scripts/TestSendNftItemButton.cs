using UnityEngine;
using UnitonConnect.Core.Data;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendNftItemButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendNftTransactionPanel _transactionPanel;

        private UserAssets.NFT _nftStorage => _transactionPanel.NftStorage;

        protected sealed override void OnEnable()
        {
            base.OnEnable();

            _nftStorage.OnTransactionSended += NftItemSended;
        }

        protected sealed override void OnDisable()
        {
            base.OnDisable();

            _nftStorage.OnTransactionSended -= NftItemSended;
        }

        public sealed override void OnClick()
        {
            var recipient = _transactionPanel.RecipientAddress;
            var nftItemAddress = _transactionPanel.NftItemAddress;
            var gasFee = _transactionPanel.GasFee;

            Debug.Log($"NFT transaction data for send, fee: {gasFee}, " +
                $"recipient: {recipient}, item address: {nftItemAddress}");

            _nftStorage.SendTransaction(nftItemAddress,
                recipient, decimal.Parse(gasFee));
        }

        private void NftItemSended(string nftItemAddress,
            SuccessTransactionData transactiionData)
        {
            var queryId = transactiionData.OutMessages[0].DecodedBody.QueryId;

            Debug.Log($"Nft item '{nftItemAddress}' " +
                $"successfully sended, query id: {queryId}");
        }
    }
}