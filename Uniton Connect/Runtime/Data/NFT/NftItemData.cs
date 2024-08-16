using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnitonConnect.Core.Utils.Debugging;

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

        public bool IsScam()
        {
            if (ApprovedBy == null ||
                ApprovedBy.Count == 0 ||
                Trust == "none")
            {
                return false;
            }

            return true;
        }

        public string GetBestResolutionPng()
        {
            return Metadata.IconURL;
        }

        public string Get5x5ResolutionWebp()
        {
            if (!IsDetectedTargetSize(0))
            {
                UnitonConnectLogger.LogError("5x5 image size not detected");

                return string.Empty;
            }

            return Previews[0].Url;
        }

        public string Get100x100ResolutionWebp()
        {
            if (!IsDetectedTargetSize(1))
            {
                UnitonConnectLogger.LogError("100x100 image size not detected");

                return string.Empty;
            }

            return Previews[1].Url;
        }

        public string Get500x500ResolutionWebp()
        {
            if (!IsDetectedTargetSize(2))
            {
                UnitonConnectLogger.LogError("500x500 image size not detected");

                return string.Empty;
            }

            return Previews[2].Url;
        }

        public string Get1500x1500ResolutionWebp()
        {
            if (!IsDetectedTargetSize(3))
            {
                UnitonConnectLogger.LogError("1500x1500 image size not detected");

                return string.Empty;
            }

            return Previews[3].Url;
        }

        private bool IsDetectedTargetSize(int id)
        {
            if (Previews[id] == null || Previews.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}