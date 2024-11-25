using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TonSdk.Core;
using TonSdk.Connect;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Data.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.DeFi;
using UnitonConnect.ThirdParty.TonAPI;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.Core
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/MrVeit/Veittech-UnitonConnect")]
    public sealed class UnitonConnectSDK : MonoBehaviour, IUnitonConnectSDKCallbacks,
        IUnitonConnectWalletCallbacks, IUnitonConnectTransactionCallbacks
    {
        private static readonly object _lock = new();

        private static UnitonConnectSDK _instance;

        public static UnitonConnectSDK Instance
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
                        _instance = FindObjectOfType<UnitonConnectSDK>();
                    }
                }

                return _instance;
            }
        }

        [Header("SDK Settings"), Space]
        [Tooltip("Additional flag to initialize a specific version of sdk, to avoid problems when used on Unity versions 2023 and higher")]
        [SerializeField] private SdkTypes _version;
        [Tooltip("Enable if you want to test the SDK without having to upload data about your dApp")]
        [SerializeField, Space] private bool _testMode;
        [Tooltip("Enable if you want to activate SDK logging for detailed analysis before releasing a dApp")]
        [SerializeField] private bool _debugMode;
        [Tooltip("Turn it off if you want to do your own cdk initialization in your scripts")]
        [SerializeField, Space] private bool _initializeOnAwake;
        [Header("Ton Connect Settings"), Space]
        [SerializeField, Space] private WalletsListData _walletsListConfig;
        [Tooltip("Enable if you want to instantly receive wallet icons in your interface without waiting for them to be downloaded from the server")]
        [SerializeField, Space] private bool _useCachedWalletsIcons;
        [Tooltip("Configuration of supported wallets for your dApp. You can change their order and number, and override the way their configurations are loaded by hosting it yourself")]
        [SerializeField, Space] private WalletsProvidersData _supportedWallets;
        [Tooltip("Delay before requesting in blockchain to retrieve data about the sent transaction")]
        [SerializeField, Space, Range(15f, 500f)] private float _confirmTransactionDelay;

        private NewWalletConfig _connectedWalletConfig;

        private TonConnect _tonConnect;
        private TonConnectOptions _tonConnectOptions;

        private AdditionalConnectOptions _additionalConnectOptions;
        private RemoteStorage _remoteStorage;

        private bool _isInitialized;
        private bool _isWalletConnected;

        private bool _restoreConnectionOnAwake = true;

        public UserWallet Wallet { get; private set; }
        public UserAssets Assets { get; private set; }

        public decimal TonBalance { get; private set; }

        public TonConnect TonConnect => _tonConnect;
        public NewWalletConfig ConnectedWalletConfig => _connectedWalletConfig;

        public List<WalletProviderConfig> SupportedWallets => _supportedWallets.Config;

        public bool IsInitialized => _isInitialized;
        public bool IsTestMode => _testMode;
        public bool IsDebugMode => _debugMode;
        public bool IsWalletConnected => _isWalletConnected;

        public bool IsUseWebWallets => false;

        public bool IsUseCachedWalletsIcons => _useCachedWalletsIcons;
        public bool IsActiveRestoreConnection => _restoreConnectionOnAwake;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use `OnNativeInitialized(bool isSuccess)` to track the status of the native initiation")]
        /// <summary>
        /// Callback if sdk initialization is successful
        /// </summary>
        public event IUnitonConnectSDKCallbacks.OnUnitonConnectInitialize OnInitialized;

        /// <summary>
        /// Callback if native sdk initialization finished with same result
        /// </summary>
        public event IUnitonConnectSDKCallbacks.OnNativeUnitonConnectInitialize OnNativeInitialized;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use `OnNativeWalletConnectionFinished` to track the status of a native wallet connection")]
        /// <summary>
        /// Callback in case of successful initialization of sdk and loading of wallet configurations for further connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectionFinish OnWalletConnectionFinished;

        /// <summary>
        /// Callback in case of successful native wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletConnectionFinish OnNativeWalletConnectionFinished;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use `OnNativeWalletConnectionFailed` to handle native wallet connection errors")]
        /// <summary>
        /// Callback for error handling, in case of unsuccessful loading of wallet configurations
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectionFail OnWalletConnectionFailed;

        /// <summary>
        /// Callback for error handling, in case of unsuccessful wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletConnectionFail OnNativeWalletConnectionFailed;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use `OnNativeWalletConnectionRestored` to get the connection restore status of a native wallet connection")]
        /// <summary>
        /// Callback for processing the status of restored connection to the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectionRestore OnWalletConnectionRestored;

        /// <summary>
        /// Callback for processing the status of restored native connection to the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletConnectionRestore OnNativeWalletConnectionRestored;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "this callback will no longer be used, as the native sdk handles all of this")]
        /// <summary>
        /// Callback to handle the status of pausing the connection to the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectionPause OnWalletConnectionPaused;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "this callback will no longer be used, as the native sdk handles all of this")]
        /// <summary>
        /// Callback to handle the shutdown status of a previously activated connection pause
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectionUnPause OnWalletConnectonUnPaused;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use `OnNativeWalletDisconnected` to get the disconnected status of the native wallet")]
        /// <summary>
        /// Callback to handle wallet connection disconnection status
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletDisconnect OnWalletDisconnected;

        /// <summary>
        /// Callback to handle native wallet connection disconnection status
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletDisconnect OnNativeWalletDisconnected;

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use `OnNativeSendingTonFinished` to process transactions with toncoin and get the hash.")]
        /// <summary>
        /// Callback to process the status of a sent toncoin transaction and retrieve its Boc
        /// </summary>
        public event IUnitonConnectTransactionCallbacks.OnTransactionSendingFinish OnSendingTonFinished;

        /// <summary>
        ///  Callback to process the status of a sent toncoin transaction and retrieve its hash
        /// </summary>
        public event IUnitonConnectTransactionCallbacks.OnNativeTransactionSendingFinish OnNativeSendingTonFinished;

        /// <summary>
        /// Callback to handle failed sending of a transaction with toncoin
        /// </summary>
        public event IUnitonConnectTransactionCallbacks.OnNativeTransactionSendingFail OnNativeTransactionSendingFailed;

        /// <summary>
        /// Callback to retrieve transaction information from the blockchain after the transaction has been successfully sent
        /// </summary>
        public event IUnitonConnectTransactionCallbacks.OnNativeTransactionConfirm OnNativeTransactionConfirmed;

        /// <summary>
        /// Callback to get the current amount of toncoin on the wallet
        /// </summary>
        public event IUnitonConnectTransactionCallbacks.OnTonBalanceClaim OnTonBalanceClaimed;

        private void Awake()
        {
            CreateInstance();

            if (!_initializeOnAwake)
            {
                return;
            }

            Initialize();
        }

        private void OnDestroy()
        {
            if (IsSupporedPlatform())
            {
                TonConnectBridge.UnSubscribe();
            }
        }

        /// <summary>
        /// Initialization of the Uniton Connect sdk if you want to do it manually.
        /// </summary>
        public void Initialize()
        {
            var dAppManifestLink = string.Empty;
            var dAppConfig = ProjectStorageConsts.GetRuntimeAppStorage();

            dAppManifestLink = WebRequestUtils.GetAppManifestLink(_testMode, dAppConfig);

            if (string.IsNullOrEmpty(dAppManifestLink))
            {
                UnitonConnectLogger.LogError("Failed to initialize Uniton Connect SDK due" +
                    " to missing configuration of your dApp. \r\nIf you want to test the operation of" +
                    " the SDK without integrating your project, activate test mode.");

                return;
            }

            Assets = new UserAssets(this, this);

            if (_version == SdkTypes.Old)
            {
                _tonConnectOptions = GetOptions(dAppManifestLink);
                _remoteStorage = GetRemoteStorage();
                _additionalConnectOptions = GetAdditionalConnectOptions();

                _tonConnect = GetTonConnectInstance(_tonConnectOptions,
                    _remoteStorage, _additionalConnectOptions);

                _tonConnect.OnStatusChange(OnWalletConnectionFinish, OnWalletConnectionFail);

                RestoreConnectionAsync(_remoteStorage);

                OnInitialize();

                _isInitialized = true;

                UnitonConnectLogger.Log("Old SDK successfully initialized");

                return;
            }

            if (IsSupporedPlatform())
            {
                TonConnectBridge.Init(dAppManifestLink,
                    OnInitialize, OnNativeWalletConnectionFinish, 
                    OnNativeWalletConnectionFail, OnWalletConnectionRestore);
            }

            _isInitialized = true;

            UnitonConnectLogger.Log("Native SDK successfully initialized");
        }

        /// <summary>
        /// Opens the native sdk connection window to connect to the selected wallet in TMA or browser
        /// </summary>
        public void Connect()
        {
            if (!IsSupporedPlatform())
            {
                return;
            }

            if (!_isInitialized)
            {
                UnitonConnectLogger.LogWarning("Sdk is not initialized, try again later");

                return;
            }

            if (_isWalletConnected)
            {
                UnitonConnectLogger.LogWarning("Wallet has been previously connected");

                return;
            }

            TonConnectBridge.Connect(OnNativeWalletConnectionFinish, OnNativeWalletConnectionFail);
        }

        /// <summary>
        /// Disable connection to a previously connected native sdk wallet
        /// </summary>
        public void Disconnect()
        {
            if (!IsSupporedPlatform())
            {
                return;
            }

            if (!_isInitialized)
            {
                UnitonConnectLogger.LogWarning("Sdk is not initialized, try again later");

                return;
            }

            if (!_isWalletConnected)
            {
                UnitonConnectLogger.LogError("No connected wallets are detected for disconnect");

                return;
            }

            TonConnectBridge.Disconnect(OnNativeWalletDisconnect);
        }

        /// <summary>
        /// Loading balance of a particular token on a connected wallet, if it exists there.
        /// </summary>
        /// <param name="tokenType"></param>
        public void LoadBalance(ClassicTokenTypes tokenType)
        {
            StartCoroutine(TonApiBridge.GetBalance((nanotonBalance) =>
            {
                var tonBalance = UserAssetsUtils.FromNanoton(nanotonBalance);

                TonBalance = tonBalance;

                OnTonBalanceClaimed?.Invoke(TonBalance);

                UnitonConnectLogger.Log($"Current TON balance: {TonBalance}");
            }));
        }

        /// <summary>
        /// Send toncoin to the specified recipient address
        /// </summary>
        /// <param name=“recipientAddress”>Token recipient address</param>
        /// <param name=“amount”>Number of tokens to send</param>
        /// <param name=“message”>Useful payload (comment, item id, etc.)</param>
        public void SendTransaction(ClassicTokenTypes tokenType,
            string recipientAddress, decimal amount, string message = null)
        {
            if (!IsSupporedPlatform())
            {
                return;
            }

            if (!_isInitialized)
            {
                UnitonConnectLogger.LogWarning("Sdk is not initialized, try again later");

                return;
            }

            if (!_isWalletConnected)
            {
                UnitonConnectLogger.LogWarning("Wallet is not connected, do so and try again later");

                return;
            }

            if (WalletConnectUtils.IsAddressesMatch(recipientAddress))
            {
                UnitonConnectLogger.LogWarning("Transaction canceled because the recipient and sender addresses match");

                return;
            }

            UnitonConnectLogger.Log($"Created a request to send a TON" +
                    $" to the recipient: {recipientAddress} in amount {amount}");

            TonConnectBridge.SendTon(recipientAddress,
                amount, message, OnSendingTonFinish, OnSendingTonFail);
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "this method will no longer be used, as the native sdk handles all of this")]
        /// <summary>
        /// Set the connection event listener to pause
        /// </summary>
        public void PauseConnection()
        {
            _tonConnect.PauseConnection();

            OnWalletConnectionPause();
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "this method will no longer be used, as the native sdk handles all of this")]
        /// <summary>
        /// Switching off the activated pause for the connection event listener
        /// </summary>
        public void UnPauseConnection()
        {
            _tonConnect.UnPauseConnection();

            OnWalletConnectionUnPause();
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `Connect()` method to connect a wallet via the native window")]
        /// <summary>
        /// Connecting to an HTTP bridged wallet via deep links
        /// </summary>
        /// <param name="wallet">Wallet configuration for connection</param>
        /// <param name="connectUrl">Link to connect to the wallet</param>
        public void ConnectHttpBridgeWalletViaDeepLink(
            WalletConfig wallet, string connectUrl)
        {
            if (!WalletConnectUtils.HasHttpBridge(wallet))
            {
                UnitonConnectLogger.LogWarning("The specified wallet configuration has no HTTP bridge detected," +
                    " the deeplink connection was terminated.");

                return;
            }

            OpenWalletViaDeepLink(Uri.EscapeUriString(connectUrl));
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `Connect()` method to connect a wallet via the native window")]
        /// <summary>
        /// Connection to a JavaScript bridged wallet via deep links
        /// </summary>
        /// <param name="wallet">Wallet configuration for connection</param>
        public async Task ConnectJavaScriptBridgeWalletViaDeeplink(WalletConfig wallet)
        {
            if (!WalletConnectUtils.HasJSBridge(wallet))
            {
                UnitonConnectLogger.LogWarning("The specified wallet configuration has no JavaScript bridge detected," +
                    " the deeplink connection was terminated.");

                return;
            }

            await GenerateConnectURL(wallet);
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `Connect()` method to connect a wallet via the native window")]
        /// <summary>
        /// Get a link to connect to the wallet via the specified config
        /// </summary>
        /// <param name="wallet">Wallet configuration to connect</param>.
        public async Task<string> GenerateConnectURL(WalletConfig wallet)
        {
            var connectUrl = string.Empty;

            try
            {
                connectUrl = await _tonConnect.Connect(wallet);
            }
            catch (WalletAlreadyConnectedError error)
            {
                UnitonConnectLogger.LogError($"Error: {error.Message}");
            }
            catch (Exception exceoption)
            {
                UnitonConnectLogger.LogError($"Failed to connect to the wallet due to " +
                    $"the following reason: {exceoption.Message}");
            }

            return connectUrl;
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `Disconnect()` method to disconnect a previously connected wallet")]
        /// <summary>
        /// Disconnect of previously connected wallet
        /// </summary>
        public async Task DisconnectWallet()
        {
            try
            {
                if (!IsWalletConnected)
                {
                    UnitonConnectLogger.LogError("No connected wallets are detected for disconnection");

                    return;
                }

                await _tonConnect.Disconnect();
            }
            catch (TonConnectError error)
            {
                UnitonConnectLogger.LogError($"Error: {error.Message}");
            }
            catch (Exception exception)
            {
                UnitonConnectLogger.LogError($"The previously connected wallet could not be " +
                    $"disconnected due to the following reason: {exception.Message}");
            }
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `SendTransaction()` method to send transactions with different tokens")]
        /// <summary>
        /// Send toncoin to the specified recipient address
        /// </summary>
        /// <param name="currentWallet">Current authorized wallet for the transaction</param>
        /// <param name="recipientAddress">Token recipient address</param>
        /// <param name="amount">Number of tokens to send</param>
        public async Task SendTon(WalletConfig currentWallet,
            string recipientAddress, double amount)
        {
            var transactionMessages = GetTransactionMessages(recipientAddress, amount);
            var transactionRequest = GetTransactionRequest(transactionMessages);

            SendTransactionResult? transactionResult = null;

            try
            {
                UnitonConnectLogger.Log($"Created a request to send a TON" +
                    $" to the recipient: {recipientAddress} in amount {amount}");

                if (WalletConnectUtils.IsAddressesMatch(recipientAddress))
                {
                    UnitonConnectLogger.LogWarning("Transaction canceled because the recipient and sender addresses match");

                    return;
                }

                OpenWalletViaDeepLink(currentWallet.UniversalUrl);

                transactionResult = await _tonConnect.SendTransaction(transactionRequest);

                if (transactionResult.HasValue)
                {
                    OnSendingTonFinish(transactionResult, true);

                    UnitonConnectLogger.Log($"Transaction successfully sended" +
                        $", Boc: {transactionResult.Value.Boc}");
                }
            }
            catch (WalletNotConnectedError connectionError)
            {
                UnitonConnectLogger.LogError($"{connectionError.Message}");

                OnSendingTonFinish(transactionResult, false);
            }
            catch (Exception exception)
            {
                UnitonConnectLogger.LogError($"Failed to send tokens due" +
                    $" to the following reason: {exception.Message}");

                OnSendingTonFinish(transactionResult, false);
            }
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `LoadBalance(ClassicTokenTypes tokenType)` method to get the balance of a particular token on a connected wallet")]
        /// <summary>
        /// Loading TON balance of a recently connected wallet. Subscribe to `OnTonBalanceClaimed` event to get the result.
        /// </summary>
        public void UpdateTonBalance()
        {
            StartCoroutine(TonApiBridge.GetBalance((nanotonBalance) =>
            {
                var tonBalance = UserAssetsUtils.FromNanoton(nanotonBalance);

                TonBalance = tonBalance;

                OnTonBalanceClaimed?.Invoke(tonBalance);

                UnitonConnectLogger.Log($"Current TON balance: {TonBalance}");
            }));
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use UnitonConnectSDK.Instance.Wallet.ToString() to get the address of the connected wallet")]
        /// <summary>
        /// Getting the address of the recently connected wallet
        /// </summary>
        /// <returns></returns>
        public string GetWalletAddress()
        {
            if (!IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning("Wallet is not connected");

                return string.Empty;
            }

            return $"{TonConnect.Wallet.Account.Address}";
        }

        [Obsolete("Starting with version 0.2.9.5 and above, " +
            "use the `Connect()` method to connect a wallet via the native window")]
        /// <summary>
        /// Start downloading wallet configurations by the specified link to the json file
        /// </summary>
        /// <param name="supportedWalletsUrl">Link to a list of all supported wallets to get their configurations. Use ProjectStorageConsts.TEST_SUPPORTED_WALLETS_LINK to get the whole list of available wallets, or bind your manifest</param>
        /// <param name="walletsClaimed">Callback to retrieve successfully downloaded wallet configurations</param>
        public void LoadWalletsConfigs(string supportedWalletsUrl,
            Action<List<WalletConfig>> walletsClaimed)
        {
            StartCoroutine(LoadWallets(supportedWalletsUrl, walletsClaimed));
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

                if (_instance != null)
                {
                    UnitonConnectLogger.LogWarning($"Another instance is detected on the scene, running delete...");

                    Destroy(gameObject);
                }
            }
        }

        private async void RestoreConnectionAsync(RemoteStorage storage)
        {
            if (!_restoreConnectionOnAwake)
            {
                ResetConnectionStorage(storage);

                return;
            }

            bool isSuccess = await _tonConnect.RestoreConnection();

            if (isSuccess)
            {
                string walletName = _tonConnect.Wallet.Device.AppName;

                LoadWalletsConfigs(ProjectStorageConsts.DEFAULT_SUPPORTED_WALLETS_LINK,
                (configs) =>
                {
                    var updatedConfigs = WalletConnectUtils.GetSupportedWalletsListForUse(configs);
                    var walletConfig = WalletConnectUtils.GetConfigOfSpecifiedWallet(updatedConfigs, walletName);

                    OnWalletConnectionRestore(isSuccess, walletConfig);

                    UnitonConnectLogger.Log($"Connection restored with status: {isSuccess}");
                });

                return;
            }

            OnWalletConnectionRestore(isSuccess, new WalletConfig());
        }

        private IEnumerator ConfirmTransactionFromBlockchain(string transactionHash)
        {
            bool isFailed = false;

            if (isFailed)
            {
                yield return new WaitForSeconds(5f);
            }

            StartCoroutine(TonApiBridge.GetTransactionData(transactionHash,
                _confirmTransactionDelay, (transactionData) =>
            {
                var fee = UserAssetsUtils.FromNanoton(transactionData.TotalFees).ToString();
                var updatedBalance = UserAssetsUtils.FromNanoton(transactionData.EndBalance).ToString();
                var sendedAmount = UserAssetsUtils.FromNanoton(transactionData.OutMessages[0].Value).ToString();

                isFailed = false;

                OnSendingTonConfirm(transactionData);

                UnitonConnectLogger.Log($"Transaction {transactionHash} confirmed, " +
                    $"fee: {fee}, updated balance: {updatedBalance}, sended amount: {sendedAmount}");
            },
            (errorMessage) =>
            {
                UnitonConnectLogger.LogError($"Failed to fetch transaction data, reason: {errorMessage}");

                if (errorMessage == "entity not found")
                {
                    isFailed = true;

                    return;
                }

                OnSendingTonConfirm(null);
            }));
        }

        private IEnumerator LoadWallets(string supportedWalletsUrl,
            Action<List<WalletConfig>> walletsClaimed)
        {
            UnityWebRequest request = UnityWebRequest.Get(supportedWalletsUrl);

            yield return request.SendWebRequest();

            if (request.result != WebRequestUtils.SUCCESS)
            {
                UnitonConnectLogger.LogError($"HTTP Error with message: {request.error}");

                yield break;
            }
            else
            {
                var responseResult = request.downloadHandler.text;
                var walletsList = JsonConvert.DeserializeObject<List<WalletProviderData>>(responseResult);

                UnitonConnectLogger.Log($"Wallet list config after load: {JsonConvert.SerializeObject(walletsList)}");

                ParseWalletsConfigs(ref walletsList, walletsClaimed);
            }

            request.Dispose();
        }

        private IEnumerator ActivateInitializationSDKListenerRoutine(
            EventListenerArgumentsData listenerData)
        {
            var url = listenerData.Url;

            var acceptHeader = WebRequestUtils.HEADER_ACCEPT;
            var textEventStreamValue = WebRequestUtils.HEADER_VALUE_TEXT_EVENT_STREAM;

            UnityWebRequest request = new(listenerData.Url, UnityWebRequest.kHttpVerbGET) { };

            WebRequestUtils.SetRequestHeader(request, acceptHeader, textEventStreamValue);

            DownloadHandlerBuffer handlerBuff = new();
            request.downloadHandler = handlerBuff;

            AsyncOperation operation = request.SendWebRequest();

            int currentPosition = 0;

            while (!listenerData.Token.IsCancellationRequested && !operation.isDone)
            {
                if (request.result == WebRequestUtils.CONNECTION_ERROR ||
                        request.result == WebRequestUtils.PROTOCOL_ERROR)
                {
                    listenerData.ErrorProvider(
                        new Exception($"SSE request error: {request.error}"));

                    UnitonConnectLogger.LogError($"Failed to activate event listener with error: {request.error}");

                    break;
                }

                string result = request.downloadHandler.text.Substring(currentPosition);
                string[] lines = result.Split('\n');

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        UnitonConnectLogger.Log(line);

                        listenerData.Provider(line);
                    }
                }

                currentPosition += result.Length;

                yield return null;
            }

            request.Dispose();
        }

        private IEnumerator ActivateGatewaySenderRoutine(GatewayMessageData gatewayMessage)
        {
            var url = WebRequestUtils.GetGatewaySenderLink(gatewayMessage);

            var contentTypeHeader = WebRequestUtils.HEADER_CONTENT_TYPE;
            var textPlainValue = WebRequestUtils.HEADER_VALUE_TEXT_PLAIN;

            UnityWebRequest request = new(url, UnityWebRequest.kHttpVerbPOST)
            {
                uploadHandler = new UploadHandlerRaw(gatewayMessage.Message)
            };

            WebRequestUtils.SetRequestHeader(request, contentTypeHeader, textPlainValue);

            yield return request.SendWebRequest();

            if (request.result != WebRequestUtils.SUCCESS)
            {
                UnitonConnectLogger.LogError($"Failed to send Gateway message with error:" +
                    $" {request.error}, response code: {request.responseCode}");
            }
            else
            {
                UnitonConnectLogger.Log("Gateway message successfully sended");
            }

            yield return null;

            request.Dispose();
        }

        private void ActivateInitializationSDKListener(CancellationToken token,
            string url, ProviderMessageHandler initializationSuccess, ProviderErrorHandler initializationFailed)
        {
            var listenerData = new EventListenerArgumentsData()
            {
                Token = token,
                Url = url,
                Provider = initializationSuccess,
                ErrorProvider = initializationFailed
            };

            StartCoroutine(ActivateInitializationSDKListenerRoutine(listenerData));
        }

        private void ActivateGatewaySender(string bridgeUrl, string postPath,
            string sessionId, string receiver, int timeToLive, string topic, byte[] message)
        {
            var gatewayMessage = new GatewayMessageData()
            {
                BridgeUrl = bridgeUrl,
                PostPath = postPath,
                SessionId = sessionId,
                Receiver = receiver,
                TimeToLive = timeToLive,
                Topic = topic,
                Message = message
            };

            StartCoroutine(ActivateGatewaySenderRoutine(gatewayMessage));
        }

        private void ParseWalletsConfigs(ref List<WalletProviderData> walletsList,
            Action<List<WalletConfig>> walletsClaimed)
        {
            var loadedWallets = new List<WalletConfig>();

            foreach (var wallet in walletsList)
            {
                WalletConfig walletConfig = new()
                {
                    Name = wallet.Name,
                    Image = wallet.Image,
                    AboutUrl = wallet.AboutUrl,
                    AppName = wallet.AppName
                };

                foreach (var bridge in wallet.Bridge)
                {
                    if (bridge.Type == WalletConfigComponents.SSE)
                    {
                        walletConfig.BridgeUrl = bridge.Url;
                        walletConfig.UniversalUrl = wallet.UniversalUrl;
                        walletConfig.JsBridgeKey = null;

                        loadedWallets.Add(walletConfig);
                    }
                    else if (bridge.Type == WalletConfigComponents.JAVA_SCRIPT)
                    {
                        walletConfig.JsBridgeKey = bridge.Key;
                        walletConfig.BridgeUrl = null;

                        loadedWallets.Add(walletConfig);
                    }
                }
            }

            walletsClaimed(loadedWallets);
        }

        private void ResetConnectionStorage(RemoteStorage storage)
        {
            var keyConnection = RemoteStorage.KEY_CONNECTION;
            var lastEventId = RemoteStorage.KEY_LAST_EVENT_ID;

            storage.RemoveItem(keyConnection);
            storage.RemoveItem(lastEventId);

            ProjectStorageConsts.DeleteConnectionKey(keyConnection);
            ProjectStorageConsts.DeleteConnectionKey(lastEventId);

            OnWalletConnectionRestore(false, new WalletConfig());
        }

        private void OpenWalletViaDeepLink(string deepLinkURL)
        {
            Application.OpenURL(deepLinkURL);
        }

        private TonConnect GetTonConnectInstance(TonConnectOptions options,
            RemoteStorage storage, AdditionalConnectOptions connectOptions)
        {
            return new TonConnect(options, storage, connectOptions);
        }

        private TonConnectOptions GetOptions(string manifestLink)
        {
            TonConnectOptions options = new()
            {
                ManifestUrl = manifestLink,

                WalletsListSource = _walletsListConfig.SourceLink,
                WalletsListCacheTTLMs = _walletsListConfig.CachedTimeToLive
            };

            return options;
        }

        private RemoteStorage GetRemoteStorage()
        {
            return new RemoteStorage(new(PlayerPrefs.GetString), new(PlayerPrefs.SetString),
                new(PlayerPrefs.DeleteKey), new(PlayerPrefs.HasKey));
        }

        private AdditionalConnectOptions GetAdditionalConnectOptions()
        {
            AdditionalConnectOptions connectOptions = new()
            {
                listenEventsFunction = new ListenEventsFunction(ActivateInitializationSDKListener),
                sendGatewayMessage = new SendGatewayMessage(ActivateGatewaySender)
            };

            return connectOptions;
        }

        private SendTransactionRequest GetTransactionRequest(Message[] messages)
        {
            long valudUntil = DateTimeOffset.Now.ToUnixTimeSeconds() + 600;

            return new SendTransactionRequest(messages, valudUntil);
        }

        private Message[] GetTransactionMessages(string receiverAddress,
            double amount)
        {
            Address receiver = new(receiverAddress);
            Coins tokensAmount = new(amount);

            Message[] messages =
            {
                new(receiver, tokensAmount)
            };

            return messages;
        }

        private bool IsSupporedPlatform()
        {
#if UNITY_EDITOR || !UNITY_WEBGL
            UnitonConnectLogger.LogWarning("Unsupported platform detected, " +
                "please build the project using WebGL and test the options");

            return false;
#endif

            return true;
        }

        private void OnInitialize()
        {
            OnInitialized?.Invoke();
        }

        private void OnInitialize(bool isSuccess)
        {
            OnNativeInitialized?.Invoke(isSuccess);
        }

        private void OnWalletConnectionFinish(Wallet wallet)
        {
            if (!IsWalletConnected)
            {
                ResetConnectionStorage(_remoteStorage);

                OnWalletDisconnect();

                UnitonConnectLogger.Log("Connection to the wallet has been successfully disconnected," +
                    " the storage of the previous session has been cleaned up");
            }

            _isWalletConnected = _tonConnect.IsConnected;

            Wallet = new UserWallet(wallet.Account.Address.ToString(), null);

            OnWalletConnectionFinished?.Invoke(wallet);
        }

        private void OnNativeWalletConnectionFinish(NewWalletConfig walletConfig)
        {
            _isWalletConnected = true;

            _connectedWalletConfig = walletConfig;

            var nonBouceableAddress = WalletConnectUtils
                .GetNonBounceableAddress(walletConfig.Address);

            var updatedConfig = new NewWalletConfig()
            {
                Address = nonBouceableAddress,
                Chain = walletConfig.Chain,
                PublicKey = walletConfig.PublicKey,
                StateInit = walletConfig.StateInit,
            };

            Wallet = new UserWallet(nonBouceableAddress, updatedConfig);

            OnNativeWalletConnectionFinished?.Invoke(updatedConfig);
        }

        private void OnWalletConnectionFail(string errorMessage)
        {
            _isWalletConnected = false;

            OnWalletConnectionFailed?.Invoke(errorMessage);
        }

        private void OnNativeWalletConnectionFail(string errorMessage)
        {
            _isWalletConnected = false;

            OnNativeWalletConnectionFailed?.Invoke(errorMessage);
        }

        private void OnWalletConnectionRestore(bool isRestored, WalletConfig restoredWallet = new())
        {
            if (isRestored)
            {
                _isWalletConnected = true;
            }

            OnWalletConnectionRestored?.Invoke(isRestored, restoredWallet);
        }

        private void OnWalletConnectionRestore(bool isRestored)
        {
            if (isRestored)
            {
                _isWalletConnected = true;
            }

            OnNativeWalletConnectionRestored?.Invoke(isRestored);
        }

        private void OnWalletConnectionPause() => OnWalletConnectionPaused?.Invoke();

        private void OnWalletConnectionUnPause() => OnWalletConnectonUnPaused?.Invoke();

        private void OnWalletDisconnect()
        {
            _isWalletConnected = false;

            OnWalletDisconnected?.Invoke();
        }

        private void OnNativeWalletDisconnect(bool isSuccess)
        {
            _isWalletConnected = false;

            Wallet = new UserWallet(null, null);

            OnNativeWalletDisconnected?.Invoke(isSuccess);
        }

        private void OnSendingTonFinish(SendTransactionResult? transactionResult,
            bool isSuccess)
        {
            OnSendingTonFinished?.Invoke(transactionResult, isSuccess);

            if (isSuccess)
            {
                UpdateTonBalance();
            }
        }

        private void OnSendingTonFinish(string transactionHash)
        {
            OnNativeSendingTonFinished?.Invoke(transactionHash);

            UnitonConnectLogger.Log("Transaction successfully sended, " +
                "start fetching status from blockchain...");

            StartCoroutine(ConfirmTransactionFromBlockchain(transactionHash));
        }

        private void OnSendingTonFail(string errorMessage) => OnNativeTransactionSendingFailed?.Invoke(errorMessage);

        private void OnSendingTonConfirm(SuccessTransactionData transactionData) => OnNativeTransactionConfirmed?.Invoke(transactionData);
    }

    public enum SdkTypes
    {
        Old,
        Native
    }
}