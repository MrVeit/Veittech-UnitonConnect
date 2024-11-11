using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public struct ConnectedWalletConfigData
    {
        public ConnectedWalletConfigData(string address, 
            string chain, string stateInit, string publicKey)
        {
            Address = address;
            Chain = chain;
            StateInit = stateInit;
            PublicKey = publicKey;
        }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("chain")]
        public string Chain { get; set; }

        [JsonProperty("walletStateInit")]
        public string StateInit { get; set; }

        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }
    }
}