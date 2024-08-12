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
        [SerializeField] private Image _icon;

        private NftItemData _nftItem;

        public void SetView(NftViewData viewData)
        {
            _headerName.text = viewData.Name;
            _icon.sprite = WalletVisualUtils.GetSpriteFromTexture(viewData.Icon);
        }
    }
}