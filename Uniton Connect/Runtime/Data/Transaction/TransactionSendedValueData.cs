using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionSendedValueData
    {
        [JsonProperty("grams")]
        public string Grams { get; set; }

        [JsonProperty("other")]
        public TransactionOtherData Other { get; set; }

        [JsonProperty("sum_type")]
        public string SumType { get; set; }

        [JsonProperty("op_code")]
        public int OpCode { get; set; }

        [JsonProperty("value")]
        public TransactionSendedValueData Value { get; set; }

        [JsonProperty("text")]
        public string MessageText { get; set; }
    }
}