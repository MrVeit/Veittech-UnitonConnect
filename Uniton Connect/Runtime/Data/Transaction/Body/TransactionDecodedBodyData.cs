using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionDecodedBodyData
    {
        // TON TRANSACTION BODY

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("subwallet_id")]
        public int SubWalletId { get; set; }

        [JsonProperty("valid_until")]
        public int ValidUntil { get; set; }

        [JsonProperty("seqno")]
        public int Seqno { get; set; }

        [JsonProperty("op")]
        public int Op { get; set; }

        [JsonProperty("payload")]
        public List<TransactionPayloadData> Payloads { get; set; }

        [JsonProperty("text")]
        public string MessageText { get; set; }

        // JETTON TRANSACTION BODY

        [JsonProperty("query_id")]
        public long QueryId { get; set; }

        [JsonProperty("amount")]
        public string SendedAmount { get; set; }

        [JsonProperty("destination")]
        public string RecipientAddress { get; set; }

        [JsonProperty("response_destination")]
        public string SenderAddress { get; set; }

        [JsonProperty("forward_ton_amount")]
        public string ForwardTonAmount { get; set; }

        [JsonProperty("forward_amount")]
        public string ForwardAmount { get; set; }

        [JsonProperty("forward_payload")]
        public TransactionBodyData? ForwardPayload { get; set; }

        // NFT TRANSACTION BODY

        [JsonProperty("new_owner")]
        public string NewOwner { get; set; }

        [JsonProperty("custom_payload")]
        public object CustomPayload { get; set; }
    }
}