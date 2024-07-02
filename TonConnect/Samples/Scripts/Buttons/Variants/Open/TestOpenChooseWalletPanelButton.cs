using UnityEngine;
using UnityEngine.UI;
using UnitonConnect.Core.Demo;

namespace UnitonConnect.Core.Data
{
    public sealed class TestOpenChooseWalletPanelButton : TestBaseButton
    {
        [SerializeField] private TestChooseWalletPanel _panel;
       
        public sealed override void OnClick()
        {
            _panel.Open();
        }
    }
}