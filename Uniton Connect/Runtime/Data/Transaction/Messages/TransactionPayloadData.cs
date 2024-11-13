using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionPayloadData
    {
        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("message")]
        public TransactionMessageData Message { get; set; }
    }
}