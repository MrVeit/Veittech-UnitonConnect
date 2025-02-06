using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonTransactionPayloadData
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("gasFee")]
        public decimal GasFeeInTon { get; set; }

        [JsonProperty("senderAddress")]
        public string SenderTonAddress { get; set; }

        [JsonProperty("recipientAddress")]
        public string RecipientTonAddress { get; set; }

        [JsonProperty("jettonType")]
        public string ShortName { get; set; }

        [JsonProperty("message")]
        public string? Comment { get; set; }
    }
}