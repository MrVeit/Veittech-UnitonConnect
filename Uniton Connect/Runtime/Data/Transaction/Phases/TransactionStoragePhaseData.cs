using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionStoragePhaseData
    {
        [JsonProperty("fees_collected")]
        public long FeesCollected { get; set; }

        [JsonProperty("fees_due")]
        public long FeesDue { get; set; }

        [JsonProperty("status_change")]
        public string StatusChange { get; set; }
    }
}