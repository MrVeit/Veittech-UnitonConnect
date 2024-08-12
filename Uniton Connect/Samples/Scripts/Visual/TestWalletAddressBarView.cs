using UnityEngine;
using TMPro;
using UnitonConnect.Core.Utils.View;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestWalletAddressBarView : MonoBehaviour
    {
        [SerializeField, Space] private TMP_InputField _addressBar;

        public string ShortAddress { get; private set; }
        public string FullAddress { get; private set; }

        private void OnEnable()
        {
            _addressBar.onValueChanged.AddListener(SetShortAddress);
        }

        private void OnDisable()
        {
            _addressBar.onDeselect.RemoveListener(SetShortAddress);
        }

        public void Set(string address)
        {
            FullAddress = address;

            SetShortAddress(address);
        }

        private void SetShortAddress(string fullAddress)
        {
            ShortAddress = WalletVisualUtils.ProcessWalletAddress(fullAddress, 6);

            _addressBar.text = ShortAddress;
        }
    }
}