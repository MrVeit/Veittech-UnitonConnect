using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestCloseNftTransactionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendNftTransactionPanel _nftTransactionPanel;

        public sealed override void OnClick()
        {
            _nftTransactionPanel.Close();
        }
    }
}