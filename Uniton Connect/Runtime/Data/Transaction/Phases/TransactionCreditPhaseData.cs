using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionCreditPhaseData
    {
        [JsonProperty("fees_collected")]
        public long FeesCollected { get; set; }

        [JsonProperty("credit")]
        public long Credit { get; set; }
    }
}