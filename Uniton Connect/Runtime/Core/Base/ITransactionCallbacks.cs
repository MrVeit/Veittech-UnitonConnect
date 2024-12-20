using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface ITransactionCallbacks
    {
        delegate void OnTransactionSend(string transactionHash);
        delegate void OnTransactionSendFail(string errorMessage);

        delegate void OnTransactionConfirm(SuccessTransactionData transactionData);

        event OnTransactionSend OnTransactionSended;
        event OnTransactionSendFail OnTransactionSendFailed;

        event OnTransactionConfirm OnTransactionConfirmed;
    }
}