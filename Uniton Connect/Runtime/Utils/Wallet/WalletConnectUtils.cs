using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.Core.Utils
{
    public static class WalletConnectUtils
    {
        private static readonly UnitonConnectSDK _unitonConnect = UnitonConnectSDK.Instance;

        internal static bool IsAddressesMatch(string recipientAddress)
        {
            var authorizedWalletAddress = _unitonConnect.Wallet.ToHex();

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
            string rawAddress = ConvertAddressByType(
                address, AddressType.Raw);

            return rawAddress;
        }

        internal static string GetBounceableAddress(string address)
        {
            string bounceableAddress = ConvertAddressByType(
                address, AddressType.Bounceable);

            return bounceableAddress;
        }

        internal static string GetNonBounceableAddress(string address)
        {
            string NonBounceableAddress = ConvertAddressByType(
                address, AddressType.NonBounceable);

            return NonBounceableAddress;
        }

        private static string ConvertAddressByType(
            string address, AddressType type)
        {
            return GetAddress(address, type);
        }

        private static string GetAddress(
            string walletAddress, AddressType type)
        {
            string targetAddress = walletAddress;

            if (type == AddressType.Raw)
            {
                TonConnectBridge.Utils.Address.ToHex(
                    walletAddress, (address) =>
                {
                    targetAddress = address;
                });
            }

            if (type == AddressType.Bounceable)
            {
                TonConnectBridge.Utils.Address.ToBounceable(
                    walletAddress, (address) =>
                {
                    targetAddress = address;
                });
            }

            if (type == AddressType.NonBounceable)
            {
                TonConnectBridge.Utils.Address.ToNonBounceable(
                    walletAddress, (address) =>
                {
                    targetAddress = address;
                });
            }

            UnitonConnectLogger.Log($"Address: {walletAddress} converted " +
                $"to {type} format, value: {targetAddress}");

            return targetAddress;
        }
    }

    public enum AddressType
    {
        Raw,
        Bounceable,
        NonBounceable
    }
}