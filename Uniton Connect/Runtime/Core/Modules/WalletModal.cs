namespace UnitonConnect.Core.Common
{
    public sealed class WalletModal: IUnitonConenctModalCallbacks
    {
        public ModalStatusTypes Status { get; private set; }
        public string? CloseReason { get; private set; }

        public event IUnitonConenctModalCallbacks.OnStateChange OnStateChanged;
        public event IUnitonConenctModalCallbacks.OnStateClaim OnStateClaimed;

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

        public void LoadStatus()
        {
            TonConnectBridge.LoadModalState((state) =>
            {
                Status = state.Status;
                CloseReason = state.CloseReason;

                OnStateClaimed?.Invoke(Status);
            });
        }
    }
}