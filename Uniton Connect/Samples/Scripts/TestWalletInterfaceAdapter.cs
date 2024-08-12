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
        private static readonly object _lock = new();

        private static TestWalletInterfaceAdapter _instance;

        public static TestWalletInterfaceAdapter Instance
        {
            get
            {
                if (_instance)
                {
                    return _instance;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<TestWalletInterfaceAdapter>();
                    }
                }

                return _instance;
            }
        }

        [SerializeField, Space] private UnitonConnectSDK _unitonSDK;
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
        [SerializeField, Space] private List<TestWalletView> _activeWallets;

        public WalletConfig LatestAuthorizedWallet { get; private set; }

        public List<WalletConfig> LoadedWallets { get; set; }

        private string _connectUrl;

        private UserAssets.NFT _nftModule => _unitonSDK.Assets.Nft;

        private void Awake()
        {
            CreateInstance();

            _unitonSDK.OnInitialized += Initialize;

            _unitonSDK.OnWalletConnectionFinished += WalletConnectionFinished;
            _unitonSDK.OnWalletConnectionFailed += WalletConnectionFailed;

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

            _unitonSDK.OnWalletDisconnected += WalletDisconnected;

            _nftModule.OnNftCollectionsClaimed -= NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed -= TargetNftCollectionLoaded;

            if (_activeWallets.Count < 1)
            {
                _unitonSDK.OnWalletDisconnected -= Initialize;
            }
        }

        private void Start()
        {
            UnitonConnectSDK.Instance.Initialize();

            if (!_unitonSDK.IsWalletConnected)
            {
                _disconnectButton.interactable = false;
                _sendTransactionButton.interactable = false;
                _openNftCollectionButton.interactable = false;
            }

            _nftModule.OnNftCollectionsClaimed += NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed += TargetNftCollectionLoaded;
        }

        private void CreateInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = this;

                    DontDestroyOnLoad(gameObject);

                    return;
                }

                UnitonConnectLogger.LogError($"Another instance is detected on the scene, running delete...");

                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            if (_unitonSDK.IsWalletConnected)
            {
                UnitonConnectLogger.Log("SDK is already initialized, " +
                    "there is no need to reconnect the wallet. To reconnect, " +
                    "you need to disconnect the previously connected wallet");

                return;
            }

            _unitonSDK.LoadWalletsConfigs(ProjectStorageConsts.
                TEST_SUPPORTED_WALLETS_LINK, (walletsConfigs) =>
                {
                    CreateWalletsList(walletsConfigs);
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

            foreach (var walletView in walletsViewList)
            {
                var name = walletView.Name;
                var icon = walletView.Icon;

                var walletViewData = Instantiate(_walletViewPrefab, _walletsParent);

                walletViewData.SetView(name, icon, _connectPanel);

                _activeWallets.Add(walletViewData);
            }
        }

        private void WalletConnectionFinished(Wallet wallet)
        {
            if (UnitonConnectSDK.Instance.IsWalletConnected)
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
                    LatestAuthorizedWallet = WalletConnectUtils.GetConfigOfSpecifiedWallet(
                        LoadedWallets, wallet.Device.AppName);

                    UnitonConnectLogger.Log($"The current wallet in the list is detected: " +
                        $"{JsonConvert.SerializeObject(LatestAuthorizedWallet)}");
                }
            }
            else
            {
                _connectButton.interactable = true;
                _disconnectButton.interactable = false;
                _sendTransactionButton.interactable = false;
                _openNftCollectionButton.interactable = false;

                _debugMessage.text = string.Empty;
                _shortWalletAddress.text = string.Empty;

                UnitonConnectLogger.LogWarning($"Connect status: " +
                    $"{UnitonConnectSDK.Instance.IsWalletConnected}");
            }
        }

        private void WalletConnectionFailed(string message)
        {
            UnitonConnectLogger.LogError($"Failed to connect " +
                $"the wallet due to the following reason: {message}");
        }

        private void NftCollectionsLoaded(NftCollectionData collections)
        {
            UnitonConnectLogger.Log($"Loaded nft collections: {collections.Items.Count}");
        }

        private void TargetNftCollectionLoaded(NftItemData nftCollection)
        {
            UnitonConnectLogger.Log($"Loaded target nft collection: {nftCollection.Collection.Name}");
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
    }
}