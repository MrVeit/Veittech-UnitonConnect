using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionPayloadData
    {
        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("message")]
        public TransactionMessageData Message { get; set; }
    }
}