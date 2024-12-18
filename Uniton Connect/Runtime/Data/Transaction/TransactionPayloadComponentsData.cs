using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionPayloadComponentsData
    {
        [JsonProperty("amount")]
        public string JettonAmount { get; set; }

        [JsonProperty("gasFee")]
        public string GasFeeInTon { get; set; }

        [JsonProperty("senderAddress")]
        public string SenderTonAddress { get; set; }

        [JsonProperty("recipientAddress")]
        public string RecipientJettonAddress { get; set; }
    }
}