namespace UnitonConnect.Core.Common
{
    public sealed class WalletModal: IUnitonConnectModalCallbacks
    {
        public ModalStatusTypes Status { get; private set; }
        public string? CloseReason { get; private set; }

        public event IUnitonConnectModalCallbacks.OnStateChange OnStateChanged;
        public event IUnitonConnectModalCallbacks.OnStateClaim OnStateClaimed;

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

        /// <summary>
        /// Loads the current status of the wallet connection modal window
        /// </summary>
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