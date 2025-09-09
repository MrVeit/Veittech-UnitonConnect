namespace UnitonConnect.Core.Common
{
    public interface IUnitonConenctModalCallbacks
    {
        delegate void OnStateChange(ModalStatusTypes state);
        delegate void OnStateClaim(ModalStatusTypes state);

        event OnStateChange OnStateChanged;
        event OnStateClaim OnStateClaimed;
    }
}