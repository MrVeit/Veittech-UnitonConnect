using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class LoadedJettonWalletData
    {
        [JsonProperty("jettonWallet")]
        public JettonWalletData Wallet { get; set; }
    }
}