using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TonApiResponseErrorData
    {
        [JsonProperty("error")]
        public string Message { get; set; }
    }
}