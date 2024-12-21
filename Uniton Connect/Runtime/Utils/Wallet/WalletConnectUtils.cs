using TonSdk.Core;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Utils
{
    public static class WalletConnectUtils
    {
        private static readonly UnitonConnectSDK _unitonConnect = UnitonConnectSDK.Instance;

        internal static bool IsAddressesMatch(string recipientAddress)
        {
            var authorizedWalletAddress = _unitonConnect.Wallet.ToString();

            if (authorizedWalletAddress == recipientAddress)
            {
                UnitonConnectLogger.LogWarning("The recipient and sender address match, " +
                    "the transaction will be canceled when you try to send it");

                return true;
            }

            return false;
        }

        internal static string GetHEXAddress(string address)
        {
            string rawAddress = ConvertAddressByType(address, AddressType.Raw);

            return rawAddress;
        }

        internal static string GetBounceableAddress(string address)
        {
            string bounceableAddress = ConvertAddressByType(address, 
                AddressType.Base64, new AddressStringifyOptions(true, false, false));

            return bounceableAddress;
        }

        internal static string GetNonBounceableAddress(string address)
        {
            string NonBounceableAddress = ConvertAddressByType(address, 
                AddressType.Base64, new AddressStringifyOptions(false, false, false));

            return NonBounceableAddress;
        }

        private static string ConvertAddressByType(string address, AddressType type,
            AddressStringifyOptions options = null)
        {
            return GetAddress(address).ToString(type, options);
        }

        private static Address GetAddress(string walletAddress)
        {
            return new Address(walletAddress);
        }
    }
}