using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TonCenterResponseErrorData
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("error")]
        public string Message { get; set; }
    }
}