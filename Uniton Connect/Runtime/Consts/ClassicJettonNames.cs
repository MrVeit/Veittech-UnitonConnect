using System.Linq;
using System.Collections.Generic;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Common
{
    public static class ClassicJettonNames
    {
        public const string GRAM_NAME = "Gram";
        public const string NOT_NAME = "Notcoin";
        public const string DOGS_NAME = "Dogs";

        public const string USDT_NAME = "Tether USD";
        public const string AQUA_USD_NAME = "AquaUSD";
        public const string WRAPPED_USDT_NAME = "jUSDT";
        public const string WRAPPED_USDC_NAME = "jUSDC";

        public static bool IsStablecoin(string jettonName)
        {
            var stablecoins = new List<string>()
            {
                USDT_NAME, AQUA_USD_NAME,
                WRAPPED_USDT_NAME, WRAPPED_USDC_NAME
            };

            var foundedStable = stablecoins.FirstOrDefault(
                (targetStable) => targetStable == jettonName);

            UnitonConnectLogger.Log($"Parsed stablecoin in common storage: {foundedStable}");

            return !string.IsNullOrEmpty(foundedStable);
        }
    }
}