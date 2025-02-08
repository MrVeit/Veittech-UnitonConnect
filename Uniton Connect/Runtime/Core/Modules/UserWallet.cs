using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Core.Utils.Debugging;

using AddressUtils = UnitonConnect.Core.TonConnectBridge.Utils.Address;

namespace UnitonConnect.Core.Common
{
    public sealed class UserWallet
    {
        private readonly string _address;

        public string PublicKey { get; private set; }
        public string Chain { get; private set; }
        public string StateInit { get; private set; }

        public bool IsUserFriendly => AddressUtils.IsUserFriendly(this.ToString());
        public bool IsBounceable => AddressUtils.IsBounceable(this.ToString());
        public bool IsTestOnly => AddressUtils.IsTestOnly(this.ToString());

        public UserWallet(string address,
            WalletConfig walletConfig)
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

        /// <summary>
        /// Returns the wallet address after a successful connection in its original form
        /// </summary>
        /// <returns></returns>
        public sealed override string ToString()
        {
            if (!IsAvaibleAddress())
            {
                return string.Empty;
            }

            return _address;
        }

        /// <summary>
        /// Returns the first and last characters of the wallet address to be briefly displayed in the interface
        /// </summary>
        /// <param name="charAmount">Number of characters to display among the first and last</param>
        public string ToShort(int charAmount)
        {
            if (!IsAvaibleAddress())
            {
                return string.Empty;
            }

            return WalletVisualUtils.ProcessWalletAddress(_address, charAmount);
        }

        /// <summary>
        /// Convert wallet address to Bounceable format (base64), example:
        /// EQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2QON
        /// </summary>
        public string ToBounceable()
        {
            if (!IsAvaibleAddress())
            {
                return string.Empty;
            }

            return WalletConnectUtils.GetBounceableAddress(_address);
        }

        /// <summary>
        /// Convert wallet address to Non Bounceable format (base64), example:
        /// UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk+yXLZXmc2V5I
        /// </summary>
        public string ToNonBounceable()
        {
            if (!IsAvaibleAddress())
            {
                return string.Empty;
            }

            return WalletConnectUtils.GetNonBounceableAddress(_address);
        }

        /// <summary>
        /// Convert wallet address to HEX/RAW format, example:
        /// 0:c1da9d221d87032b762ad647658a2b5115a38af4b2329d4824fb25cb65799cd9
        /// </summary>
        public string ToHex()
        {
            if (!IsAvaibleAddress())
            {
                return string.Empty;
            }

            return WalletConnectUtils.GetHEXAddress(_address);
        }

        private bool IsAvaibleAddress()
        {
            if (!UnitonConnectSDK.Instance.IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning($"Wallet is not connected, address is empty");

                return false;
            }

            return true;
        }
    }
}