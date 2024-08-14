using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestOpenNftCollectionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestWalletNftCollectionsPanel _nftCollectionPanel;

        public sealed override void OnClick()
        {
            _nftCollectionPanel.Init();
            _nftCollectionPanel.Open();
        }
    }
}