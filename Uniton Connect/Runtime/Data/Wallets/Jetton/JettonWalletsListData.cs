using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonWalletsListData
    {
        [JsonProperty("jetton_wallets")]
        public List<JettonWalletData> JettonWallets { get; set; }

        [JsonProperty("address_book")]
        public Dictionary<string, AddressBookPage> AddressBook { get; set; }
    }
}