using System;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    [Serializable]
    public sealed class NftSellerData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("market")]
        public NftMarketData Market { get; set; }

        [JsonProperty("owner")]
        public NftCollectionOwnerData Owner { get; set; }

        [JsonProperty("price")]
        public NftPriceData Price { get; set; }
    }
}