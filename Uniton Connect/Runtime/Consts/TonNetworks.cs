using System.Collections.Generic;

namespace UnitonConnect.Core.Common
{
    public static class TonNetworks
    {
        private static readonly Dictionary<NetworkTypes,
            string> _chainConfig = new()
        {
            { NetworkTypes.MAINNET, "-239" },
            { NetworkTypes.TESTNET, "-3" }
        };

        public static string GetChain(NetworkTypes type)
        {
            return _chainConfig.GetValueOrDefault(type);
        }
    }
}
