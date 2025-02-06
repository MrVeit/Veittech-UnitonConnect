using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    public sealed class WalletCurrenciesBalanceData
    {
        [JsonProperty("description")]
        public string FiatAssets { get; set; }
    }
}