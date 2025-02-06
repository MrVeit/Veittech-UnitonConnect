using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class WalletConfig
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("chain")]
        public string Chain { get; set; }

        [JsonProperty("walletStateInit")]
        public string StateInit { get; set; }

        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }
    }
}