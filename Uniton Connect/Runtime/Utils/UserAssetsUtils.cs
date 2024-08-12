using System.Linq;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;

namespace UnitonConnect.Core.Utils
{
    public static class UserAssetsUtils
    {
        private readonly static decimal NANOTON_VALUE = 1000000000m;

        /// <summary>
        /// Conversion of balance in TON to Nanotons (1 TON - 1.000.000.000 Nanoton)
        /// </summary>
        /// <param name="tonBalance"></param>
        /// <returns></returns>
        public static decimal ToNanoton(decimal tonBalance)
        {
            var nanoTons = new TonSdk.Core.Coins(tonBalance).ToNano();

            return decimal.Parse(nanoTons.ToString());
        }

        /// <summary>
        /// Converting Nanotons balance to TON (1 TON - 1.000.000.000 Nanoton)
        /// </summary>
        /// <param name="nanotonBalance"></param>
        /// <returns></returns>
        public static decimal FromNanoton(decimal nanotonBalance)
        {
            var tonBalance = nanotonBalance / NANOTON_VALUE;

            return tonBalance;
        }

        /// <summary>
        /// Retrieving a nft collection with a specific address among previously uploaded ones
        /// </summary>
        /// <param name="collectionAddress">Collection address, 
        /// for example: EQAl_hUCAeEv-fKtGxYtITAS6PPxuMRaQwHj0QAHeWe6ZSD0</param>
        /// <returns></returns>
        public static NftItemData GetNftCollectionByAddress(string collectionAddress)
        {
            var hexAddress = WalletConnectUtils.GetHEXAddress(collectionAddress);

            var unitonConnect = UnitonConnectSDK.Instance;
            var nftModule = unitonConnect.Assets.Nft;

            if (!unitonConnect.IsWalletConnected)
            {
                UnitonConnectLogger.LogError("Failed to detect downloaded nft collections," +
                    " connect your wallet and try again later.");

                return null;
            }

            if (nftModule.LatestNftCollections == null)
            {
                UnitonConnectLogger.LogError("No previously downloaded nft" +
                    " collections were detected on the wallet");

                return null;
            }

            var targetCollection = nftModule.LatestNftCollections.Items.FirstOrDefault(
                targetCollection => targetCollection.Address == hexAddress);

            if (targetCollection == null)
            {
                UnitonConnectLogger.LogError("The required collection is missing from this wallet");

                return null;
            }

            return targetCollection;
        }
    }
}