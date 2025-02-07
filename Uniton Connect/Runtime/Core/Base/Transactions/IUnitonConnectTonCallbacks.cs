using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectTonCallbacks
    {
        delegate void OnTonBalanceClaim(decimal tonBalance);

        delegate void OnTonTransactionSend(string transactionHash);
        delegate void OnTonTransactionSendFail(string errorMessage);

        delegate void OnTonTransactionConfirm(SuccessTransactionData transactionData);

        event OnTonBalanceClaim OnTonBalanceClaimed;

        event OnTonTransactionSend OnTonTransactionSended;
        event OnTonTransactionSendFail OnTonTransactionSendFailed;

        event OnTonTransactionConfirm OnTonTransactionConfirmed;
    }
}