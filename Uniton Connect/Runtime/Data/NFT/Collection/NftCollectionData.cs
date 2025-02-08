using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    public sealed class NftCollectionData
    {
        [JsonProperty("nftItems")]
        public List<NftItemData> Items { get; set; }
    }
}