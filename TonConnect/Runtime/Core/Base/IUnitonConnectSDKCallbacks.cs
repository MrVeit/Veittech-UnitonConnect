namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectSDKCallbacks
    {
        delegate void OnUnitonConnectInitialize();

        event OnUnitonConnectInitialize OnInitialized;
    }
}