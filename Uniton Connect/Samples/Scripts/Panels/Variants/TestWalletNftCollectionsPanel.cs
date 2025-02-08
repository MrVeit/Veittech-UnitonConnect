using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
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
        [SerializeField] private GameObject _loadAnimation;
        [SerializeField, Space] private Transform _contentParent;
        [SerializeField] private RectTransform _contentSize;
        [SerializeField, Space] private TestOpenNftTransactionPanelButton _openNftTransferPanelButton;
        [SerializeField, Space] private List<TestNftView> _createdNfts;

        private UnitonConnectSDK _unitonConnect => _interfaceAdapter.UnitonSDK;
        private UserAssets.NFT _nftStorage => _interfaceAdapter.NftStorage;

        private List<NftItemData> _loadedCollections;

        private bool _isInitialized;

        private void OnEnable()
        {
            _openNftTransferPanelButton.DisableInteractable();

            _unitonConnect.OnWalletDisconnected += RemoveNftCollectionStorage;

            _nftStorage.OnNftCollectionsClaimed += NftCollectionsClaimed;
            _nftStorage.OnTargetNftCollectionClaimed += TargetNftCollectionClaimed;

            _nftStorage.OnNftCollectionsNotFounded += NftCollectionsNotFounded;

            _nftStorage.OnTransactionSended += NftItemSended;

            TestInputViewEvents.OnNftItemSelected += NftItemSelected;
        }

        private void OnDestroy()
        {
            _unitonConnect.OnWalletDisconnected -= RemoveNftCollectionStorage;

            _nftStorage.OnNftCollectionsClaimed -= NftCollectionsClaimed;
            _nftStorage.OnTargetNftCollectionClaimed -= TargetNftCollectionClaimed;

            _nftStorage.OnNftCollectionsNotFounded -= NftCollectionsNotFounded;

            _nftStorage.OnTransactionSended -= NftItemSended;

            TestInputViewEvents.OnNftItemSelected -= NftItemSelected;

            UnSelectNftItems();
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

            _nftStorage.Load(10);

            _loadAnimation.SetActive(true);
            _warningMessage.gameObject.SetActive(false);
        }

        public void RemoveNftCollectionStorage(bool isSuccess)
        {
            if (!isSuccess)
            {
                return;
            }

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

        private async void CreateNftViewContainer(
            NftCollectionData collections, 
            Action<List<NftViewData>> visualCreated)
        {
            List<NftViewData> nftVisual = new();

            _loadedCollections = UserAssetsUtils.GetCachedNftsByScamStatus(true);

            if (_loadedCollections == null)
            {
                Debug.LogWarning("Detected skam NFT items, " +
                    "creating elements in gallery cancelled");

                NftCollectionsNotFounded();

                return;
            }

            foreach (var nft in _loadedCollections)
            {
                var iconUrl = nft.Get500x500ResolutionWebp();

                Debug.Log($"Claimed icon by urL: {iconUrl}");

                Texture2D nftIcon = await WalletVisualUtils.
                    GetWalletIconFromServerAsync(iconUrl);

                var nftName = nft.Metadata.ItemName;

                var newNftView = new NftViewData()
                {
                    Icon = nftIcon,
                    Name = nftName,
                    NFTAddress = nft.Address,
                };

                nftVisual.Add(newNftView);

                Debug.Log($"Created NFT View with name: {nftName}");
            }

            visualCreated?.Invoke(nftVisual);
        }

        private void CreateNftItem(
            List<NftViewData> viewContainer)
        {
            if (_createdNfts.Count > 0)
            {
                return;
            }

            foreach (var nftItem in viewContainer)
            {
                var newNftView = Instantiate(
                    _nftPrefab, _contentParent);

                newNftView.SetView(nftItem);

                _createdNfts.Add(newNftView);
            }

            _loadAnimation.SetActive(false);

            _isInitialized = true;
        }

        private void UnSelectNftItems()
        {
            if (_createdNfts == null ||
                _createdNfts.Count <= 0)
            {
                return;
            }

            foreach (var nftItem in _createdNfts)
            {
                nftItem.UnSelect();
            }
        }

        private bool IsExistNFTs()
        {
            if (_createdNfts.Count > 0)
            {
                return true;
            }

            return false;
        }

        private void NftCollectionsClaimed(
            NftCollectionData nftCollections)
        {
            if (IsExistNFTs())
            {
                _loadAnimation.SetActive(false);

                return;
            }

            List<NftViewData> nftsContainers = null;

            CreateNftViewContainer(nftCollections, (createdViews) =>
            {
                nftsContainers = createdViews;

                if (nftsContainers == null)
                {
                    NftCollectionsNotFounded();

                    return;
                }

                CreateNftItem(nftsContainers);
            });
        }

        private void TargetNftCollectionClaimed(
            NftCollectionData collection)
        {
            NftCollectionsClaimed(collection);
        }

        private void NftCollectionsNotFounded()
        {
            _warningMessage.gameObject.SetActive(true);

            _loadAnimation.SetActive(false);
        }

        private void NftItemSended(string nftItemAddress,
            SuccessTransactionData transactiionData)
        {
            var foundedItem = _createdNfts.FirstOrDefault(
                nftItem => nftItem.Address == nftItemAddress);

            if (foundedItem == null)
            {
                Debug.LogWarning($"Target nft item not " +
                    $"found in gallery: {nftItemAddress}");

                return;
            }

            for (int i = 0; i < _createdNfts.Count; i++)
            {
                if (_createdNfts[i].Address == nftItemAddress)
                {
                    Destroy(_createdNfts[i].gameObject);

                    _createdNfts.RemoveAt(i);
                    _openNftTransferPanelButton.DisableInteractable();

                    if (_createdNfts.Count <= 0)
                    {
                        NftCollectionsNotFounded();
                    }

                    Debug.Log("Cleared NFT from gallery after send new recipient");

                    break;
                }
            }
        }

        private void NftItemSelected(string nftAddress)
        {
            if (_createdNfts == null ||
                _createdNfts.Count <= 0)
            {
                return;
            }

            UnSelectNftItems();

            TestNftView selectedItem = _createdNfts.FirstOrDefault(
                nftItem => nftItem.Address == nftAddress);

            selectedItem.Select();

            _openNftTransferPanelButton.EnableInteractable();
            _openNftTransferPanelButton.Init(_nftStorage,
                selectedItem.ItemName, selectedItem.Address);

            Debug.Log($"Selected NFT item for " +
                $"transfer with address '{selectedItem.Address}'");
        }
    }
}