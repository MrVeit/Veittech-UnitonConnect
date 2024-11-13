using System;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class TransactionComputePhaseData
    {
        [JsonProperty("skipped")]
        public bool IsSkipped { get; set; }

        [JsonProperty("skip_reason")]
        public string SkipReason { get; set; }

        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("gas_fees")]
        public long GasFees { get; set; }

        [JsonProperty("gas_used")]
        public long GasUsed { get; set; }

        [JsonProperty("vm_steps")]
        public int VmSteps { get; set; }

        [JsonProperty("exit_code")]
        public int ExitCode { get; set; }

        [JsonProperty("exit_code_description")]
        public string ExitCodeDescription { get; set; }
    }
}