using System.Collections.Generic;
using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectJettonCallbacks
    {
        delegate void OnJettonBalanceLoaded(decimal balance, 
            string jettonName, string masterAddress);

        delegate void OnJettonAddressParsed(string jettonAddress);

        delegate void OnLastJettonTransactionsLoaded(TransactionTypes type, 
            List<JettonTransactionData> transactions);

        delegate void OnJettonTransactionSend(string masterAddress,
            SuccessTransactionData transactionData);
        delegate void OnJettonTransactionSendFail(
            string masterAddress, string errorMessage);

        event OnJettonBalanceLoaded OnBalanceLoaded;
        event OnJettonAddressParsed OnAddressParsed;

        event OnLastJettonTransactionsLoaded OnLastTransactionsLoaded;

        event OnJettonTransactionSend OnTransactionSended;
        event OnJettonTransactionSendFail OnTransactionSendFailed;
    }
}