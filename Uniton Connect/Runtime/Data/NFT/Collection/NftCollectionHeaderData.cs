using System;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    [Serializable]
    public sealed class NftCollectionHeaderData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}