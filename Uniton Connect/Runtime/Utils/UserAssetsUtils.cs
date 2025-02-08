using System;
using System.Linq;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnitonConnect.Runtime.Data;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.ThirdParty;

namespace UnitonConnect.Core.Utils
{
    public static class UserAssetsUtils
    {
        private const decimal nanoDivider = 1_000_000_000m;

        /// <summary>
        /// Conversion of balance in TON to Nanotons (1 TON - 1.000.000.000 Nanoton)
        /// </summary>
        /// <param name="tonBalance"></param>
        /// <returns></returns>
        internal static decimal ToNanoton(this decimal tonBalance)
        {
            if (tonBalance <= 0)
            {
                throw new ArgumentException("Value must be a positive number!");
            }

            return (decimal)new BigInteger(tonBalance * nanoDivider);
        }

        /// <summary>
        /// Converting Nanotons balance to TON (1 TON - 1.000.000.000 Nanoton)
        /// </summary>
        /// <param name="nanotonBalance"></param>
        /// <returns></returns>
        internal static decimal FromNanoton(this decimal nanotonBalance)
        {
            if (nanotonBalance < 0)
            {
                throw new ArgumentException("Value must be a non-negative number");
            }

            return nanotonBalance / nanoDivider;
        }

        /// <summary>
        /// Conversion of balance in USDT to Nanotons (1 USDT - 1.000.000 Nanoton)
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        internal static long ToUSDtNanoton(double amount)
        {
            return (long)Math.Floor(amount * 1e6);
        }

        /// <summary>
        /// Converting Nanotons balance to USDT (1 USDT - 1.000.000 Nanoton)
        /// </summary>
        /// <param name="nanoAmount"></param>
        /// <returns></returns>
        internal static double FromUSDtNanoton(long nanoAmount)
        {
            return nanoAmount / 1e6;
        }

        /// <summary>
        /// Retrieving token wallet data from the master address, if it exists.
        /// </summary>
        /// <param name="masterAddress"></param>
        /// <param name="tonAddress"></param>
        /// <returns></returns>
        public static IEnumerator GetJettonWalletByAddress(string masterAddress,
            string tonAddress, Action<JettonWalletData> jettonWalletParsed)
        {
            yield return TonCenterApiBridge.Jetton.GetJettonWalletByOwner(
                tonAddress, masterAddress, (loadedWallet) =>
            {
                if (loadedWallet.Wallet == null)
                {
                    UnitonConnectLogger.LogWarning($"Jetton Wallet is not" +
                        $" deployed by master address: {masterAddress}");

                    jettonWalletParsed?.Invoke(null);

                    return;
                }

                jettonWalletParsed?.Invoke(loadedWallet.Wallet);
            });
        }

        /// <summary>
        /// Returns only those nft items that match the contract address
        /// </summary>
        /// <param name="collectionAddress">Collection address, 
        /// for example: EQAl_hUCAeEv-fKtGxYtITAS6PPxuMRaQwHj0QAHeWe6ZSD0</param>
        public static List<NftItemData> GetCachedNftsByContractAddress(string collectionAddress)
        {
            var hexAddress = WalletConnectUtils.GetHEXAddress(collectionAddress);

            var filteredNfts = GetCachedNftsByFilter(collection => collection.Owner.Address == hexAddress);

            if (filteredNfts == null)
            {
                UnitonConnectLogger.LogWarning($"No nft collections matching " +
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
                UnitonConnectLogger.LogWarning($"No nft collections are " +
                    $"found that match the IsScam: {isScam} condition");

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
                UnitonConnectLogger.LogWarning("No cached nft collections detected, filtering canceled.");

                return null;
            }

            var filteredCollections = collections.Where(sortFilter).ToList();

            if (!filteredCollections.Any())
            {
                UnitonConnectLogger.LogWarning("No nft collection items were found that match the specified filter");

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
                UnitonConnectLogger.LogWarning("Failed to detect downloaded nft collections," +
                    " connect your wallet and try again later.");

                return null;
            }

            if (nftModule.LatestNftCollections == null)
            {
                UnitonConnectLogger.LogWarning("No previously downloaded nft" +
                    " collections were detected on the wallet");

                return null;
            }

            return nftModule.LatestNftCollections.Items;
        }
    }
}