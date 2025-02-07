namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectSDKCallbacks
    {
        delegate void OnInitialize(bool isSuccess);

        event OnInitialize OnInitiliazed;
    }
}