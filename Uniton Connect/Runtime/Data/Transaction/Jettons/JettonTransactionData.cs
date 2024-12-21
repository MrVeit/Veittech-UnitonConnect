using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonTransactionData
    {
        [JsonProperty("query_id")]
        public string QueryId { get; set; }

        [JsonProperty("source")]
        public string SenderAddress { get; set; }

        [JsonProperty("destination")]
        public string RecipientAddress { get; set; }

        [JsonProperty("amount")]
        public string AmountInNano { get; set; }

        [JsonProperty("source_wallet")]
        public string SourceWallet { get; set; }

        [JsonProperty("jetton_master")]
        public string JettonMasterAddress { get; set; }

        [JsonProperty("transaction_hash")]
        public string TransactionHash { get; set; }

        [JsonProperty("transaction_lt")]
        public string TransactionLt { get; set; }

        [JsonProperty("transaction_now")]
        public long TransactionNow { get; set; }

        [JsonProperty("transaction_aborted")]
        public bool IsAborted { get; set; }

        [JsonProperty("response_destination")]
        public string ResponseDestination { get; set; }

        [JsonProperty("custom_payload")]
        public object CustomPayload { get; set; }

        [JsonProperty("forward_ton_amount")]
        public string ForwardTonAmount { get; set; }

        [JsonProperty("forward_payload")]
        public object ForwardPayload { get; set; }

        [JsonProperty("trace_id")]
        public string TraceId { get; set; }
    }
}