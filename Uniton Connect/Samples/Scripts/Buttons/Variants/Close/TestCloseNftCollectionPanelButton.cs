using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestCloseNftCollectionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletNftCollectionsPanel _nftCollectionPanel;

        public sealed override void OnClick()
        {
            _nftCollectionPanel.Close();
        }
    }
}