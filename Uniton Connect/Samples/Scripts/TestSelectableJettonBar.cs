using UnityEngine;
using UnityEngine.UI;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSelectableJettonBar : MonoBehaviour
    {
        [SerializeField, Space] private JettonTypes _type;
        [SerializeField, Space] private Button _selectButton;
        [SerializeField] private Image _currentIcon;
        [SerializeField, Space] private TestSelectedJettonBar _currentJettonBar;

        private TestJettonView _currentConfig;

        public void Init()
        {
            _currentConfig = new TestJettonView()
            {
                Type = _type,
                Icon = _currentIcon.sprite
            };

            _selectButton.onClick.AddListener(Select);
        }

        private void Select()
        {
            _currentJettonBar.ActivateJetton(_currentConfig);
        }
    }

    public sealed class TestJettonView
    {
        public JettonTypes Type { get; set; }
        public Sprite Icon { get; set; }
    }
}