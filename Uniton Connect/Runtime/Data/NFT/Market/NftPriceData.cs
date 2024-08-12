using System;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    [Serializable]
    public sealed class NftPriceData
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("token_name")]
        public string TokenName { get; set; }
    }
}