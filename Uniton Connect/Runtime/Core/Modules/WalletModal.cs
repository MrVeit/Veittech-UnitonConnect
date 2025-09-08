namespace UnitonConnect.Core.Common
{
    public sealed class WalletModal: IUnitonConenctModalCallbacks
    {
        public ModalStatusTypes Status { get; private set; }
        public string? CloseReason { get; private set; }

        public event IUnitonConenctModalCallbacks.OnStateChange OnStateChanged;

        public WalletModal()
        {
            TonConnectBridge.InitModalState((modalState) =>
            {
                if (modalState == null)
                {
                    return;
                }

                Status = modalState.Status;
                CloseReason = modalState.CloseReason;

                OnStateChanged?.Invoke(Status);
            });
        }

        public ModalStatusTypes GetStatus()
        {
            return TonConnectBridge.GetCurrentModalState().Status;
        }
    }
}