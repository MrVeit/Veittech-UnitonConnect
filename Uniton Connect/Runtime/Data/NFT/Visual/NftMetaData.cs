using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    [Serializable]
    public sealed class NftMetaData
    {
        [JsonProperty("name")]
        public string ItemName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string IconURL { get; set; }

        [JsonProperty("attributes")]
        public List<NftMetaAttributeData> Attributes { get; set; }
    }

    [Serializable]
    public sealed class NftMetaAttributeData
    {
        [JsonProperty("trait_type")]
        public string trait_type { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }
    }
}