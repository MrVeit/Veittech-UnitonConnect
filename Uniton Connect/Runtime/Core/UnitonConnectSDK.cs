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
        [SerializeField, Space, Range(10f, 500f)] private float _confirmDelay = 10f;
        [Tooltip("List of available tokens for transactions/reading balances and more")]
        [SerializeField, Space] private JettonConfigsStorage _jettonStorage;

        private WalletConfig _connectedWalletConfig;

        private bool _isInitialized;
        private bool _isWalletConnected;

        public UserWallet Wallet { get; private set; }
        public UserAssets Assets { get; private set; }
        public WalletModal Modal { get; private set; }

        public SignMessageData LastMessageForSign { get; private set; }
        public SignedMessageData LastSignedMessage { get; private set; }

        public decimal TonBalance { get; private set; }

        public WalletConfig ConnectedWalletConfig => _connectedWalletConfig;
        public JettonConfigsStorage JettonStorage => _jettonStorage;

        public bool IsInitialized => _isInitialized;
        public bool IsTestMode => _testMode;
        public bool IsDebugMode => _debugMode;

        public bool IsWalletConnected => _isWalletConnected;

        public float TransactionFetchDelay => _confirmDelay;

        /// <summary>
        /// Callback if native sdk initialization finished with same result
        /// </summary>
        public event IUnitonConnectSDKCallbacks.OnInitialize OnInitiliazed;

        /// <summary>
        /// Callback in case of successful native wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnect OnWalletConnected;

        /// <summary>
        /// Callback for error handling, in case of unsuccessful wallet connection
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectFail OnWalletConnectFailed;

        /// <summary>
        /// Callback for processing the status of restored native connection to the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletConnectRestore OnWalletConnectRestored;

        /// <summary>
        /// Callback to handle native wallet connection disconnection status
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletDisconnect OnWalletDisconnected;

        /// <summary>
        /// Callback to process the status of the sent toncoin transaction and get its hash
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonTransactionSend OnTonTransactionSended;

        /// <summary>
        /// Callback to handle failed sending of a transaction with toncoin
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonTransactionSendFail OnTonTransactionSendFailed;

        /// <summary>
        /// Callback to retrieve transaction information from the blockchain after the transaction has been successfully sent
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonTransactionConfirm OnTonTransactionConfirmed;

        /// <summary>
        /// Callback to get the current amount of toncoin on the wallet
        /// </summary>
        public event IUnitonConnectTonCallbacks.OnTonBalanceClaim OnTonBalanceClaimed;

        /// <summary>
        /// Callback to receive the result of signing a message from the wallet
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletMessageSign OnWalletMessageSigned;

        /// <summary>
        /// Callback about unsuccessful signing of a message in the wallet under one of the reasons
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletMessageSignFail OnWalletMessageSignFailed;

        /// <summary>
        /// Callback about the received verification status of the wallet payload after signing the message
        /// </summary>
        public event IUnitonConnectWalletCallbacks.OnWalletMessageVerify OnWalletMessageVerified;

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
                TonConnectBridge.Dispose();
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
                TonConnectBridge.Init(dAppManifestLink, OnInitialize,
                    OnConnect, OnConnectFail, OnConnectRestore);
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
                UnitonConnectLogger.LogWarning("Transaction canceled because "+
                    "the recipient and sender addresses match");

                return;
            }

            UnitonConnectLogger.Log($"Created a request to send a TON" +
                $" to the recipient: {recipientAddress} in amount {amount}");

            TonConnectBridge.SendTon(recipientAddress, amount,
                message, OnSendingTonFinish, OnSendingTonFail);
        }

        /// <summary>
        /// Signs a message on the connected wallet to verify that it is actually connected to the dApp.
        /// </summary>
        public void SignData(SignMessageData message)
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

            TonConnectBridge.SignWalletMessage(message,
                OnWalletMessageSign, OnWalletMessageSigFail);
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
                    UnitonConnectLogger.LogWarning($"Another instance is "+
                        "detected on the scene, running delete...");

                    Destroy(gameObject);
                }
            }
        }

        private IEnumerator VerifySignedPayload(
            SignedMessageData signedPayload)
        {
            var signedMessagePayload = new MessagePayloadVerificationData()
            {
                Signature = signedPayload.Signature,
                Timestamp = signedPayload.Timestamp,
                AppDomain = signedPayload.AppDomain,
                MessagePayload = signedPayload.Payload,
                WalletAddress = signedPayload.Address,
                WalletPublicKey = Wallet.PublicKey,
                WalletStateInit = Wallet.StateInit
            };

            yield return TonCenterApiBridge.VerifySignedPayload(
                signedMessagePayload, (verifyStatus) =>
            {
                if (verifyStatus == null)
                {
                    UnitonConnectLogger.LogWarning("Signed message " +
                        "verification process failed");

                    OnWalletMessageVerified?.Invoke(false);
                }

                UnitonConnectLogger.Log($"Signed message verification process " +
                    $"finished with result: {verifyStatus.IsVerified}");

                OnWalletMessageVerified?.Invoke(verifyStatus.IsVerified);
            });
        }

        private IEnumerator ConfirmTonTransaction(
            string transactionHash, bool isFailed = false)
        {
            if (isFailed)
            {
                if (_confirmDelay <= 0)
                {
                    _confirmDelay = 10f;
                }

                UnitonConnectLogger.LogWarning($"Enabled a delay of {_confirmDelay} " +
                    $"seconds between attempts due to a failed last request");

                yield return new WaitForSeconds(_confirmDelay);
            }

            yield return TonApiBridge.GetTransactionData(transactionHash, (transactionData) =>
            {
                var fee = UserAssetsUtils.FromNanoton(transactionData.TotalFees).ToString();
                var updatedBalance = UserAssetsUtils.FromNanoton(transactionData.EndBalance).ToString();
                var sendedAmount = UserAssetsUtils.FromNanoton(transactionData.OutMessages[0].Value).ToString();

                OnSendingTonConfirm(transactionData);

                UnitonConnectLogger.Log($"Ton transaction {transactionHash} confirmed, " +
                    $"fee: {fee}, updated balance: {updatedBalance}, sended amount: {sendedAmount}");
            },
            (errorMessage) =>
            {
                UnitonConnectLogger.LogError($"Failed to fetch ton transaction data, reason: {errorMessage}");

                if (errorMessage == "entity not found")
                {
                    StartCoroutine(ConfirmTonTransaction(transactionHash, true));

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
            Modal = new WalletModal();

            OnInitiliazed?.Invoke(isSuccess);
        }

        private void OnConnect(WalletConfig walletConfig)
        {
            _isWalletConnected = true;

            _connectedWalletConfig = walletConfig;

            var nonBouceableAddress = WalletConnectUtils
                .GetNonBounceableAddress(walletConfig.Address);

            walletConfig.Address = nonBouceableAddress;

            Wallet = new UserWallet(nonBouceableAddress, walletConfig);

            OnWalletConnected?.Invoke(walletConfig);
        }

        private void OnConnectFail(string errorMessage)
        {
            _isWalletConnected = false;

            OnWalletConnectFailed?.Invoke(errorMessage);
        }

        private void OnConnectRestore(bool isRestored)
        {
            if (isRestored)
            {
                _isWalletConnected = true;
            }

            OnWalletConnectRestored?.Invoke(isRestored);
        }

        private void OnDisconnect(bool isSuccess)
        {
            _isWalletConnected = false;

            Wallet = new UserWallet(null, null);

            OnWalletDisconnected?.Invoke(isSuccess);
        }

        private void OnSendingTonFinish(string transactionHash)
        {
            OnTonTransactionSended?.Invoke(transactionHash);

            UnitonConnectLogger.Log("Ton transaction successfully sended, " +
                "start fetching status from blockchain...");

            StartCoroutine(ConfirmTonTransaction(transactionHash));
        }

        private void OnSendingTonFail(string errorMessage)
        {
            OnTonTransactionSendFailed?.Invoke(errorMessage);
        }

        private void OnSendingTonConfirm(SuccessTransactionData transactionData)
        {
            OnTonTransactionConfirmed?.Invoke(transactionData);
        }

        private void OnWalletMessageSign(SignedMessageData signedPayload)
        {
            LastMessageForSign = signedPayload.Payload;
            LastSignedMessage = signedPayload;

            UnitonConnectLogger.Log($"Wallet message successfully signed, message: "+
                $"{LastMessageForSign.Type}, signed payload: {signedPayload.Signature}");

            OnWalletMessageSigned?.Invoke(signedPayload);

            StartCoroutine(VerifySignedPayload(LastSignedMessage));
        }

        private void OnWalletMessageSigFail(string error)
        {
            UnitonConnectLogger.LogError($"Failed to sign wallet message, reason: {error}");

            OnWalletMessageSignFailed?.Invoke(error);
        }
    }
}