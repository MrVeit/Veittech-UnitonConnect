namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectSDKCallbacks
    {
        delegate void OnInitialize(bool isSuccess);
        delegate void OnNativeUnitonConnectInitialize(bool isSuccess);

        event OnInitialize OnInitiliazed;
        event OnNativeUnitonConnectInitialize OnNativeInitialized;
    }
}