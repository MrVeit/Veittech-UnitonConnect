using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    public sealed class NftCollectionData
    {
        [JsonProperty("nft_items")]
        public List<NftItemData> Items { get; set; }
    }
}