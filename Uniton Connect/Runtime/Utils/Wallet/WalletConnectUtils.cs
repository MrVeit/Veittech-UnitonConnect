using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using TonSdk.Core;
using TonSdk.Connect;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Data.Common;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Utils
{
    public static class WalletConnectUtils
    {
        private static readonly UnitonConnectSDK _unitonConnect = UnitonConnectSDK.Instance;

        /// <summary>
        /// Checks if the recipient and sender addresses match
        /// </summary>
        /// <param name="recipientAddress">Recipient's address for sending tokens</param>
        /// <returns></returns>
        public static bool IsAddressesMatch(string recipientAddress)
        {
            var authorizedWalletAddress = _unitonConnect.Wallet.ToString();

            if (authorizedWalletAddress == recipientAddress)
            {
                UnitonConnectLogger.LogWarning("The recipient and sender address match, " +
                    "the transaction will be canceled when you try to send it");

                return true;
            }

            return false;
        }

        /// <summary>
        /// Convert wallet address to Non Bounceable format (base64), example:
        /// UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2V5I
        /// </summary>
        public static string ToNonBounceable(this UserWallet wallet)
        {
            return GetNonBounceableAddress(wallet.ToString());
        }

        /// <summary>
        /// Convert wallet address to Bounceable format (base64), example:
        /// EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON
        /// </summary>
        public static string ToBounceable(this UserWallet wallet)
        {
            return GetBounceableAddress(wallet.ToString());
        }

        /// <summary>
        /// Convert wallet address to HEX/RAW format, example:
        /// 0:c1da9d221d87032b762ad647658a2b5115a38af4b2329d4824fb25cb65799cd9
        /// </summary>
        public static string ToHex(this UserWallet wallet)
        {
            return GetHEXAddress(wallet.ToString());
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
            "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Returns the full wallet configuration if its name is found in the list
        /// </summary>
        /// <param name="walletsConfigs">Wallet configuration to check. Call `OnWalletConnectionFinished` to get all available configurations.</param>
        /// <param name="targetWalletName">Wallet name to be found</param>
        public static WalletConfig GetConfigOfSpecifiedWallet(
            List<WalletConfig> walletsConfigs, string targetWalletName)
        {
            var wallet = walletsConfigs.FirstOrDefault(wallet => 
                wallet.Name.Equals(targetWalletName, StringComparison.OrdinalIgnoreCase));

            if (wallet.Name == null)
            {
                UnitonConnectLogger.LogWarning($"Wallet with name '{targetWalletName}' not found is list.");

                wallet = walletsConfigs.FirstOrDefault(wallet =>
                    wallet.AppName.Equals(targetWalletName, StringComparison.OrdinalIgnoreCase));

                UnitonConnectLogger.LogWarning($"A search for another name of this wallet has been started");
            }

            return wallet;
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Checks the wallet configuration for the presence of an http bridge
        /// </summary>
        /// <param name="config">Wallet configuration to check. Call `OnWalletConnectionFinished` to get all available configurations.</param>
        public static bool HasHttpBridge(WalletConfig config)
        {
            return !string.IsNullOrEmpty(config.BridgeUrl);
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Checks the wallet configuration for the presence of an javascript bridge
        /// </summary>
        /// <param name="config">Wallet configuration to check. Call `OnWalletConnectionFinished` to get all available configurations.</param>
        public static bool HasJSBridge(WalletConfig config)
        {
            return !string.IsNullOrEmpty(config.JsBridgeKey);
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Checks the wallet configuration for more than one bridge
        /// </summary>
        /// <param name="targetWalletName">Wallet name to check.</param>
        /// <param name="wallets">Configurations of previously loaded wallets. Call `OnWalletConnectionFinished` to retrieve this data.</param>.
        public static bool HasMultipleBridgeTypes(string targetWalletName, 
            List<WalletConfig> wallets)
        {
            var targetWallets = wallets.Where(w => w.Name == targetWalletName).ToList();

            foreach (var wallet in targetWallets)
            {
                bool hasHttpBridge = HasHttpBridge(wallet);
                bool hasJavaScriptBridge = HasJSBridge(wallet);

                if (hasHttpBridge == hasJavaScriptBridge)
                {
                    continue;
                }

                return true;
            }

            return false;
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Get the configuration with the specified bridge, if the wallet has one
        /// </summary>
        /// <param name="bridgeType">Required bridge type to get the configuration</param>
        /// <param name="walletName">Name of the desired wallet</param>
        /// <param name="wallets">Configurations of previously loaded wallets. Call `OnWalletConnectionFinished` to retrieve this data.</param>.
        public static WalletConfig GetTargetWalletConfig(string bridgeType,
            string walletName, List<WalletConfig> wallets)
        {
            var wallet = wallets.FirstOrDefault(wallet => wallet.Name == walletName &&
                ((bridgeType == WalletConfigComponents.SSE && wallet.BridgeUrl != null) ||
                (bridgeType == WalletConfigComponents.JAVA_SCRIPT && wallet.JsBridgeKey != null)));

            return wallet;
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Get the configuration with the specified bridge if it does not have a second bridge
        /// </summary>
        /// <param name="bridgeType">Required bridge type to get the configuration</param>
        /// <param name="walletName">Name of the required wallet</param>
        /// <param name="wallets">Configurations of previously loaded wallets. Call `OnWalletConnectionFinished` to retrieve this data.</param>.
        public static WalletConfig GetTargetWalletConfigWithoutSecondBridge(string bridgeType,
            string walletName, List<WalletConfig> wallets)
        {
            WalletConfig config;

            var httpBridge = WalletConfigComponents.SSE;
            var jsBridge = WalletConfigComponents.JAVA_SCRIPT;

            var targetWallets = wallets.Where(wallet => wallet.Name == walletName).ToList();

            foreach (var wallet in targetWallets)
            {
                bool hasHttpBridge = HasHttpBridge(wallet);
                bool hasJavaScriptBridge = HasJSBridge(wallet);

                if (bridgeType == httpBridge &&
                    hasHttpBridge && !hasJavaScriptBridge)
                {
                    return wallet;
                }

                if (bridgeType == jsBridge &&
                    hasJavaScriptBridge && !hasHttpBridge)
                {
                    return wallet;
                }

                if (hasHttpBridge == hasJavaScriptBridge)
                {
                    continue;
                }
            }

            return config;
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Get a list of htttp bridge wallets for further generation of QR code to connect to them
        /// </summary>
        /// <param name="loadedWallets">Previously received wallet configuration via the `OnWalletConnectionFinished` event.</param>
        public static List<WalletConfig> GetHttpBridgeWallets(List<WalletConfig> loadedWallets)
        {
            List<WalletConfig> wallets = new();

            var httpBridgeWallets = loadedWallets.Where(wallet => wallet.BridgeUrl != null);

            foreach (var wallet in httpBridgeWallets)
            {
                wallets.Add(wallet);
            }

            return wallets;
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Get a list of javascript bridge wallets to connect to via DeepLink on WebGL Mobile/Desktop
        /// </summary>
        /// <param name="loadedWallets">Previously received wallet configuration via the `OnWalletConnectionFinished` event.</param>
        public static List<WalletConfig> GetJavaScriptBridgeWallets(List<WalletConfig> loadedWallets)
        {
            List<WalletConfig> wallets = new();

            if (_unitonConnect.IsUseWebWallets)
            {
                var jsBridgeWallets = loadedWallets.Where(
                    wallet => wallet.JsBridgeKey != null ||
                    InjectedProvider.IsWalletInjected(wallet.JsBridgeKey));

                foreach (var wallet in jsBridgeWallets)
                {
                    wallets.Add(wallet);
                }

                return wallets;
            }

            return null;
        }

        [Obsolete("Starting from 0.2.9.5 version, " +
                    "there is no need to configure connection to wallets, as there is a native connection window.")]
        /// <summary>
        /// Get the list of downloaded wallet configurations with filtering on those supported by the current platform
        /// <param name="wallets">Configurations of previously loaded wallets. Call `OnWalletConnectionFinished` to retrieve this data.</param>.
        public static List<WalletConfig> GetSupportedWalletsListForUse(List<WalletConfig> wallets)
        {
            List<WalletConfig> httpBridgeWallets = GetHttpBridgeWallets(wallets);
            List<WalletConfig> jsBridgeWallets = GetJavaScriptBridgeWallets(wallets);

            List<WalletConfig> uniqueWallets;
            List<WalletConfig> walletsConfigs;

            if (_unitonConnect.IsUseWebWallets)
            {
                foreach (var wallet in httpBridgeWallets)
                {
                    jsBridgeWallets.Add(wallet);
                }

                UnitonConnectLogger.Log($"Created wallet list: {JsonConvert.SerializeObject(jsBridgeWallets)}");
            }

            if (_unitonConnect.IsUseWebWallets)
            {
                uniqueWallets = jsBridgeWallets.GroupBy(wallet => wallet.Name).
                    Select(group => group.First()).ToList();
            }
            else
            {
                uniqueWallets = httpBridgeWallets.GroupBy(wallet => wallet.Name).
                    Select(group => group.First()).ToList();
            }

            walletsConfigs = uniqueWallets.Take(uniqueWallets.Capacity).ToList();

            return walletsConfigs;
        }

        [Obsolete("Use UnitonConnectSDK.Instance.Wallet.ToHex() for convert address for current format")]
        /// <summary>
        /// Convert wallet address to HEX/RAW format, example:
        /// 0:c1da9d221d87032b762ad647658a2b5115a38af4b2329d4824fb25cb65799cd9
        /// </summary>
        public static string GetHEXAddress(string address)
        {
            string rawAddress = ConvertAddressByType(address, AddressType.Raw);

            return rawAddress;
        }

        [Obsolete("Use UnitonConnectSDK.Instance.Wallet.ToBounceable() for convert address for current format")]
        /// <summary>
        /// Convert wallet address to Bounceable format (base64), example:
        /// EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON
        /// </summary>
        public static string GetBounceableAddress(string address)
        {
            string bounceableAddress = ConvertAddressByType(address, 
                AddressType.Base64, new AddressStringifyOptions(true, false, false));

            return bounceableAddress;
        }

        [Obsolete("Use UnitonConnectSDK.Instance.Wallet.ToNonBounceable() for convert address for current format")]
        /// <summary>
        /// Convert wallet address to Non Bounceable format (base64), example:
        /// UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2V5I
        /// </summary>
        public static string GetNonBounceableAddress(string address)
        {
            string NonBounceableAddress = ConvertAddressByType(address, 
                AddressType.Base64, new AddressStringifyOptions(false, false, false));

            return NonBounceableAddress;
        }

        private static string ConvertAddressByType(string address, AddressType type,
            AddressStringifyOptions options = null)
        {
            if (!_unitonConnect.IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning($"Wallet is not connected, address is empty");

                return string.Empty;
            }

            return GetAddress(address).ToString(type, options);
        }

        private static Address GetAddress(string walletAddress)
        {
            return new Address(walletAddress);
        }
    }
}