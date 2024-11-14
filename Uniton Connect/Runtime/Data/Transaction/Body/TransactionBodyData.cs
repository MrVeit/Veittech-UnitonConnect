using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionBodyData
    {
        [JsonProperty("is_right")]
        public bool IsRight { get; set; }

        [JsonProperty("value")]
        public TransactionSendedValueData Value { get; set; }
    }
}