using Newtonsoft.Json;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Data
{
    public sealed class ModalStateData
    {
        [JsonProperty("status")]
        public ModalStatusTypes Status { get; set; }

        [JsonProperty("closeReason")]
        public string CloseReason { get; set; }
    }
}