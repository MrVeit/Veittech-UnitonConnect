using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitonConnect.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.ThirdParty;
using UnitonConnect.Editor.Data;

namespace UnitonConnect.DeFi
{
    public sealed class UserAssets
    {
        public NFT Nft { get; private set; }

        public Jetton Jettons { get; private set; }

        public UserAssets(MonoBehaviour mono,
            UnitonConnectSDK sdk)
        {
            Nft = new NFT(mono, sdk);
            Jettons = new Jetton(mono, sdk);
        }

        public sealed class NFT : IUnitonConnectNftCallbacks
        {
            private readonly MonoBehaviour _mono;
            private readonly UnitonConnectSDK _sdk;

            private string _latestNftItemAddress;

            public NFT(MonoBehaviour mono,
                UnitonConnectSDK sdk)
            {
                _mono = mono;
                _sdk = sdk;
            }

            private string _walletAddress => _sdk.Wallet.ToString();

            public NftCollectionData LatestNftCollections { get; private set; }
            public NftCollectionData LatestTargetNftCollection { get; private set; }

            /// <summary>
            /// Callback to retrieve all nft collections on a user's account
            /// </summary>
            public event IUnitonConnectNftCallbacks.OnNftCollectionsClaim OnNftCollectionsClaimed;

            /// <summary>
            /// Callback to retrieve the current nft collection on the user's account
            /// </summary>
            public event IUnitonConnectNftCallbacks.OnTargetNftCollectionClaim OnTargetNftCollectionClaimed;

            /// <summary>
            /// Callback for notification that no NFTs are detected on the account
            /// </summary>
            public event IUnitonConnectNftCallbacks.OnNftCollectionsNotFound OnNftCollectionsNotFounded;

            /// <summary>
            /// Callback to retrieve nft transaction information from the blockchain after a successful send.
            /// </summary>
            public event IUnitonConnectNftCallbacks.OnNftTransactionSend OnTransactionSended;

            /// <summary>
            /// Callback to handle an unsuccessful nft transaction submission
            /// </summary>
            public event IUnitonConnectNftCallbacks.OnNftTransactionSendFail OnTransactionSendFailed;

            /// <summary>
            /// Receive all available collections on your NFT account
            /// </summary>
            /// <param name="limit">Number of collections displayed</param>
            /// <param name="offset">Number of gaps between collections</param>
            public void Load(int limit, int offset = 0)
            {
                _mono.StartCoroutine(TonApiBridge.NFT.GetCollections(
                    null, limit, offset, (collections) =>
                {
                    if (collections.Items.Count == 0 || collections.Items == null)
                    {
                        OnNftCollectionsNotFounded?.Invoke();

                        UnitonConnectLogger.LogWarning("NFT collections are not detected on the current wallet");

                        return;
                    }

                    LatestNftCollections = collections;

                    OnNftCollectionsClaimed?.Invoke(collections);
                }));
            }

            /// <summary>
            /// Get a collection on an account, with a specific contract address
            /// </summary>
            /// <param name="collectionAddress">Address nft collection</param>
            public void LoadTargetCollection(string collectionAddress, 
                int limit, int offset = 0)
            {
                _mono.StartCoroutine(TonApiBridge.NFT.GetCollections(
                    collectionAddress, limit, offset, (collection) =>
                {
                    if (collection.Items.Count == 0 || collection.Items == null)
                    {
                        OnNftCollectionsNotFounded?.Invoke();

                        UnitonConnectLogger.LogWarning("NFT collections are not detected on the current wallet");

                        return;
                    }

                    LatestTargetNftCollection = collection;

                    OnTargetNftCollectionClaimed?.Invoke(LatestTargetNftCollection);
                }));
            }

            /// <summary>
            /// Sending an nft item to its smart contract address to the target recipient
            /// </summary>
            /// <param name="recipient"></param>
            /// <param name="nftItemAddress"></param>
            /// <param name="gasFee">Validated range from 0.05-0.12 TON</param>
            public void SendTransaction(string nftItemAddress,
                string recipient, decimal gasFee)
            {
                CreateTransactionPayload(recipient, nftItemAddress, gasFee);
            }

            private void CreateTransactionPayload(string recipient, 
                string nftItemAddress, decimal gasFee)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                var ownerAddress = _sdk.Wallet.ToHex();
                var recipientToHex = WalletConnectUtils.GetHEXAddress(recipient);

                if (WalletConnectUtils.IsAddressesMatch(recipient))
                {
                    return;
                }

                _latestNftItemAddress = nftItemAddress;

                _mono.StartCoroutine(CreateTransaction(recipientToHex,
                    ownerAddress, nftItemAddress, gasFee));
            }

