using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestOpenJettonTransactionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendJettonTransactionPanel _panel;

        public sealed override void OnClick()
        {
            _panel.Init();

            _panel.Open();
        }
    }
}