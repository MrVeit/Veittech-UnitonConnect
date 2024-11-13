using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionMessageData
    {
        [JsonProperty("sum_type")]
        public string SumType { get; set; }

        [JsonProperty("message_internal")]
        public TransactionInternalMessageData InternalMessage { get; set; }
    }
}