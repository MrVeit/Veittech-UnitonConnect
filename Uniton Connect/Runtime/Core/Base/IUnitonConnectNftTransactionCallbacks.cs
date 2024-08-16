using UnitonConnect.Runtime.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectNftTransactionCallbacks
    {
        delegate void OnNftCollectionsClaim(NftCollectionData collections);
        delegate void OnTargetNftCollectionClaim(NftCollectionData collection);

        delegate void OnNftCollectionsNotFound();

        event OnNftCollectionsClaim OnNftCollectionsClaimed;
        event OnTargetNftCollectionClaim OnTargetNftCollectionClaimed;

        event OnNftCollectionsNotFound OnNftCollectionsNotFounded;
    }
}