using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonWalletData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("jetton")]
        public string MasterAddress { get; set; }

        [JsonProperty("last_transaction_lt")]
        public string LastTransactionLt { get; set; }

        [JsonProperty("code_hash")]
        public string CodeHash { get; set; }

        [JsonProperty("data_hash")]
        public string DataHash { get; set; }
    }
}