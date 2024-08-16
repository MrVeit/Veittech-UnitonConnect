using System;
using System.Linq;
using System.Collections.Generic;
using UnitonConnect.Runtime.Data;
using UnitonConnect.Core.Utils.Debugging;

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
        /// Returns only those nft items that match the contract address
        /// </summary>
        /// <param name="collectionAddress">Collection address, 
        /// for example: EQAl_hUCAeEv-fKtGxYtITAS6PPxuMRaQwHj0QAHeWe6ZSD0</param>
        /// <returns></returns>
        public static List<NftItemData> GetCachedNftsByContractAddress(string collectionAddress)
        {
            var hexAddress = WalletConnectUtils.GetHEXAddress(collectionAddress);

            var filteredNfts = GetCachedNftsByFilter(collection => collection.Owner.Address == hexAddress);

            if (filteredNfts == null)
            {
                UnitonConnectLogger.LogError($"No nft collections matching " +
                    $"the ContractAddress condition were found: {collectionAddress}");

                return null;
            }

            return filteredNfts;
        }

        /// <summary>
        /// Returns only those elements of nft collections that have or have not passed the nft marketplace checklist
        /// </summary>
        /// <param name="isScam">Verification status of nft item on the marketplace</param>
        /// <returns></returns>
        public static List<NftItemData> GetCachedNftsByScamStatus(bool isScam)
        {
            var filteredNfts = GetCachedNftsByFilter(collection => collection.IsScam() == isScam);

            if (filteredNfts == null)
            {
                UnitonConnectLogger.LogError($"No nft collections are found that match the IsScam: {isScam} condition");

                return null;
            }

            return filteredNfts;
        }

        /// <summary>
        /// Returns nft elements of collections that match the specified filter
        /// </summary>
        /// <param name="sortFilter">Filter to return nft items that match the condition</param>
        /// <returns></returns>
        public static List<NftItemData> GetCachedNftsByFilter(
            Func<NftItemData, bool> sortFilter)
        {
            var collections = GetCachedNftsIfExist();

            if (collections == null)
            {
                UnitonConnectLogger.LogError("No cached nft collections detected, filtering canceled.");

                return null;
            }

            var filteredCollections = collections.Where(sortFilter).ToList();

            if (!filteredCollections.Any())
            {
                UnitonConnectLogger.LogError("No nft collection items were found that match the specified filter");

                return null;
            }

            return filteredCollections;
        }

        /// <summary>
        /// Returns cached nft collections if they have been previously downloaded
        /// </summary>
        /// <returns></returns>
        public static List<NftItemData> GetCachedNftsIfExist()
        {
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

            return nftModule.LatestNftCollections.Items;
        }
    }
}