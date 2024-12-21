using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectJettonCallbacks
    {
        delegate void OnJettonAddressParsed(string jettonAddress);

        delegate void OnJettonTransactionSend(string masterAddress,
            SuccessTransactionData transactionData);
        delegate void OnJettonTransactionSendFail(string masterAddress,
            string errorMessage);

        event OnJettonAddressParsed OnAddressParsed;

        event OnJettonTransactionSend OnTransactionSended;
        event OnJettonTransactionSendFail OnTransactionSendFailed;
    }
}