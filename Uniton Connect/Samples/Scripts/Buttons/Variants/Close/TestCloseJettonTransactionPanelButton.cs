using UnityEngine;
using UnitonConnect.Core.Demo;

namespace UnitonConnect.Core.Data
{
    public sealed class TestCloseJettonTransactionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendJettonTransactionPanel _panel;

        public sealed override void OnClick()
        {
            _panel.Close();
        }
    }
}