using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonTransactionsListData
    {
        [JsonProperty("jetton_transfers")]
        public List<JettonTransactionData> Transfers { get; set; }

        [JsonProperty("address_book")]
        public Dictionary<string, AddressBookPage> AddressBook { get; set; }
    }
}