using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectWalletCallbacks
    {
        delegate void OnWalletConnect(WalletConfig wallet);
        delegate void OnWalletConnectFail(string errorMessage);
        delegate void OnWalletConnectRestore(bool isRestored);
        delegate void OnWalletDisconnect(bool isSuccess);

        delegate void OnNativeWalletConnectionFinish(NewWalletConfig wallet);
        delegate void OnNativeWalletConnectionFail(string errorMessage);
        delegate void OnNativeWalletConnectionRestore(bool isRestored);
        delegate void OnNativeWalletDisconnect(bool isSuccess);

        event OnWalletConnect OnWalletConnected;
        event OnWalletConnectFail OnWalletConnectFailed;
        event OnWalletConnectRestore OnWalletConnectRestored;
        event OnWalletDisconnect OnWalletDisconnected;

        event OnNativeWalletConnectionFinish OnNativeWalletConnectionFinished;
        event OnNativeWalletConnectionFail OnNativeWalletConnectionFailed;
        event OnNativeWalletConnectionRestore OnNativeWalletConnectionRestored;
        event OnNativeWalletDisconnect OnNativeWalletDisconnected;
    }
}