            private IEnumerator CreateTransaction(string recipient,
                string sender, string nftItemAddress, decimal gasFee)
            {
                yield return TonApiBridge.NFT.GetTransactionPayload(
                    recipient, sender, (payload) =>
                {
                    if (string.IsNullOrEmpty(payload))
                    {
                        UnitonConnectLogger.LogError($"Failed to create a payload" +
                            $" to send NFT item to {recipient}, try again later.");

                        return;
                    }

                    var feeInNano = UserAssetsUtils.ToNanoton(gasFee);

                    TonConnectBridge.SendNft(nftItemAddress,
                        feeInNano.ToString(), payload, (transactionHash) =>
                    {
                        UnitonConnectLogger.Log($"NFT transaction with " +
                            $"payload successfully sended: {transactionHash}");

                        _mono.StartCoroutine(LoadTransactionStatus(_mono, transactionHash,
                            _latestNftItemAddress, (nftAddress, transactionData) =>
                        {
                            OnTransactionSended?.Invoke(
                                nftAddress, transactionData);

                            UnitonConnectLogger.Log($"Nft item successfully " +
                                $"sended with address: {nftAddress}");
                        },
                        (nftAddress, error) =>
                        {
                            OnTransactionSendFailed?.Invoke(nftAddress, error);

                            UnitonConnectLogger.LogError($"Failed to send nft " +
                                $"item '{nftAddress}', reason: {error}");
                        }));
                    },
                    (errorMessage) =>
                    {
                        OnTransactionSendFailed?.Invoke(
                            _latestNftItemAddress, errorMessage);
                    });
                });
            }
        }

        public sealed class Jetton : IUnitonConnectJettonCallbacks
        {
            private readonly MonoBehaviour _mono;
            private readonly UnitonConnectSDK _sdk;

            public readonly decimal ForwardFee = (decimal)0.0000000010f;

            private string _latestMasterAddress;

            public string LatestJettonWalletAddress { get; private set; }

            public Jetton(MonoBehaviour mono,
                UnitonConnectSDK sdk)
            {
                _mono = mono;
                _sdk = sdk;
            }

            /// <summary>
            /// Callback to get the jetton balance for a specific master address with its configuration
            /// </summary>
            public event IUnitonConnectJettonCallbacks.OnJettonBalanceLoaded OnBalanceLoaded;

            /// <summary>
            /// Callback to get the wallet address of a specific token, if it exists in the connected account
            /// </summary>
            public event IUnitonConnectJettonCallbacks.OnJettonAddressParsed OnAddressParsed;

            /// <summary>
            /// Callback to retrieve a list of successful owner's Jetton transactions (received or sent)
            /// </summary>
            public event IUnitonConnectJettonCallbacks.OnLastJettonTransactionsLoaded OnLastTransactionsLoaded;

            /// <summary>
            /// Callback to retrieve transaction information from the blockchain after a successful submission
            /// </summary>
            public event IUnitonConnectJettonCallbacks.OnJettonTransactionSend OnTransactionSended;

            /// <summary>
            /// Callback for handling failed transaction sending from jetton
            /// </summary>
            public event IUnitonConnectJettonCallbacks.OnJettonTransactionSendFail OnTransactionSendFailed;

            /// <summary>
            /// Getting the address of the jetton wallet by the master address, 
            /// if it was previously created on the connected wallet
            /// </summary>
            /// <param name="masterAddress">Master address of the target jetton contract</param>
            public void GetAddress(string masterAddress)
            {
                if (!IsWalletConnected())
                {
                   return;
                }

                if (string.IsNullOrEmpty(masterAddress))
                {
                    UnitonConnectLogger.LogWarning("The master address is " +
                        "required to load the jetton wallet address!");

                    return;
                }

                var currentAddress = _sdk.Wallet.ToHex();

                GetAddress(masterAddress, currentAddress, (walletConfig) =>
                {
                    if (walletConfig == null)
                    {
                        OnAddressParsed?.Invoke(null);

                        return;
                    }

                    var parsedAddress = walletConfig.Address;

                    LatestJettonWalletAddress = WalletConnectUtils.GetBounceableAddress(parsedAddress);

                    OnAddressParsed?.Invoke(LatestJettonWalletAddress);
                });
            }

            /// <summary>
            /// Loading the balance of the classic token on the connected wallet
            /// </summary>
            /// <param name="type"></param>
            public void GetBalance(JettonTypes type)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                var targetJetton = GetConfigByType(type);

                if (targetJetton == null)
                {
                    return;
                }

                var masterAddress = targetJetton.MasterAddress;

                GetBalance(masterAddress);
            }

