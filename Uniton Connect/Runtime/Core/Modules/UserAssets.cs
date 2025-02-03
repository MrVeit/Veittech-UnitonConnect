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
            /// Receive all available collections on your NFT account
            /// </summary>
            /// <param name="limit">Number of collections displayed</param>
            /// <param name="offset">Number of gaps between collections</param>
            public void Load(int limit, int offset = 0)
            {
                var encodedWalletAddress = ConvertAddressToEncodedURL(_walletAddress);
                var url = TonApiBridge.NFT.GetAllCollectionsUrl(encodedWalletAddress, limit, offset);

                _mono.StartCoroutine(TonApiBridge.NFT.GetCollections(url, (collections) =>
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
            public void LoadTargetCollection(string collectionAddress, int limit, int offset = 0)
            {
                var encodedWalletAddress = ConvertAddressToEncodedURL(_walletAddress);
                var encodedCollectionAddress = ConvertAddressToEncodedURL(collectionAddress);

                var url = TonApiBridge.NFT.GetTargetCollectionUrl(encodedWalletAddress,
                    encodedCollectionAddress, limit, offset);

                _mono.StartCoroutine(TonApiBridge.NFT.GetCollections(url, (collection) =>
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

            private string ConvertAddressToEncodedURL(string address)
            {
                return TonApiBridge.ConvertAddressToEncodeURL(address);
            }
        }

        public sealed class Jetton : IUnitonConnectJettonCallbacks
        {
            private readonly MonoBehaviour _mono;
            private readonly UnitonConnectSDK _sdk;

            private string _latestMasterAddress;

            public readonly decimal ForwardFee = (decimal)0.0000000010f;

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
                   // return;
                }

                if (string.IsNullOrEmpty(masterAddress))
                {
                    UnitonConnectLogger.LogWarning("The master address is " +
                        "required to load the jetton balance!");

                    return;
                }

                var currentAddress = "UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I"; //_sdk.Wallet.ToHex();

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
                    
                    if (tokenName == ClassicJettonNames.USDT_NAME)
                    {
                        balance = (decimal)UserAssetsUtils.FromUSDtNanoton(balanceInNano);
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
                        OnLastTransactionsLoaded?.Invoke(type, new List<JettonTransactionData>());

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
            /// <param name="gasFee">Validated range from 0.003 (with available token wallet) to 0.011 (with deposited token wallet) </param>
            public void SendTransaction(JettonTypes type, string recipientAddress,
                decimal amount, decimal gasFee, string message = null)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                if (string.IsNullOrEmpty(recipientAddress))
                {
                    UnitonConnectLogger.LogWarning("Recipient address " +
                        "is required, transaction cancelled");

                    return;
                }

                var targetJetton = GetConfigByType(type);

                if (targetJetton == null)
                {
                    return;
                }

                _latestMasterAddress = targetJetton.MasterAddress;

                SendTransaction(_latestMasterAddress, recipientAddress, amount, gasFee, message);
            }

            /// <summary>
            /// Sending custom tokens to a master address with adjustable fees 
            /// to avoid problems when sending to new wallets
            /// </summary>
            /// <param name="masterAddress"></param>
            /// <param name="recipientAddress"></param>
            /// <param name="amount"></param>
            /// <param name="gasFee">Validated range from 0.003 TON (with available jetton wallet)
            /// to 0.012 TON (with creating jetton wallet) </param>
            public void SendTransaction(string masterAddress, string recipientAddress,
                decimal amount, decimal gasFee, string message = null)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                if (string.IsNullOrEmpty(LatestJettonWalletAddress))
                {
                    UnitonConnectLogger.LogWarning("The jetton wallet has not been loaded, start parsing...");
                }

                var ownerAddress = _sdk.Wallet.ToHex();
                var recipientToHex = WalletConnectUtils.GetHEXAddress(recipientAddress);

                if (WalletConnectUtils.IsAddressesMatch(recipientAddress))
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

                    _mono.StartCoroutine(CreateTransaction(amount, gasFee, 
                        ownerAddress, recipientAddress));
                });
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

            private IEnumerator CreateTransaction(decimal amount, decimal gasFee,
                string sender, string recipient)
            {
                yield return TonApiBridge.GetTransactionPayload(amount,
                    ForwardFee, sender, recipient, (payload) =>
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

                        _mono.StartCoroutine(LoadTransactionStatus(transactionHash));
                    },
                    (errorMessage) =>
                    {
                        OnTransactionSendFailed?.Invoke(_latestMasterAddress, errorMessage);
                    });
                });
            }

            private IEnumerator LoadTransactionStatus(
                string transactionHash, bool isFailedResponse = false)
            {
                var delay = _sdk.TransactionFetchDelay;

                if (isFailedResponse)
                {
                    UnitonConnectLogger.LogWarning($"Enabled a delay of {delay} seconds " +
                        "between attempts due to a failed last request");

                    yield return new WaitForSeconds(delay);
                }

                yield return TonApiBridge.GetTransactionData(
                    transactionHash, (transactionData) =>
                {
                    OnTransactionSended?.Invoke(_latestMasterAddress, transactionData);
                },
                (errorMessage) =>
                {
                    UnitonConnectLogger.LogError($"Failed to fetch jetton" +
                        $" transaction data, reason: {errorMessage}");

                    if (errorMessage == "entity not found")
                    {
                        _mono.StartCoroutine(LoadTransactionStatus(transactionHash, true));

                        return;
                    }

                    OnTransactionSendFailed?.Invoke(_latestMasterAddress, errorMessage);
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

            private bool IsWalletConnected()
            {
                if (!_sdk.IsWalletConnected)
                {
                    UnitonConnectLogger.LogWarning("Wallet is not connected, action canceled");

                    return false;
                }

                return true;
            }
        }
    }
}