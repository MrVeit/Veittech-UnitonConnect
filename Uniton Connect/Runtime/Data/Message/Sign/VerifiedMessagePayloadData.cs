using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class VerifiedMessagePayloadData
    {
        [JsonProperty("isVerified")]
        public bool IsVerified { get; set; }

        [JsonProperty("signedMessage")]
        public SignMessageData SignedMessage { get; set; }
    }
}