using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class JettonTransactionsListData
    {
        [JsonProperty("jettonTransactions")]
        public List<JettonTransactionData> Transfers { get; set; }
    }
}