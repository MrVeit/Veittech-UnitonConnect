using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnitonConnect.Runtime.Data;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public sealed class UserAccountData
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("balance")]
        public long Balance { get; set; }

        [JsonProperty("currencies_balance")]
        public WalletCurrenciesBalanceData CurrenciesBalance { get; set; }

        [JsonProperty("last_activity")]
        public long LastActivity { get; set; }

        [JsonProperty("status")]
        public AccountStatuses Status { get; set; }

        [JsonProperty("interfaces")]
        public List<string> Interfaces { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_scam")]
        public bool IsScam { get; set; }

        [JsonProperty("icon")]
        public string IconUrl { get; set; }

        [JsonProperty("memo_required")]
        public bool MemoRequired { get; set; }

        [JsonProperty("get_methods")]
        public List<string> GetMethods { get; set; }

        [JsonProperty("is_suspended")]
        public bool IsSuspended { get; set; }

        [JsonProperty("is_wallet")]
        public bool IsWallet { get; set; }
    }

    public enum AccountStatuses
    {
        nonexist,
        uninit,
        active,
        frozen
    }
}