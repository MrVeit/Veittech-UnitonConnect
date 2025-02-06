using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectWalletCallbacks
    {
        delegate void OnWalletConnect(WalletConfig wallet);
        delegate void OnWalletConnectFail(string errorMessage);
        delegate void OnWalletConnectRestore(bool isRestored);
        delegate void OnWalletDisconnect(bool isSuccess);

        event OnWalletConnect OnWalletConnected;
        event OnWalletConnectFail OnWalletConnectFailed;
        event OnWalletConnectRestore OnWalletConnectRestored;
        event OnWalletDisconnect OnWalletDisconnected;
    }
}