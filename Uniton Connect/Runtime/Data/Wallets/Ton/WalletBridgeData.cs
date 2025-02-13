using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class WalletBridgeData
    {
        [JsonProperty("type")]  
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; } 
    }
}