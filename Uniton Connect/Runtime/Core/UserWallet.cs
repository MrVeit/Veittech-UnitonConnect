using UnitonConnect.Core.Data;

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

            PublicKey = walletConfig.PublicKey;
            StateInit = walletConfig.StateInit;

            Chain = walletConfig.Chain;
        }

        public sealed override string ToString()
        {
            return _address;
        }
    }
}