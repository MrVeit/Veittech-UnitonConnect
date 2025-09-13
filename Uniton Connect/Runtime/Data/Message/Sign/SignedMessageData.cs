using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class SignedMessageData
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("domain")]
        public string AppDomain { get; set; }

        [JsonProperty("payload")]
        public SignMessageData Payload { get; set; }
    }
}