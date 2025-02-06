using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class NftTransactionPayloadData
    {
        [JsonProperty("senderAddress")]
        public string Sender { get; set; }

        [JsonProperty("recipientAddress")]
        public string Recipient { get; set; }
    }
}