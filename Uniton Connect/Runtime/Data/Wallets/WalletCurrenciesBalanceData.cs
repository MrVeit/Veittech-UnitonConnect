using System;
using Newtonsoft.Json;

namespace UnitonConnect.Runtime.Data
{
    [Serializable]
    public sealed class WalletCurrenciesBalanceData
    {
        [JsonProperty("description")]
        public string FiatAssets { get; set; }
    }
}