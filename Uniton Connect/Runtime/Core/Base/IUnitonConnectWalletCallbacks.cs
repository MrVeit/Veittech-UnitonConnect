using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectWalletCallbacks
    {
        delegate void OnConnect(WalletConfig wallet);
        delegate void OnConnectFail(string errorMessage);
        delegate void OnConnectRestore(bool isRestored);
        delegate void OnDisconnect(bool isSuccess);

        delegate void OnNativeWalletConnectionFinish(NewWalletConfig wallet);
        delegate void OnNativeWalletConnectionFail(string errorMessage);
        delegate void OnNativeWalletConnectionRestore(bool isRestored);
        delegate void OnNativeWalletDisconnect(bool isSuccess);

        event OnConnect OnConnected;
        event OnConnectFail OnConnectFailed;
        event OnConnectRestore OnConnectRestored;
        event OnDisconnect OnDisconnected;

        event OnNativeWalletConnectionFinish OnNativeWalletConnectionFinished;
        event OnNativeWalletConnectionFail OnNativeWalletConnectionFailed;
        event OnNativeWalletConnectionRestore OnNativeWalletConnectionRestored;
        event OnNativeWalletDisconnect OnNativeWalletDisconnected;
    }
}