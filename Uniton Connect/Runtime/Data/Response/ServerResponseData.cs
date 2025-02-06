using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class ServerResponseData
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}