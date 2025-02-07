using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class SuccessTransactionData
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("lt")]
        public long It { get; set; }

        [JsonProperty("account")]
        public WalletAccountData Account { get; set; }

        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("utime")]
        public long UniversalTime { get; set; }

        [JsonProperty("orig_status")]
        public string OriginalStatus { get; set; }

        [JsonProperty("end_status")]
        public string EndStatus { get; set; }

        [JsonProperty("total_fees")]
        public decimal TotalFees { get; set; }

        [JsonProperty("end_balance")]
        public decimal EndBalance { get; set; }

        [JsonProperty("transaction_type")]
        public string TransactionType { get; set; }

        [JsonProperty("state_update_old")]
        public string StateUpdateOld { get; set; }

        [JsonProperty("state_update_new")]
        public string StateUpdateNew { get; set; }

        [JsonProperty("in_msg")]
        public InMessageData InMessage { get; set; }

        [JsonProperty("out_msgs")]
        public List<OutMessageData> OutMessages { get; set; }

        [JsonProperty("block")]
        public string Block { get; set; }

        [JsonProperty("prev_trans_hash")]
        public string PreviosTransactionHash { get; set; }

        [JsonProperty("prev_trans_lt")]
        public long PreviosTransactionIt { get; set; }

        [JsonProperty("compute_phase")]
        public TransactionComputePhaseData ComputePhase { get; set; }

        [JsonProperty("storage_phase")]
        public TransactionStoragePhaseData StoragePhase { get; set; }

        [JsonProperty("credit_phase")]
        public TransactionCreditPhaseData CreditPhase { get; set; }

        [JsonProperty("action_phase")]
        public TransactionActionPhaseData ActionPhase { get; set; }

        [JsonProperty("bounce_phase")]
        public string BouncePhase { get; set; }

        [JsonProperty("aborted")]
        public bool IsAborted { get; set; }

        [JsonProperty("destroyed")]
        public bool IsDestroyed { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }
    }
}