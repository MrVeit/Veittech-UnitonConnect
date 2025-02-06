using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class LoadedTransactionPayloadData
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("payload")]
        public string Payload { get; set; }
    }
}