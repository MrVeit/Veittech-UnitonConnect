using UnityEngine;
using UnitonConnect.Core;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.ThirdParty.TonAPI;

namespace UnitonConnect.DeFi
{
    public sealed class UserAssets
    {
        public NFT Nft { get; private set; }

        public UserAssets(MonoBehaviour mono,
            UnitonConnectSDK sdk)
        {
            Nft = new NFT(mono, sdk);
        }

        public sealed class NFT : IUnitonConnectNftTransactionCallbacks
        {
            private readonly MonoBehaviour _mono;
            private readonly UnitonConnectSDK _sdk;

            public NFT(MonoBehaviour mono,
                UnitonConnectSDK sdk)
            {
                _mono = mono;
                _sdk = sdk;
            }

            private string _walletAddress => _sdk.GetWalletAddress();


            public NftCollectionData LatestNftCollections { get; private set; }
            public NftCollectionData LatestTargetNftCollection { get; private set; }

            /// <summary>
            /// Callback to retrieve all nft collections on a user's account
            /// </summary>
            public event IUnitonConnectNftTransactionCallbacks.OnNftCollectionsClaim OnNftCollectionsClaimed;

            /// <summary>
            /// Callback to retrieve the current nft collection on the user's account
            /// </summary>
            public event IUnitonConnectNftTransactionCallbacks.OnTargetNftCollectionClaim OnTargetNftCollectionClaimed;

            /// <summary>
            /// Callback for notification that no NFTs are detected on the account
            /// </summary>
            public event IUnitonConnectNftTransactionCallbacks.OnNftCollectionsNotFound OnNftCollectionsNotFounded;

            /// <summary>
            /// Receive all available collections on your NFT account
            /// </summary>
            /// <param name="limit">Number of collections displayed</param>
            /// <param name="offset">Number of gaps between collections</param>
            public void Load(int limit, int offset = 0)
            {
                var encodedWalletAddress = ConvertAddressToEncodedURL(_walletAddress);
                var url = TonApiBridge.NFT.GetAllNftCollectionsUrl(encodedWalletAddress, limit, offset);

                _mono.StartCoroutine(TonApiBridge.NFT.GetNftCollections(url, (collections) =>
                {
                    if (collections.Items.Count == 0 || collections.Items == null)
                    {
                        OnNftCollectionsNotFounded?.Invoke();

                        UnitonConnectLogger.LogWarning("NFT collections are not detected on the current wallet");

                        return;
                    }

                    LatestNftCollections = collections;

                    OnNftCollectionsClaimed?.Invoke(collections);
                }));
            }

            /// <summary>
            /// Get a collection on an account, with a specific contract address
            /// </summary>
            /// <param name="collectionAddress">Address nft collection</param>
            public void LoadTargetCollection(string collectionAddress, int limit, int offset = 0)
            {
                var encodedWalletAddress = ConvertAddressToEncodedURL(_walletAddress);
                var encodedCollectionAddress = ConvertAddressToEncodedURL(collectionAddress);

                var url = TonApiBridge.NFT.GetTargetNftCollectionUrl(encodedWalletAddress,
                    encodedCollectionAddress, limit, offset);

                _mono.StartCoroutine(TonApiBridge.NFT.GetNftCollections(url, (collection) =>
                {
                    if (collection.Items.Count == 0 || collection.Items == null)
                    {
                        OnNftCollectionsNotFounded?.Invoke();

                        UnitonConnectLogger.LogWarning("NFT collections are not detected on the current wallet");

                        return;
                    }

                    LatestTargetNftCollection = collection;

                    OnTargetNftCollectionClaimed?.Invoke(LatestTargetNftCollection);
                }));
            }

            private string ConvertAddressToEncodedURL(string address)
            {
                return TonApiBridge.ConvertAddressToEncodeURL(address);
            }
        }
    }
}

