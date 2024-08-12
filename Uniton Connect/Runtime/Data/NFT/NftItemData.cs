using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    [Serializable]
    public sealed class NftItemData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("index")]
        public long Id { get; set; }

        [JsonProperty("owner")]
        public NftCollectionOwnerData Owner { get; set; }

        [JsonProperty("collection")]
        public NftCollectionHeaderData Collection { get; set; }

        [JsonProperty("verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("metadata")]
        public NftMetaData Metadata { get; set; }

        [JsonProperty("sale")]
        public NftSellerData Seller { get; set; }

        [JsonProperty("previews")]
        public List<NftPreviewData> Previews { get; set; }

        [JsonProperty("dns")]
        public string Dns { get; set; }

        [JsonProperty("approved_by")]
        public List<string> ApprovedBy { get; set; }

        [JsonProperty("include_cnft")]
        public bool IncludeCnft { get; set; }

        [JsonProperty("trust")]
        public string Trust { get; set; }
    }
}