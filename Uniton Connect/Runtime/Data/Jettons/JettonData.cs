using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonData
    {
        [JsonProperty("address")]
        public string MasterAddress { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string TokenSymbol { get; set; }

        [JsonProperty("decimals")]
        public decimal DecimalsInNano { get; set; }

        [JsonProperty("image")]
        public string IconUrl { get; set; }

        [JsonProperty("verification")]
        public string VerificationStatus { get; set; }

        [JsonProperty("score")]
        public int TrustScore { get; set; }
    }
}