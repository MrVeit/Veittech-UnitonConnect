using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionActionPhaseData
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("result_code")]
        public int ResultCode { get; set; }

        [JsonProperty("total_actions")]
        public int TotalActions { get; set; }

        [JsonProperty("skipped_actions")]
        public int SkippedActions { get; set; }

        [JsonProperty("fwd_fees")]
        public decimal ForwardFees { get; set; }

        [JsonProperty("total_fees")]
        public decimal TotalFees { get; set; }

        [JsonProperty("result_code_description")]
        public string ResultCodeDescription { get; set; }
    }
}