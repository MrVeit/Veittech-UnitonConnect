using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionDecodedBodyData
    {
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
    }
}