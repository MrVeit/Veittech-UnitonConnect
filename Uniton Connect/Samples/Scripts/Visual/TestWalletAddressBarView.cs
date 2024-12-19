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
            _addressBar.onValueChanged.AddListener(Set);
        }

        private void OnDisable()
        {
            _addressBar.onValueChanged.RemoveListener(Set);
        }

        public void Set(string address)
        {
            Debug.Log($"Recipient address has been changed:" +
                $" {address}, old value: {FullAddress}");

            FullAddress = address;

            _addressBar.text = FullAddress;

            SetShortAddress(address);
        }

        private void SetShortAddress(string fullAddress)
        {
            ShortAddress = WalletVisualUtils.ProcessWalletAddress(fullAddress, 6);
        }
    }
}