using Newtonsoft.Json;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Data
{
    public sealed class SignMessageData
    {
        public SignMessageData(SignWalletDataTypes type)
        {
            Type = $"{type}";
        }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("bytes")]
        public string? Bytes { get; set; }

        [JsonProperty("schema")]
        public string? Schema { get; set; }

        [JsonProperty("cell")]
        public string? Cell { get; set; }

        [JsonProperty("network")]
        public string Network => TonNetworks.GetChain(NetworkTypes.MAINNET);

        [JsonProperty("from")]
        public string From { get; set; }
    }
}