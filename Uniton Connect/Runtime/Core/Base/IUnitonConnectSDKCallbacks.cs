namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectSDKCallbacks
    {
        delegate void OnUnitonConnectInitialize();
        delegate void OnNativeUnitonConnectInitialize(bool isSuccess);

        event OnUnitonConnectInitialize OnInitialized;
        event OnNativeUnitonConnectInitialize OnNativeInitialized;
    }
}