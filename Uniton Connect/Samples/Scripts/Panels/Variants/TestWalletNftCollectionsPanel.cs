using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Runtime.Data;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestWalletNftCollectionsPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _interfaceAdapter;
        [SerializeField, Space] private TestNftView _nftPrefab;
        [SerializeField, Space] private TextMeshProUGUI _warningMessage;
        [SerializeField, Space] private GameObject _loadAnimation;
        [SerializeField, Space] private Transform _contentParent;
        [SerializeField] private RectTransform _contentSize;
        [SerializeField, Space] private List<TestNftView> _createdNfts;

        private UnitonConnectSDK _unitonConnect => _interfaceAdapter.UnitonSDK;
        private UserAssets.NFT _nftModule => _interfaceAdapter.NftStorage;

        private float _startSize;

        private bool _isInitialized;

        private readonly float _slotSize = 350f;

        private void OnEnable()
        {
            _unitonConnect.OnWalletDisconnected += RemoveNftCollectionStorage;

            _nftModule.OnNftCollectionsClaimed += NftCollectionsClaimed;
            _nftModule.OnNftCollectionsNotFounded += NftCollectionsNotFounded;
        }

        private void OnDestroy()
        {
            _unitonConnect.OnWalletDisconnected -= RemoveNftCollectionStorage;

            _nftModule.OnNftCollectionsClaimed -= NftCollectionsClaimed;
            _nftModule.OnNftCollectionsNotFounded -= NftCollectionsNotFounded;
        }

        public void Init()
        {
            if (!_unitonConnect.IsWalletConnected)
            {
                return;
            }

            if (_isInitialized)
            {
                return;
            }

            _startSize = _slotSize;

            _nftModule.Load(10, 0);

            SetContentSlotSize(_startSize);

            _loadAnimation.SetActive(true);
            _warningMessage.gameObject.SetActive(false);
        }

        public void RemoveNftCollectionStorage()
        {
            if (_createdNfts.Count == 0)
            {
                return;
            }

            foreach (var nft in _createdNfts)
            {
                Destroy(nft.gameObject);
            }

            _createdNfts.Clear();

            _warningMessage.gameObject.SetActive(false);

            _isInitialized = false;
        }

        private async Task<List<NftViewData>> CreateNftViewContainer(NftCollectionData collections)
        {
            List<NftViewData> nftVisual = new();

            foreach (var nft in collections.Items)
            {
                var iconUrl = nft.Get100x100ResolutionWebp();

                UnitonConnectLogger.Log($"Claimed icon by urL: {iconUrl}");

                var nftIcon = await WalletVisualUtils.GetIconFromProxyServerAsync(iconUrl);
                var nftName = nft.Metadata.ItemName;

                var newNftView = new NftViewData()
                {
                    Icon = nftIcon,
                    Name = nftName
                };

                nftVisual.Add(newNftView);

                UnitonConnectLogger.Log($"Created NFT View with name: {nftName}");
            }

            return nftVisual;
        }

        private void CreateNftItem(List<NftViewData> viewContainer)
        {
            int sizeCount = 0;

            foreach (var nftItem in viewContainer)
            {
                var newNftView = Instantiate(_nftPrefab, _contentParent);

                newNftView.SetView(nftItem);

                _createdNfts.Add(newNftView);

                sizeCount++;

                if (sizeCount >= 2)
                {
                    sizeCount = 0;

                    var slotSize = _startSize + _slotSize;

                    SetContentSlotSize(slotSize);
                }
            }

            _loadAnimation.SetActive(false);

            _isInitialized = true;
        }

        private void SetContentSlotSize(float size)
        {
            _contentSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        }

        private async void NftCollectionsClaimed(NftCollectionData nftCollections)
        {
            if (_createdNfts.Count > 0)
            {
                _loadAnimation.SetActive(false);

                return;
            }

            var viewNftCollections = await CreateNftViewContainer(nftCollections);

            CreateNftItem(viewNftCollections);
        }

        private void NftCollectionsNotFounded()
        {
            _warningMessage.gameObject.SetActive(true);

            _loadAnimation.SetActive(false);
        }

        private bool IsSupportedWebServer()
        {
            string currentURL = Application.absoluteURL;

            if (currentURL.Contains("github"))
            {
                return false;
            }

            return true;
        }
    }
}