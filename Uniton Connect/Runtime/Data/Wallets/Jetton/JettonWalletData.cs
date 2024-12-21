using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class JettonWalletData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("jetton")]
        public string MasterAddress { get; set; }

        [JsonProperty("last_transaction_lt")]
        public string LastTransactionLt { get; set; }

        [JsonProperty("code_hash")]
        public string CodeHash { get; set; }

        [JsonProperty("data_hash")]
        public string DataHash { get; set; }
    }

    [Serializable]
    public sealed class MintlessInfo
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("custom_payload_api_uri")]
        public List<string> CustomPayloadApiUrl { get; set; }

        [JsonProperty("expire_at")]
        public long ExpireAt { get; set; }

        [JsonProperty("start_from")]
        public long StartFrom { get; set; }
    }

    [Serializable]
    public sealed class AddressBookPage
    {
        [JsonProperty("user_friendly")]
        public string UserFriendly { get; set; }
    }
}