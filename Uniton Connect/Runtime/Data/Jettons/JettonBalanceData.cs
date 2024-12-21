using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonBalanceData
    {
        [JsonProperty("balance")]
        public string BalanceInNano { get; set; }

        [JsonProperty("wallet_address")]
        public WalletAccountData Account { get; set; }

        [JsonProperty("jetton")]
        public JettonData Configuration { get; set; }
    }
}