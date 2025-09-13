namespace UnitonConnect.Core.Common
{
    public interface IUnitonConnectModalCallbacks
    {
        delegate void OnStateChange(ModalStatusTypes state);
        delegate void OnStateClaim(ModalStatusTypes state);

        event OnStateChange OnStateChanged;
        event OnStateClaim OnStateClaimed;
    }
}