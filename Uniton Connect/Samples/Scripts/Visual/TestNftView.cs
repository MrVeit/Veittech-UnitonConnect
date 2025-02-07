using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Runtime.Data;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestNftView : MonoBehaviour
    {
        [SerializeField, Space] private TextMeshProUGUI _headerName;
        [SerializeField, Space] private Image _icon;
        [SerializeField] private Image _selectIcon;
        [SerializeField, Space] private Button _selectButton;

        private NftViewData _nftItem;

        public string Address
        {
            get
            {
                if (string.IsNullOrEmpty(_nftItem.NFTAddress))
                {
                    return null;
                }

                return _nftItem.NFTAddress;
            }
        }

        public string ItemName
        {
            get
            {
                var name = _nftItem.Name;

                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }

                return name;
            }
        }

        public void SetView(NftViewData viewData)
        {
            _nftItem = viewData;

            _headerName.text = _nftItem.Name;

            _selectButton.onClick.AddListener(SelectForTransfer);

            SetIcon(_nftItem.Icon);
        }

        public void SetIcon(Texture2D icon)
        {
            if (icon == null)
            {
                return;
            }

            _icon.sprite = WalletVisualUtils.GetSpriteFromTexture(icon);
        }

        public void Select()
        {
            SetSelectIconVisible(true);
        }

        public void UnSelect()
        {
            SetSelectIconVisible(false);
        }

        private void SelectForTransfer()
        {
            Select();

            Debug.Log($"Start selecting NFT with address: {Address}");

            TestInputViewEvents.NftItemSelected(Address);
        }

        private void SetSelectIconVisible(bool isVisible)
        {
            _selectIcon.gameObject.SetActive(isVisible);
        }
    }
}