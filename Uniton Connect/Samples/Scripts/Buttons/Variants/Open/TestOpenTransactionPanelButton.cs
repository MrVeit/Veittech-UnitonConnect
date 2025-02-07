using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestOpenTransactionPanelButton : TestBaseButton
    {
        [SerializeField, Space] private TestSendTonTransactionPanel _panel;

        public sealed override void OnClick()
        {
            _panel.Init();

            _panel.Open();    
        }
    }
}