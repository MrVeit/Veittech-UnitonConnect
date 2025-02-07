using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionCreditPhaseData
    {
        [JsonProperty("fees_collected")]
        public long FeesCollected { get; set; }

        [JsonProperty("credit")]
        public long Credit { get; set; }
    }
}