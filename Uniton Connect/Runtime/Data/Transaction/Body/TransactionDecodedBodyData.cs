using Newtonsoft.Json;

namespace UnitonConnect.Core.Data
{
    public sealed class TransactionDecodedBodyData
    {
        // TON TRANSACTION BODY

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("subwallet_id")]
        public int SubWalletId { get; set; }

        [JsonProperty("valid_until")]
        public long ValidUntil { get; set; }

        [JsonProperty("seqno")]
        public int Seqno { get; set; }

        [JsonProperty("op")]
        public int? Op { get; set; }

        [JsonProperty("payload")]
        public TransactionInternalPayloadData Payload { get; set; }

        [JsonProperty("text")]
        public string? MessageText { get; set; }

        // JETTON TRANSACTION BODY

        [JsonProperty("query_id")]
        public long QueryId { get; set; }

        [JsonProperty("amount")]
        public string SendedAmount { get; set; }

        [JsonProperty("destination")]
        public string RecipientAddress { get; set; }

        [JsonProperty("response_destination")]
        public string SenderAddress { get; set; }

        [JsonProperty("forward_ton_amount")]
        public string ForwardTonAmount { get; set; }

        [JsonProperty("forward_amount")]
        public string ForwardAmount { get; set; }

        [JsonProperty("forward_payload")]
        public TransactionBodyData? ForwardPayload { get; set; }

        // NFT TRANSACTION BODY

        [JsonProperty("new_owner")]
        public string NewOwner { get; set; }

        [JsonProperty("custom_payload")]
        public object CustomPayload { get; set; }
    }

    public sealed class TransactionInternalPayloadData
    {
        [JsonProperty("sum_type")]
        public string SumType { get; set; }

        [JsonProperty("simple_send")]
        public TransactionSimpleSendData SimpleSend { get; set; }

        [JsonProperty("deploy_and_install_plugin")]
        public DeployAndInstallPluginData DeployAndInstallPlugin { get; set; }

        [JsonProperty("install_plugin")]
        public PluginActionData InstallPlugin { get; set; }

        [JsonProperty("remove_plugin")]
        public PluginActionData RemovePlugin { get; set; }
    }

    public sealed class TransactionSimpleSendData
    {
        [JsonProperty("payload")]
        public TransactionPayloadData[] Payloads { get; set; }
    }

    public sealed class DeployAndInstallPluginData
    {
        [JsonProperty("plugin_workchain")]
        public int Workchain { get; set; }

        [JsonProperty("plugin_balance")]
        public string Balance { get; set; }

        [JsonProperty("state_init")]
        public PluginStateInit StateInit { get; set; }

        [JsonProperty("body")]
        public object Body { get; set; }
    }

    public sealed class PluginActionData
    {
        [JsonProperty("plugin_workchain")]
        public string Workchain { get; set; }

        [JsonProperty("plugin_address")]
        public string Address { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("query_id")]
        public long QueryId { get; set; }
    }

    public sealed class PluginStateInit
    {
        [JsonProperty("split_depth")]
        public object? SplitDepth { get; set; }

        [JsonProperty("special")]
        public object? Special { get; set; }

        [JsonProperty("code")]
        public object? Code { get; set; }

        [JsonProperty("data")]
        public object? Data { get; set; }

        [JsonProperty("library")]
        public object Library { get; set; }
    }
}