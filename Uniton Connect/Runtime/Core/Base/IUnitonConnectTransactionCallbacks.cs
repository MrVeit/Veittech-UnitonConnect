using TonSdk.Connect;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectTransactionCallbacks
    {
        delegate void OnTransactionSendingFinish(SendTransactionResult? transactionResult, bool isSuccess);

        event OnTransactionSendingFinish OnTransactionSendingFinished;
    }
}