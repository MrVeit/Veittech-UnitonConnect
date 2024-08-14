using TonSdk.Connect;

namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectWalletCallbacks
    {
        delegate void OnWalletConnectionFinish(Wallet wallet);
        delegate void OnWalletConnectionFail(string errorMessage);

        delegate void OnWalletConnectionRestore(bool isRestored, WalletConfig wallet);
        delegate void OnWalletDisconnect();

        delegate void OnWalletConnectionPause();
        delegate void OnWalletConnectionUnPause();

        event OnWalletConnectionFinish OnWalletConnectionFinished;
        event OnWalletConnectionFail OnWalletConnectionFailed;

        event OnWalletConnectionRestore OnWalletConnectionRestored;
        event OnWalletDisconnect OnWalletDisconnected;

        event OnWalletConnectionPause OnWalletConnectionPaused;
        event OnWalletConnectionUnPause OnWalletConnectonUnPaused;
    }
}