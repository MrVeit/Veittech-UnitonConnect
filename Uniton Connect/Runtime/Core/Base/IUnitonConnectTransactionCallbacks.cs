using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectTransactionCallbacks : ITransactionCallbacks
    {
        delegate void OnTonBalanceClaim(decimal tonBalance);

        delegate void OnNativeTransactionSendingFinish(string transactionHash);
        delegate void OnNativeTransactionSendingFail(string errorMessage);

        delegate void OnNativeTransactionConfirm(SuccessTransactionData transactionData);

        event OnTonBalanceClaim OnTonBalanceClaimed;

        event OnNativeTransactionSendingFinish OnNativeSendingTonFinished;
        event OnNativeTransactionSendingFail OnNativeTransactionSendingFailed;

        event OnNativeTransactionConfirm OnNativeTransactionConfirmed;
    }
}