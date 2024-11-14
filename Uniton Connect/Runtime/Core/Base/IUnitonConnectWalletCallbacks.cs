using TonSdk.Connect;
using UnitonConnect.Core.Data;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectWalletCallbacks
    {
        delegate void OnWalletConnectionFinish(Wallet wallet);
        delegate void OnNativeWalletConnectionFinish(NewWalletConfig wallet);

        delegate void OnWalletConnectionFail(string errorMessage);
        delegate void OnNativeWalletConnectionFail(string errorMessage);

        delegate void OnWalletConnectionRestore(bool isRestored, WalletConfig wallet);
        delegate void OnNativeWalletConnectionRestore(bool isRestored);

        delegate void OnWalletDisconnect();
        delegate void OnNativeWalletDisconnect(bool isSuccess);

        delegate void OnWalletConnectionPause();
        delegate void OnWalletConnectionUnPause();

        event OnWalletConnectionFinish OnWalletConnectionFinished;
        event OnNativeWalletConnectionFinish OnNativeWalletConnectionFinished;

        event OnWalletConnectionFail OnWalletConnectionFailed;
        event OnNativeWalletConnectionFail OnNativeWalletConnectionFailed;

        event OnWalletConnectionRestore OnWalletConnectionRestored;
        event OnNativeWalletConnectionRestore OnNativeWalletConnectionRestored;

        event OnWalletDisconnect OnWalletDisconnected;
        event OnNativeWalletDisconnect OnNativeWalletDisconnected;

        event OnWalletConnectionPause OnWalletConnectionPaused;
        event OnWalletConnectionUnPause OnWalletConnectonUnPaused;
    }
}