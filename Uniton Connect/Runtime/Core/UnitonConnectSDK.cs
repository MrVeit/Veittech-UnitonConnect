using System;
using System.Collections;
using UnityEngine;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.DeFi;
using UnitonConnect.ThirdParty;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.Core
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/MrVeit/Veittech-UnitonConnect")]
    public sealed class UnitonConnectSDK : MonoBehaviour, IUnitonConnectSDKCallbacks,
        IUnitonConnectWalletCallbacks, IUnitonConnectTonCallbacks
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
        [Tooltip("Enable if you want to test the SDK without having to upload data about your dApp")]
        [SerializeField, Space] private bool _testMode;
        [Tooltip("Enable if you want to activate SDK logging for detailed analysis before releasing a dApp")]
        [SerializeField] private bool _debugMode;
        [Tooltip("Turn it off if you want to do your own cdk initialization in your scripts")]
        [SerializeField, Space] private bool _initializeOnAwake;
        [Tooltip("Delay before requesting in blockchain to retrieve data about the sent transaction")]
        [SerializeField, Space, Range(15f, 500f)] private float _confirmTransactionDelay;
        [Tooltip("List of available tokens for transactions/reading balances and more")]
        [SerializeField, Space] private JettonConfigsStorage _jettonStorage;

        private NewWalletConfig _connectedWalletConfig;

        private bool _isInitialized;
        private bool _isWalletConnected;

        public UserWallet Wallet { get; private set; }
        public UserAssets Assets { get; private set; }

        public decimal TonBalance { get; private set; }

        public NewWalletConfig ConnectedWalletConfig => _connectedWalletConfig;
        public JettonConfigsStorage JettonStorage => _jettonStorage;

        public bool IsInitialized => _isInitialized;
        public bool IsTestMode => _testMode;
        public bool IsDebugMode => _debugMode;

        public bool IsWalletConnected => _isWalletConnected;

        public float TransactionFetchDelay => _confirmTransactionDelay;

        /// <summary>
        /// Callback if native sdk initialization finished with same result
        /// </summary>
        public event IUnitonConnectSDKCallbacks.OnInitialize OnInitiliazed;

        /// <summary>
        /// Callback if native sdk initialization finished with same result
        /// </summary>
        public event IUnitonConnectSDKCallbacks.OnNativeUnitonConnectInitialize OnNativeInitialized;

        /// <summary>
        /// Callback in case of successful native wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnConnect OnConnected;

        /// <summary>
        /// Callback in case of successful native wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletConnectionFinish OnNativeWalletConnectionFinished;

        /// <summary>
        /// Callback for error handling, in case of unsuccessful wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnConnectFail OnConnectFailed;

        /// <summary>
        /// Callback for error handling, in case of unsuccessful wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletConnectionFail OnNativeWalletConnectionFailed;

        /// <summary>
        /// Callback for processing the status of restored native connection to the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnConnectRestore OnConnectRestored;

        /// <summary>
        /// Callback for processing the status of restored native connection to the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletConnectionRestore OnNativeWalletConnectionRestored;

        /// <summary>
        /// Callback to handle native wallet connection disconnection status
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnDisconnect OnDisconnected;

        /// <summary>
        /// Callback to handle native wallet connection disconnection status
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnNativeWalletDisconnect OnNativeWalletDisconnected;

        /// <summary>
        /// Callback to process the status of the sent toncoin transaction and get its hash
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonTransactionSend OnTonTransactionSended;

        /// <summary>
        /// Callback to process the status of a sent toncoin transaction and retrieve its hash
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnNativeTransactionSendingFinish OnNativeSendingTonFinished;

        /// <summary>
        /// Callback to handle failed sending of a transaction with toncoin
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonTransactionSendFail OnTonTransactionSendFailed;
        /// <summary>
        /// Callback to handle failed sending of a transaction with toncoin
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnNativeTransactionSendingFail OnNativeTransactionSendingFailed;

        /// <summary>
        /// Callback to retrieve transaction information from the blockchain after the transaction has been successfully sent
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonTransactionConfirm OnTonTransactionConfirmed;

        /// <summary>
        /// Callback to retrieve transaction information from the blockchain after it has been successfully sent
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnNativeTransactionConfirm OnNativeTransactionConfirmed;

        /// <summary>
        /// Callback to get the current amount of toncoin on the wallet
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonBalanceClaim OnTonBalanceClaimed;

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

            if (IsSupporedPlatform())
            {
                TonConnectBridge.Init(dAppManifestLink,
                    OnInitialize, OnConnect, 
                    OnConnectFail, OnConnectRestore);
            }
            else
            {
                OnInitialize(true);
            }

            UnitonConnectLogger.Log("Native SDK successfully initialized");

            _isInitialized = true;
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

            TonConnectBridge.Connect(OnConnect, OnConnectFail);
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

            TonConnectBridge.Disconnect(OnDisconnect);
        }

        /// <summary>
        /// Loading ton balance on a connected wallet, if it exists there
        /// </summary>
        public void LoadBalance()
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
        public void SendTransaction(string recipientAddress, 
            decimal amount, string message = null)
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

            var recipientToHex = WalletConnectUtils.GetHEXAddress(recipientAddress);

            if (WalletConnectUtils.IsAddressesMatch(recipientToHex))
            {
                UnitonConnectLogger.LogWarning("Transaction canceled because the recipient and sender addresses match");

                return;
            }

            UnitonConnectLogger.Log($"Created a request to send a TON" +
                    $" to the recipient: {recipientAddress} in amount {amount}");

            TonConnectBridge.SendTon(recipientAddress,
                amount, message, OnSendingTonFinish, OnSendingTonFail);
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

        private IEnumerator ConfirmTonTransaction(string transactionHash)
        {
            bool isFailed = false;

            if (isFailed)
            {
                yield return new WaitForSeconds(5f);
            }

            yield return TonApiBridge.GetTransactionData(transactionHash,
                _confirmTransactionDelay, (transactionData) =>
            {
                var fee = UserAssetsUtils.FromNanoton(transactionData.TotalFees).ToString();
                var updatedBalance = UserAssetsUtils.FromNanoton(transactionData.EndBalance).ToString();
                var sendedAmount = UserAssetsUtils.FromNanoton(transactionData.OutMessages[0].Value).ToString();

                isFailed = false;

                OnSendingTonConfirm(transactionData);

                UnitonConnectLogger.Log($"Ton transaction {transactionHash} confirmed, " +
                    $"fee: {fee}, updated balance: {updatedBalance}, sended amount: {sendedAmount}");
            },
            (errorMessage) =>
            {
                UnitonConnectLogger.LogError($"Failed to fetch ton transaction data, reason: {errorMessage}");

                if (errorMessage == "entity not found")
                {
                    isFailed = true;

                    StartCoroutine(ConfirmTonTransaction(transactionHash));

                    return;
                }

                OnSendingTonConfirm(null);
            });
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

        private void OnInitialize(bool isSuccess)
        {
            OnInitiliazed?.Invoke(isSuccess);
            OnNativeInitialized?.Invoke(isSuccess);
        }

        private void OnConnect(NewWalletConfig walletConfig)
        {
            _isWalletConnected = true;

            _connectedWalletConfig = walletConfig;

            var nonBouceableAddress = WalletConnectUtils
                .GetNonBounceableAddress(walletConfig.Address);

            var config = new WalletConfig()
            {
                Address = nonBouceableAddress,
                Chain = walletConfig.Chain,
                PublicKey = walletConfig.PublicKey,
                StateInit = walletConfig.StateInit,
            };

            NewWalletConfig updatedConfig = config;

            Wallet = new UserWallet(nonBouceableAddress, updatedConfig);

            OnConnected?.Invoke(config);
            OnNativeWalletConnectionFinished?.Invoke(updatedConfig);
        }

        private void OnConnectFail(string errorMessage)
        {
            _isWalletConnected = false;

            OnConnectFailed?.Invoke(errorMessage);
            OnNativeWalletConnectionFailed?.Invoke(errorMessage);
        }

        private void OnConnectRestore(bool isRestored)
        {
            if (isRestored)
            {
                _isWalletConnected = true;
            }

            OnConnectRestored?.Invoke(isRestored);
            OnNativeWalletConnectionRestored?.Invoke(isRestored);
        }

        private void OnDisconnect(bool isSuccess)
        {
            _isWalletConnected = false;

            Wallet = new UserWallet(null, null);

            OnDisconnected?.Invoke(isSuccess);
            OnNativeWalletDisconnected?.Invoke(isSuccess);
        }

        private void OnSendingTonFinish(string transactionHash)
        {
            OnTonTransactionSended?.Invoke(transactionHash);
            OnNativeSendingTonFinished?.Invoke(transactionHash);

            UnitonConnectLogger.Log("Ton transaction successfully sended, " +
                "start fetching status from blockchain...");

            StartCoroutine(ConfirmTonTransaction(transactionHash));
        }

        private void OnSendingTonFail(string errorMessage)
        {
            OnTonTransactionSendFailed?.Invoke(errorMessage);
            OnNativeTransactionSendingFailed?.Invoke(errorMessage);
        }

        private void OnSendingTonConfirm(SuccessTransactionData transactionData)
        {
            OnTonTransactionConfirmed?.Invoke(transactionData);
            OnNativeTransactionConfirmed?.Invoke(transactionData);
        }
    }
}