            /// <summary>
            /// Loading the balance of a custom token on the connected wallet by its master address
            /// </summary>
            /// <param name="masterAddress">Jetton master address for loading</param>
            public void GetBalance(string masterAddress)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                if (string.IsNullOrEmpty(masterAddress))
                {
                    UnitonConnectLogger.LogWarning("The master address is " +
                        "required to load the jetton balance!");

                    return;
                }

                var currentAddress = _sdk.Wallet.ToHex();

                _mono.StartCoroutine(TonApiBridge.Jetton.GetBalance(
                    currentAddress, masterAddress, (loadedJettonConfig) =>
                {
                    if (loadedJettonConfig == null)
                    {
                        UnitonConnectLogger.LogWarning($"Target jetton " +
                            $"{masterAddress} not found at connected wallet");

                        OnBalanceLoaded?.Invoke(0, "NOT_FOUND", masterAddress);

                        return;
                    }

                    var balanceInNano = long.Parse(loadedJettonConfig.BalanceInNano);
                    decimal balance = UserAssetsUtils.FromNanoton(balanceInNano);

                    var tokenName = loadedJettonConfig.Configuration.Name;

                    UnitonConnectLogger.Log($"Loaded jetton balance " +
                        $"{tokenName} with balance {balance}");
                    
                    if (ClassicJettonNames.IsStablecoin(tokenName))
                    {
                        balance = (decimal)UserAssetsUtils.FromUSDtNanoton(balanceInNano);

                        UnitonConnectLogger.Log($"Loaded jetton by type " +
                            $"'Stablecoin' with name {tokenName} and balance: {balance}");
                    }

                    OnBalanceLoaded?.Invoke(balance, tokenName, masterAddress);
                }));
            }

            /// <summary>
            /// Getting the latest successful transactions by 'sent/received' type
            /// </summary>
            /// <param name="type"></param>
            /// <param name="limit"></param>
            public void GetLastTransactions(TransactionTypes type, 
                int limit, string address = null)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                var transactionTag = type == TransactionTypes.Received ? "in" : "out";

                string connectedAddress = _sdk.Wallet.ToHex();

                if (string.IsNullOrEmpty(address))
                {
                    address = connectedAddress;
                }

                _mono.StartCoroutine(TonCenterApiBridge.Jetton.GetLastTransactions(
                    address, transactionTag, limit, (loadedTransactions) =>
                {
                    if (loadedTransactions == null)
                    {
                        OnLastTransactionsLoaded?.Invoke(type, 
                            new List<JettonTransactionData>());

                        return;
                    }

                    UnitonConnectLogger.Log($"Loaded the last {limit} jetton transactions");

                    OnLastTransactionsLoaded?.Invoke(type, loadedTransactions);
                }));
            }

            /// <summary>
            /// Sending classic tokens with adjustable fees to avoid problems when sending to new wallets
            /// </summary>
            /// <param name="type"></param>
            /// <param name="recipientAddress"></param>
            /// <param name="amount"></param>
            /// <param name="gasFee">Validated range from 0.05-0.1 TON 
            /// (with available jetton wallet by recipient or not)</param>
            public void SendTransaction(JettonTypes type, string recipientAddress,
                decimal amount, decimal gasFee, string message = null)
            {
                var targetJetton = GetConfigByType(type);

                if (targetJetton == null)
                {
                    return;
                }

                _latestMasterAddress = targetJetton.MasterAddress;

                CreateTransactionPayload(type, _latestMasterAddress, 
                    recipientAddress, amount, gasFee, message);
            }

            /// <summary>
            /// Sending custom tokens to a master address with adjustable fees 
            /// to avoid problems when sending to new wallets
            /// </summary>
            /// <param name="masterAddress"></param>
            /// <param name="recipientAddress"></param>
            /// <param name="amount"></param>
            /// <param name="gasFee">Validated range from 0.05 TON (with available jetton wallet)
            /// to 0.1 TON (with creating jetton wallet) </param>
            public void SendTransaction(string masterAddress, string recipientAddress,
                decimal amount, decimal gasFee, string message = null)
            {
                CreateTransactionPayload(JettonTypes.Custom, masterAddress, 
                    recipientAddress, amount, gasFee, message);
            }

            private void GetAddress(string masterAddress, string tonAddress,
                 Action<JettonWalletData> walletParsed)
            {
                _mono.StartCoroutine(UserAssetsUtils.GetJettonWalletByAddress(
                    masterAddress, tonAddress, (parsedJettonWallet) =>
                    {
                        if (parsedJettonWallet == null)
                        {
                            UnitonConnectLogger.LogWarning($"Jetton Wallet is not" +
                                $" deployed by master address: {masterAddress}");

                            walletParsed?.Invoke(null);

                            return;
                        }

                        walletParsed?.Invoke(parsedJettonWallet);

                        UnitonConnectLogger.Log($"Parsed jetton wallet: {parsedJettonWallet.Address}");
                    }));
            }

            private void CreateTransactionPayload(JettonTypes type, string masterAddress, 
                string recipient, decimal amount, decimal gasFee, string message = null)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                if (string.IsNullOrEmpty(LatestJettonWalletAddress))
                {
                    UnitonConnectLogger.LogWarning("The jetton wallet " +
                        "has not been loaded, start parsing...");
                }

                var ownerAddress = _sdk.Wallet.ToHex();
                var recipientToHex = WalletConnectUtils.GetHEXAddress(recipient);

                if (WalletConnectUtils.IsAddressesMatch(recipient))
                {
                    return;
                }

                GetAddress(masterAddress, ownerAddress, (walletConfig) =>
                {
                    if (walletConfig == null)
                    {
                        OnTransactionSendFailed?.Invoke(null, $"Jetton wallet was not " +
                            $"deploay at ton address: {ownerAddress}");

                        return;
                    }

                    LatestJettonWalletAddress = walletConfig.Address;

                    _mono.StartCoroutine(CreateTransaction(type,
                        amount, gasFee, ownerAddress, recipient, message));
                });
            }

            private IEnumerator CreateTransaction(JettonTypes jettonType, decimal amount,
                decimal gasFee, string sender, string recipient, string message = null)
            {
                yield return TonApiBridge.Jetton.GetTransactionPayload(jettonType, amount,
                    ForwardFee, sender, recipient, message, (payload) =>
                {
                    if (string.IsNullOrEmpty(payload))
                    {
                        UnitonConnectLogger.LogError($"Failed to create a payload" +
                            $" to send jettons to {recipient}, try again later.");

                        return;
                    }

                    var feeInNano = UserAssetsUtils.ToNanoton(gasFee);

                    TonConnectBridge.SendJetton(LatestJettonWalletAddress,
                        feeInNano.ToString(), payload, (transactionHash) =>
                    {
                        UnitonConnectLogger.Log($"Jetton transaction with " +
                        $"payload successfully sended: {transactionHash}");

                        _mono.StartCoroutine(LoadTransactionStatus(_mono,
                            transactionHash, _latestMasterAddress,
                            (masterAddress, transactionData) =>
                        {
                            OnTransactionSended?.Invoke(
                                masterAddress, transactionData);
                        },
                        (masterAddress, error) =>
                        {
                            OnTransactionSendFailed?.Invoke(
                                masterAddress, error);
                        }));
                    },
                    (errorMessage) =>
                    {
                        OnTransactionSendFailed?.Invoke(
                            _latestMasterAddress, errorMessage);
                    });
                });
            }

            private JettonConfig GetConfigByType(JettonTypes type)
            {
                var targetJetton = _sdk.JettonStorage.Jettons
                    .FirstOrDefault(jetton => jetton.Type == type);

                if (targetJetton == null)
                {
                    UnitonConnectLogger.LogError($"Jetton {type} not found " +
                        $"in the storage of available jettons for sending");

                    return null;
                }

                return targetJetton;
            }
        }

        internal static IEnumerator LoadTransactionStatus(MonoBehaviour mono, string hash, 
            string itemId, Action<string, SuccessTransactionData> transactionConfirmed,
            Action<string, string> transactionSendFailed, bool isFailedResponse = false)
        {
            var delay = UnitonConnectSDK.Instance.TransactionFetchDelay;

            if (isFailedResponse)
            {
                if (delay <= 0)
                {
                    delay = 15f;
                }

                UnitonConnectLogger.LogWarning($"Enabled a delay of {delay} seconds " +
                    "between attempts due to a failed last request");

                yield return new WaitForSeconds(delay);
            }

            yield return TonApiBridge.GetTransactionData(hash,
                (transactionData) =>
            {
                transactionConfirmed?.Invoke(itemId, transactionData);
            },
            (errorMessage) =>
            {
                UnitonConnectLogger.LogError($"Failed to fetch " +
                    $"transaction data, reason: {errorMessage}");

                if (errorMessage == "entity not found")
                {
                    mono.StartCoroutine(LoadTransactionStatus(mono, hash,
                        itemId, transactionConfirmed, transactionSendFailed, true));

                    return;
                }

                transactionSendFailed?.Invoke(itemId, errorMessage);
            });
        }

        internal static bool IsWalletConnected()
        {
            if (!UnitonConnectSDK.Instance.IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning("Wallet is not connected, action canceled");

                return false;
            }

            return true;
        }
    }
}