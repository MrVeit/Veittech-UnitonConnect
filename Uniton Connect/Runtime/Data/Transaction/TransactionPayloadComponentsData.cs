using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionPayloadComponentsData
    {
        [JsonProperty("amount")]
        public decimal JettonAmount { get; set; }

        [JsonProperty("gasFee")]
        public decimal GasFeeInTon { get; set; }

        [JsonProperty("senderAddress")]
        public string SenderTonAddress { get; set; }

        [JsonProperty("recipientAddress")]
        public string RecipientJettonAddress { get; set; }
    }
}