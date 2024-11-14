using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public class InMessageData
    {
        [JsonProperty("msg_type")]
        public string MessageType { get; set; }

        [JsonProperty("created_lt")]
        public long CreatedIt { get; set; }

        [JsonProperty("ihr_disabled")]
        public bool IsIhrDisabled { get; set; }

        [JsonProperty("bounce")]
        public bool IsBounce { get; set; }

        [JsonProperty("bounced")]
        public bool IsBounced { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("fwd_fee")]
        public decimal ForwardFee { get; set; }

        [JsonProperty("ihr_fee")]
        public long IhrFee { get; set; }

        [JsonProperty("destination")]
        public TransactionRecipientData Recipient { get; set; }

        [JsonProperty("source")]
        public TransactionSenderData Sender { get; set; }

        [JsonProperty("import_fee")]
        public long ImportFee { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("op_code")]
        public string OpCode { get; set; }

        [JsonProperty("init")]
        public TransactionInitData InitData { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("raw_body")]
        public string RawBody { get; set; }

        [JsonProperty("decoded_op_name")]
        public string DecodedOpName { get; set; }

        [JsonProperty("decoded_body")]
        public TransactionDecodedBodyData DecodedBody { get; set; }
    }

    [Serializable]
    public sealed class TransactionInitData
    {
        [JsonProperty("boc")]
        public string Boc { get; set; }

        [JsonProperty("interfaces")]
        public List<string> Interfaces { get; set; }
    }
}