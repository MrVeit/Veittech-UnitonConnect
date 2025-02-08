using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonConfigData
    {
        [JsonProperty("jettonBalance")]
        public JettonBalanceData JettonConfig { get; set; }
    }
}