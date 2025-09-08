namespace UnitonConnect.Core.Common
{
    public interface IUnitonConenctModalCallbacks
    {
        delegate void OnStateChange(ModalStatusTypes state);

        event OnStateChange OnStateChanged;
    }
}