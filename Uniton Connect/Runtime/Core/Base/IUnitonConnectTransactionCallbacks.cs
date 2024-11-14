using TonSdk.Connect;
using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectTransactionCallbacks
    {
        delegate void OnTonBalanceClaim(decimal tonBalance);

        delegate void OnTransactionSendingFinish(SendTransactionResult? transactionResult, bool isSuccess);

        delegate void OnNativeTransactionSendingFinish(string transactionHash);
        delegate void OnNativeTransactionConfirm(SuccessTransactionData transactionData);

        delegate void OnNativeTransactionSendingFail(string errorMessage);

        event OnTonBalanceClaim OnTonBalanceClaimed;

        event OnTransactionSendingFinish OnSendingTonFinished;

        event OnNativeTransactionSendingFinish OnNativeSendingTonFinished;
        event OnNativeTransactionConfirm OnNativeTransactionConfirmed;

        event OnNativeTransactionSendingFail OnNativeTransactionSendingFailed;
    }
}