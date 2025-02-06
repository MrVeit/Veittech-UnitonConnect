using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnitonConnect.Core.Common;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSelectedJettonBar : MonoBehaviour
    {
        [SerializeField, Space] private Button _selectJettonButton;
        [SerializeField, Space] private Image _selectedJettonIcon;
        [SerializeField] private TextMeshProUGUI _jettonNameHeader;
        [SerializeField] private TestAvailableJettonsBar _jettonsBar;
        [SerializeField, Space] private TestWalletAddressBarView _masterAddressBar;

        public JettonTypes CurrentJetton { get; private set; }

        private void Start()
        {
            CurrentJetton = JettonTypes.USDT;

            _selectJettonButton.onClick.AddListener(ShowList);
        }

        public void ActivateJetton(TestJettonView jettonView)
        {
            if (jettonView.Type == JettonTypes.Custom)
            {
                SetViewByType(false, true);

                _masterAddressBar.gameObject.SetActive(true);
            }
            else
            {
                SetViewByType(true, false);

                _selectedJettonIcon.sprite = jettonView.Icon;

                _masterAddressBar.gameObject.SetActive(false);
            }

            CurrentJetton = jettonView.Type;

            HideList();

            Debug.Log($"Selected jetton with type " +
                $"{jettonView.Type} for create transaction");
        }

        private void ShowList()
        {
            _jettonsBar.Open();
        }

        private void HideList()
        {
            _jettonsBar.Close();
        }

        private void SetViewByType(bool withIcon, bool withName)
        {
            _selectedJettonIcon.gameObject.SetActive(withIcon);
            _jettonNameHeader.gameObject.SetActive(withName);
        }
    }
}