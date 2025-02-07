using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionSendedValueData
    {
        [JsonProperty("grams")]
        public string Grams { get; set; }

        [JsonProperty("other")]
        public object? Other { get; set; }

        [JsonProperty("sum_type")]
        public string SumType { get; set; }

        [JsonProperty("op_code")]
        public int OpCode { get; set; }

        [JsonProperty("value")]
        public TransactionSendedValueData Value { get; set; }

        [JsonProperty("text")]
        public string MessageText { get; set; }

        // DETAILED VALUE FOR NFT TRANSFER

        [JsonProperty("query_id")]
        public long QueryId { get; set; }

        [JsonProperty("new_owner")]
        public string NewOwner { get; set; }

        [JsonProperty("response_destination")]
        public string Sender { get; set; }

        [JsonProperty("custom_payload")]
        public object CustomPayload { get; set; }

        [JsonProperty("forward_amount")]
        public string ForwardAmount { get; set; }

        [JsonProperty("forward_payload")]
        public TransactionBodyData ForwardPayload { get; set; }
    }
}