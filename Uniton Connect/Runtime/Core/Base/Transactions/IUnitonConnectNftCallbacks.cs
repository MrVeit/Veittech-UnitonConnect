using System.Collections.Generic;
using UnitonConnect.Core.Data;
using UnitonConnect.Runtime.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectNftCallbacks
    {
        delegate void OnNftCollectionsClaim(NftCollectionData collections);
        delegate void OnTargetNftCollectionClaim(NftCollectionData collection);

        delegate void OnNftCollectionsNotFound();

        delegate void OnLastNftTransactionsLoaded(TransactionTypes type,
            List<JettonTransactionData> transactions);

        delegate void OnNftTransactionSend(string nftItemAddress,
            SuccessTransactionData transactiionData);
        delegate void OnNftTransactionSendFail(
            string nftItemAddress, string errorMessage);

        event OnNftCollectionsClaim OnNftCollectionsClaimed;
        event OnTargetNftCollectionClaim OnTargetNftCollectionClaimed;

        event OnNftCollectionsNotFound OnNftCollectionsNotFounded;

        event OnNftTransactionSend OnTransactionSended;
        event OnNftTransactionSendFail OnTransactionSendFailed;
    }
}