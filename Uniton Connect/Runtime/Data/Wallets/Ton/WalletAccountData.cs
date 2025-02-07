using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class WalletAccountData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("icon")]
        public string? IconUrl { get; set; }

        [JsonProperty("is_scam")]
        public bool IsScam { get; set; }

        [JsonProperty("is_wallet")]
        public bool IsWallet { get; set; }
    }
}