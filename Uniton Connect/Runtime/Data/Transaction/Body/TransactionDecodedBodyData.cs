using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
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
        public string QueryId { get; set; }

        [JsonProperty("amount")]
        public string SendedAmount { get; set; }

        [JsonProperty("from")]
        public string SenderAddress { get; set; }

        [JsonProperty("response_address")]
        public string RecipientAddress { get; set; }

        [JsonProperty("forward_ton_amount")]
        public string ForwardTonAmount { get; set; }

        [JsonProperty("forward_payload")]
        public object ForwardPayload { get; set; }
    }
}