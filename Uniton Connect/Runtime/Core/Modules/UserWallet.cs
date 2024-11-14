using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Common
{
    public sealed class UserWallet
    {
        private readonly string _address;

        public string PublicKey { get; private set; }
        public string Chain { get; private set; }
        public string StateInit { get; private set; }

        public UserWallet(string address,
            NewWalletConfig walletConfig)
        {
            _address = address;

            if (walletConfig == null)
            {
                return;
            }

            PublicKey = walletConfig.PublicKey;
            StateInit = walletConfig.StateInit;

            Chain = walletConfig.Chain;
        }

        public sealed override string ToString()
        {
            if (!UnitonConnectSDK.Instance.IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning($"Wallet is not connected, address is empty");

                return string.Empty;
            }

            return _address;
        }
    }
}