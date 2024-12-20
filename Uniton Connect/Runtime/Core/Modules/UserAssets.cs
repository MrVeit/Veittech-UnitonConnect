using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnitonConnect.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.ThirdParty;

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

            public readonly decimal ForwardFee = decimal.Parse("0.0000000010");

            public string LatestJettonWalletAddress { get; private set; }

            public Jetton(MonoBehaviour mono,
                UnitonConnectSDK sdk)
            {
                _mono = mono;
                _sdk = sdk;
            }

            /// <summary>
            /// Callback to get the wallet address of a specific token, if it exists in the connected account
            /// </summary>
            public event IUnitonConnectJettonCallbacks.OnJettonAddressParsed OnAddressParsed;

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
            public void ParseAddress(string masterAddress)
            {
                if (!IsWalletConnected())
                {
                    return;
                }

                var currentAddress = _sdk.Wallet.ToHex();

                ParseAddress(masterAddress, currentAddress, (walletConfig) =>
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
            public void LoadBalance(JettonTypes type)
            {
                if (!IsWalletConnected())
                {
                    return;
                }


            }

            /// <summary>
            /// Loading the balance of a custom token on the connected wallet by its master address
            /// </summary>
            /// <param name="masterAddress"></param>
            public void LoadBalance(string masterAddress)
            {
                if (!IsWalletConnected())
                {
                    return;
                }


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

                var targetJetton = _sdk.JettonStorage.Jettons
                    .FirstOrDefault(jetton => jetton.Type == type);

                if (targetJetton == null)
                {
                    UnitonConnectLogger.LogError($"Jetton {type} not found " +
                        $"in the storage of available jettons for sending");

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
                if (string.IsNullOrEmpty(LatestJettonWalletAddress))
                {
                    UnitonConnectLogger.LogWarning("The jetton wallet has not been loaded, start parsing...");
                }

                var ownerAddress = _sdk.Wallet.ToHex();

                ParseAddress(masterAddress, ownerAddress, (walletConfig) =>
                {
                    if (walletConfig == null)
                    {
                        OnTransactionSendFailed?.Invoke(null, $"Jetton wallet was not " +
                            $"deploay at ton address: {ownerAddress}");

                        return;
                    }

                    LatestJettonWalletAddress = walletConfig.Address;

                    _mono.StartCoroutine(CreateTransaction(amount, ownerAddress, recipientAddress));
                });
            }

            private void ParseAddress(string masterAddress, string tonAddress,
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

            private IEnumerator CreateTransaction(decimal amount,
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

                    var amountInNano = UserAssetsUtils.ToUSDtNanoton((double)amount);

                    TonConnectBridge.SendJetton(LatestJettonWalletAddress,
                        amountInNano.ToString(), payload, (transactionHash) =>
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

            private IEnumerator LoadTransactionStatus(string transactionHash)
            {
                bool isFailed = false;

                if (isFailed)
                {
                    yield return new WaitForSeconds(5f);
                }

                yield return TonApiBridge.GetTransactionData(transactionHash,
                    _sdk.TransactionFetchDelay, (transactionData) =>
                {
                    isFailed = false;

                    OnTransactionSended?.Invoke(_latestMasterAddress, transactionData);
                },
                (errorMessage) =>
                {
                    UnitonConnectLogger.LogError($"Failed to fetch jetton" +
                        $" transaction data, reason: {errorMessage}");

                    if (errorMessage == "entity not found")
                    {
                        isFailed = true;

                        return;
                    }

                    OnTransactionSendFailed?.Invoke(_latestMasterAddress, errorMessage);
                });
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