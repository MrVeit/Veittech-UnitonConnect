using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UnitonConnect.Core.Demo
{
    public abstract class TestSetTonAmountButton : MonoBehaviour
    {
        [SerializeField, Space] protected Button _targetButton;
        [SerializeField, Space] private TextMeshProUGUI _amountBar;

        private void Start()
        {
            _targetButton.onClick.AddListener(Init);
        }

        public abstract void Init();

        protected void SetAmount(double amount)
        {
            _amountBar.text = $"{amount} Ton";
        }
    }
}