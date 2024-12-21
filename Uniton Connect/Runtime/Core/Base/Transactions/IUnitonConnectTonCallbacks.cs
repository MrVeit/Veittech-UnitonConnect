using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectTonCallbacks
    {
        delegate void OnTonBalanceClaim(decimal tonBalance);

        delegate void OnTonTransactionSend(string transactionHash);
        delegate void OnTonTransactionSendFail(string errorMessage);

        delegate void OnTonTransactionConfirm(SuccessTransactionData transactionData);

        delegate void OnNativeTransactionSendingFinish(string transactionHash);
        delegate void OnNativeTransactionSendingFail(string errorMessage);

        delegate void OnNativeTransactionConfirm(SuccessTransactionData transactionData);

        event OnTonBalanceClaim OnTonBalanceClaimed;

        event OnTonTransactionSend OnTonTransactionSended;
        event OnTonTransactionSendFail OnTonTransactionSendFailed;

        event OnTonTransactionConfirm OnTonTransactionConfirmed;

        event OnNativeTransactionSendingFinish OnNativeSendingTonFinished;
        event OnNativeTransactionSendingFail OnNativeTransactionSendingFailed;

        event OnNativeTransactionConfirm OnNativeTransactionConfirmed;
    }
}