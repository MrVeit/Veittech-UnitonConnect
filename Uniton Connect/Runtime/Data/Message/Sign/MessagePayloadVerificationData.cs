using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class MessagePayloadVerificationData
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("domain")]
        public string AppDomain { get; set; }

        [JsonProperty("messagePayload")]
        public SignMessageData MessagePayload { get; set; }

        [JsonProperty("address")]
        public string WalletAddress { get; set; }

        [JsonProperty("walletPublicKey")]
        public string WalletPublicKey { get; set; }

        [JsonProperty("walletStateInit")]
        public string WalletStateInit { get; set; }
    }
}