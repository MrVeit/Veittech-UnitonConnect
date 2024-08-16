using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using TMPro;
using TonSdk.Core;
using TonSdk.Connect;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.View;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.DeFi;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestWalletInterfaceAdapter : MonoBehaviour
    {
        [SerializeField, Space] private WalletsProvidersData _walletsStorage;
        [SerializeField, Space] private TextMeshProUGUI _debugMessage;
        [SerializeField] private TextMeshProUGUI _shortWalletAddress;
        [SerializeField, Space] private Button _connectButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField] private Button _sendTransactionButton;
        [SerializeField] private Button _openNftCollectionButton;
        [SerializeField, Space] private TestChooseWalletPanel _chooseWalletPanel;
        [SerializeField] private TestSelectedWalletConnectionPanel _connectPanel;
        [SerializeField] private TestWalletNftCollectionsPanel _nftCollectionPanel;
        [SerializeField, Space] private TestWalletView _walletViewPrefab;
        [SerializeField] private Transform _walletsParent;
        [SerializeField] private RectTransform _walletsContentSize;
        [SerializeField, Space] private List<TestWalletView> _activeWallets;

        private string _connectUrl;

        private int _contentSize;

        private UnitonConnectSDK _unitonSDK;
        private UserAssets.NFT _nftModule => _unitonSDK.Assets.Nft;

        public UnitonConnectSDK UnitonSDK => _unitonSDK;
        public UserAssets.NFT NftStorage => _nftModule;

        public WalletConfig LatestAuthorizedWallet { get; private set; }
        public List<WalletConfig> LoadedWallets { get; set; }

        private readonly int _cellSize = 225;

        private void Awake()
        {
            _unitonSDK = UnitonConnectSDK.Instance;

            _unitonSDK.OnInitialized += Initialize;

            _unitonSDK.OnWalletConnectionFinished += WalletConnectionFinished;
            _unitonSDK.OnWalletConnectionFailed += WalletConnectionFailed;
            _unitonSDK.OnWalletConnectionRestored += WalletConnectionRestored;

            _unitonSDK.OnWalletDisconnected += WalletDisconnected;

            if (_activeWallets.Count < 1)
            {
                _unitonSDK.OnWalletDisconnected += Initialize;
            }
        }

        private void OnDestroy()
        {
            _unitonSDK.OnInitialized -= Initialize;

            _unitonSDK.OnWalletConnectionFinished -= WalletConnectionFinished;
            _unitonSDK.OnWalletConnectionFailed -= WalletConnectionFailed;
            _unitonSDK.OnWalletConnectionRestored -= WalletConnectionRestored;

            _unitonSDK.OnWalletDisconnected -= WalletDisconnected;

            _nftModule.OnNftCollectionsClaimed -= NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed -= TargetNftCollectionLoaded;

            if (_activeWallets.Count < 1)
            {
                _unitonSDK.OnWalletDisconnected -= Initialize;
            }
        }

        private void Start()
        {
            _unitonSDK.Initialize();

            _contentSize = _cellSize;

            SetContentSlotSize(_contentSize);

            if (!_unitonSDK.IsWalletConnected)
            {
                _disconnectButton.interactable = false;
                _sendTransactionButton.interactable = false;
                _openNftCollectionButton.interactable = false;
            }

            _nftModule.OnNftCollectionsClaimed += NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed += TargetNftCollectionLoaded;
        }

        private void Initialize()
        {
            LoadWallets((walletsConfig) =>
            {
                LoadedWallets = WalletConnectUtils.GetSupportedWalletsListForUse(walletsConfig);

                if (_unitonSDK.IsWalletConnected)
                {
                    UnitonConnectLogger.Log("SDK is already initialized, " +
                        "there is no need to reconnect the wallet. To reconnect, " +
                        "you need to disconnect the previously connected wallet");

                    return;
                }

                CreateWalletsList(walletsConfig);
            });
        }

        private async void CreateWalletsList(List<WalletConfig> wallets)
        {
            var walletsConfigs = WalletConnectUtils.GetSupportedWalletsListForUse(wallets);

            UnitonConnectLogger.Log($"Created {walletsConfigs.Capacity} wallets");

            LoadedWallets = walletsConfigs;

            UnitonConnectLogger.Log(JsonConvert.SerializeObject(walletsConfigs));

            var walletsViewList = new List<WalletViewData>();

            foreach (var wallet in LoadedWallets)
            {
                WalletViewData walletView = null;

                walletView = await WalletVisualUtils.GetWalletViewIfIconIsNotExist(
                    wallet, _walletsStorage);

                walletsViewList.Add(walletView);
            }

            int sizeCount = 0;

            foreach (var walletView in walletsViewList)
            {
                var name = walletView.Name;
                var icon = walletView.Icon;

                var walletViewData = Instantiate(_walletViewPrefab, _walletsParent);

                walletViewData.SetView(this, name, icon, _connectPanel);

                _activeWallets.Add(walletViewData);

                sizeCount++;

                if (sizeCount >= 3)
                {
                    sizeCount = 0;

                    _contentSize = _cellSize + _contentSize;

                    SetContentSlotSize(_contentSize);
                }
            }
        }

        private void LoadWallets(Action<List<WalletConfig>> walletsLoaded)
        {
            _unitonSDK.LoadWalletsConfigs(ProjectStorageConsts.
                TEST_SUPPORTED_WALLETS_LINK, (walletsConfigs) =>
                {
                    walletsLoaded?.Invoke(walletsConfigs);
                });
        }

        private void SetContentSlotSize(float size)
        {
            _walletsContentSize.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        }

        private WalletConfig GetWalletConfigByName(Wallet wallet)
        {
            var loadedConfig = WalletConnectUtils.GetConfigOfSpecifiedWallet(
                LoadedWallets, wallet.Device.AppName);

            return loadedConfig;
        }

        private void WalletConnectionFinished(Wallet wallet)
        {
            if (_unitonSDK.IsWalletConnected)
            {
                var successConnectMessage = $"Wallet is connected, full account address: {wallet.Account.Address}, \n" +
                $"Platform: {wallet.Device.Platform}, " +
                $"Name: {wallet.Device.AppName}, " +
                $"Version: {wallet.Device.AppVersion}";

                var userAddress = $"{wallet.Account.Address}";

                var shortWalletAddress = WalletVisualUtils.ProcessWalletAddress(
                    wallet.Account.Address.ToString(AddressType.Base64), 6);

                _debugMessage.text = successConnectMessage;
                _shortWalletAddress.text = shortWalletAddress;

                UnitonConnectLogger.Log($"Connected wallet short address: {shortWalletAddress}");

                _connectButton.interactable = false;
                _disconnectButton.interactable = true;
                _sendTransactionButton.interactable = true;
                _openNftCollectionButton.interactable = true;

                _chooseWalletPanel.Close();

                if (LoadedWallets != null)
                {
                    LatestAuthorizedWallet = GetWalletConfigByName(wallet);

                    UnitonConnectLogger.Log($"The current wallet in the list is detected: " +
                        $"{JsonConvert.SerializeObject(LatestAuthorizedWallet)}");
                }

                return;
            }

            _connectButton.interactable = true;
            _disconnectButton.interactable = false;
            _sendTransactionButton.interactable = false;
            _openNftCollectionButton.interactable = false;

            _debugMessage.text = string.Empty;
            _shortWalletAddress.text = string.Empty;

            UnitonConnectLogger.LogWarning($"Connect status: " +
                $"{_unitonSDK.IsWalletConnected}");
        }

        private void WalletConnectionFailed(string message)
        {
            UnitonConnectLogger.LogError($"Failed to connect " +
                $"the wallet due to the following reason: {message}");
        }

        private void WalletConnectionRestored(bool isRestored, WalletConfig wallet)
        {
            if (isRestored)
            {
                LatestAuthorizedWallet = wallet;

                UnitonConnectLogger.Log($"Connection to previously connected wallet {wallet.Name} restored," +
                    $" local configuration updated");
            }
        }

        private void WalletDisconnected()
        {
            _nftCollectionPanel.RemoveNftCollectionStorage();

            if (_activeWallets.Count == 0)
            {
                return;
            }

            foreach (var wallet in _activeWallets)
            {
                Destroy(wallet.gameObject);
            }

            _activeWallets.Clear();
        }

        private void NftCollectionsLoaded(NftCollectionData collections)
        {
            UnitonConnectLogger.Log($"Loaded nft collections: {collections.Items.Count}");
        }

        private void TargetNftCollectionLoaded(NftCollectionData nftCollection)
        {
            UnitonConnectLogger.Log($"Loaded target nft collection with name: {nftCollection.Items[0].Collection.Name}");
        }
    }
}