using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    public sealed class NftPreviewData
    {
        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}