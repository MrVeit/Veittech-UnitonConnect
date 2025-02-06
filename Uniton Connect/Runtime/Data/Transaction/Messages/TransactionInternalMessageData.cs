using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionInternalMessageData
    {
        [JsonProperty("ihr_disabled")]
        public bool IhrDisabled { get; set; }

        [JsonProperty("bounce")]
        public bool IsBounce { get; set; }

        [JsonProperty("bounced")]
        public bool IsBounced { get; set; }

        [JsonProperty("src")]
        public string Sender { get; set; }

        [JsonProperty("dest")]
        public string Recipient { get; set; }

        [JsonProperty("value")]
        public TransactionSendedValueData SendedValue { get; set; }

        [JsonProperty("ihr_fee")]
        public string IhrFee { get; set; }

        [JsonProperty("fwd_fee")]
        public string FwdFee { get; set; }

        [JsonProperty("created_lt")]
        public int CreatedIt { get; set; }

        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }

        [JsonProperty("init")]
        public object Init { get; set; }

        [JsonProperty("body")]
        public TransactionBodyData Body { get; set; }
    }
}