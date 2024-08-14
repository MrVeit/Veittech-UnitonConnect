using TonSdk.Connect;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectTransactionCallbacks
    {
        delegate void OnTonBalanceClaim(decimal tonBalance);
        delegate void OnTransactionSendingFinish(SendTransactionResult? transactionResult, bool isSuccess);

        event OnTransactionSendingFinish OnSendingTonFinished;
        event OnTonBalanceClaim OnTonBalanceClaimed;
    }
}