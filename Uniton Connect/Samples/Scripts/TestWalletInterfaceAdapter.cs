using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Runtime.Data;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestWalletInterfaceAdapter : MonoBehaviour
    {
        [SerializeField, Space] private TextMeshProUGUI _debugMessage;
        [SerializeField] private TextMeshProUGUI _shortWalletAddress;
        [SerializeField, Space] private Button _connectButton;
        [SerializeField] private Button _disconnectButton;
        [SerializeField] private Button _sendTransactionButton;
        [SerializeField] private Button _sendJettonTransactionButton;
        [SerializeField] private Button _openNftCollectionButton;
        [SerializeField, Space] private TestWalletNftCollectionsPanel _nftCollectionPanel;

        private UnitonConnectSDK _unitonSDK;

        private UserAssets.NFT _nftModule;
        private UserAssets.Jetton _jettonModule;

        private string _latestTransactionHash;

        public UnitonConnectSDK UnitonSDK => _unitonSDK;
        public UserAssets.NFT NftStorage => _nftModule;
        public UserAssets.Jetton JettonModule => _jettonModule;

        private void Awake()
        {
            _unitonSDK = UnitonConnectSDK.Instance;

            _unitonSDK.OnInitiliazed += SdkInitialized;

            _unitonSDK.OnWalletConnected += WalletConnectionFinished;
            _unitonSDK.OnWalletConnectFailed += WalletConnectionFailed;

            _unitonSDK.OnWalletConnectRestored += WalletConnectionRestored;
            _unitonSDK.OnWalletDisconnected += WalletDisconnected;

            _unitonSDK.OnTonTransactionSended += TonTransactionSended;
            _unitonSDK.OnTonTransactionSendFailed += TonTransactionSendFailed;

            _unitonSDK.OnTonTransactionConfirmed += TonTransactionConfirmed;
        }

        private void OnDestroy()
        {
            _unitonSDK.OnInitiliazed -= SdkInitialized;

            _unitonSDK.OnWalletConnected -= WalletConnectionFinished;
            _unitonSDK.OnWalletConnectFailed -= WalletConnectionFailed;

            _unitonSDK.OnWalletConnectRestored -= WalletConnectionRestored;
            _unitonSDK.OnWalletDisconnected -= WalletDisconnected;

            _unitonSDK.OnTonTransactionSended -= TonTransactionSended;
            _unitonSDK.OnTonTransactionSendFailed -= TonTransactionSendFailed;

            _unitonSDK.OnTonTransactionConfirmed -= TonTransactionConfirmed;

            if (_nftModule == null)
            {
                return;
            }

            _nftModule.OnNftCollectionsClaimed -= NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed -= TargetNftCollectionLoaded;

            _nftModule.OnTransactionSended -= NftTransactionSended;
            _nftModule.OnTransactionSendFailed -= NftTransactionSendFailed;

            if (_jettonModule == null)
            {
                return;
            }

            _jettonModule.OnTransactionSended -= JettonTransactionSended;
            _jettonModule.OnTransactionSendFailed -= JettonTransactionSendFailed;
        }

        private void Start()
        {
            _unitonSDK.Initialize();

            if (!_unitonSDK.IsWalletConnected)
            {
                _disconnectButton.interactable = false;
                _sendTransactionButton.interactable = false;
                _sendJettonTransactionButton.interactable = false;
                _openNftCollectionButton.interactable = false;
            }
        }

        private void PrintSuccessTransactionData(string transactionName,
            SuccessTransactionData transaction)
        {
            var status = transaction.IsSuccess;
            var newBalance = transaction.EndBalance.FromNanoton();
            var fee = transaction.TotalFees.FromNanoton();

            decimal sendedAmount = transaction.OutMessages[0].Value.FromNanoton();

            if (transactionName == "JETTON")
            {
                var amount = transaction.OutMessages[0].DecodedBody.SendedAmount;

                Debug.Log($"Parsed sended jetton amount: {amount}");

                sendedAmount = UserAssetsUtils.FromNanoton(decimal.Parse(amount));
            }

            string recipientAddress = transaction.OutMessages[0].Recipient.Address;
            var decodedBody = transaction.OutMessages[0].DecodedBody;

            if (transactionName == "JETTON")
            {
                recipientAddress = decodedBody.RecipientAddress;
            }
            else if (transactionName == "NFT")
            {
                recipientAddress = decodedBody.NewOwner;
            }

            var convertedAddress = WalletConnectUtils.GetNonBounceableAddress(recipientAddress);

            string message = string.Empty;

            if (transactionName == "TON")
            {
                if (decodedBody != null)
                {
                    message = decodedBody.MessageText;
                }
            }
            else if (transactionName == "JETTON")
            {
                var payload = decodedBody.ForwardPayload;

                if (payload.IsRight)
                {
                    message = message = payload.Value.Value.MessageText;

                    Debug.LogWarning(("Detected jetton transfer with message"));
                }
            }

            string transactionHeader = string.Empty;

            if (transactionName != "TON")
            {
                transactionHeader = transaction.OutMessages[0].DecodedOperationName.ToUpper();
            }
            else
            {
                transactionHeader = "TON_TRANSFER";
            }

            _debugMessage.text = $"Loaded '{transactionHeader}' transaction data: \n" +
                $"STATUS: {transaction.IsSuccess},\n" +
                $"HASH: {transaction.Hash},\n" +
                $"NEW BALANCE: {newBalance} TON,\n" +
                $"TOTAL FEE: {fee} TON,\n" +
                $"SENDED AMOUNT: {sendedAmount} {transactionName},\n" +
                $"RECIPIENT ADDRESS: {convertedAddress},\n" +
                $"MESSAGE: {message}";
        }

        private void SdkInitialized(bool isSuccess)
        {
            if (!isSuccess)
            {
                Debug.Log("Failed tto initialize sdk, something wrong...");

                return;
            }

            _connectButton.interactable = true;

            _nftModule = _unitonSDK.Assets.Nft;
            _jettonModule = _unitonSDK.Assets.Jettons;

            _nftModule.OnNftCollectionsClaimed += NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed += TargetNftCollectionLoaded;

            _nftModule.OnTransactionSended += NftTransactionSended;
            _nftModule.OnTransactionSendFailed += NftTransactionSendFailed;

            _jettonModule.OnTransactionSended += JettonTransactionSended;
            _jettonModule.OnTransactionSendFailed += JettonTransactionSendFailed;
        }

        private void WalletConnectionFinished(WalletConfig wallet)
        {
            if (_unitonSDK.IsWalletConnected)
            {
                var userAddress = wallet.Address;

                var successConnectMessage = $"Wallet is connected, " +
                    $"full account address: {userAddress}, \n" +
                    $"Public Key: {wallet.PublicKey}";

                var shortWalletAddress = _unitonSDK.Wallet.ToShort(6);

                _debugMessage.text = successConnectMessage;
                _shortWalletAddress.text = shortWalletAddress;

                Debug.Log($"Connected wallet short address: {shortWalletAddress}");

                Debug.Log($"Connected address is user friendly: {_unitonSDK.Wallet.IsUserFriendly}");
                Debug.Log($"Connected address is bounceable: {_unitonSDK.Wallet.IsBounceable}");
                Debug.Log($"Connected address from testnet: {_unitonSDK.Wallet.IsTestOnly}");

                _connectButton.interactable = false;
                _disconnectButton.interactable = true;
                _sendTransactionButton.interactable = true;
                _sendJettonTransactionButton.interactable = true;
                _openNftCollectionButton.interactable = true;
            }
        }

        private void WalletConnectionFailed(string message)
        {
            Debug.LogError($"Failed to connect " +
                $"the wallet due to the following reason: {message}");

            _connectButton.interactable = true;
            _disconnectButton.interactable = false;
            _sendTransactionButton.interactable = false;
            _sendJettonTransactionButton.interactable = false;
            _openNftCollectionButton.interactable = false;

            _debugMessage.text = string.Empty;
            _shortWalletAddress.text = string.Empty;

            Debug.LogWarning($"Connect status: " +
                $"{_unitonSDK.IsWalletConnected}");
        }

        private void WalletConnectionRestored(bool isRestored)
        {
            if (!isRestored)
            {
                return;
            }

            _connectButton.interactable = false;
            _disconnectButton.interactable = true;
            _sendTransactionButton.interactable = true;
            _sendJettonTransactionButton.interactable = true;
            _openNftCollectionButton.interactable = true;

            Debug.Log($"Connection to previously connected wallet restored");
        }

        private void WalletDisconnected(bool isSuccess)
        {
            if (!isSuccess)
            {
                return;
            }

            _nftCollectionPanel.RemoveNftCollectionStorage(true);

            _connectButton.interactable = true;
            _disconnectButton.interactable = false;
            _sendTransactionButton.interactable = false;
            _sendJettonTransactionButton.interactable = false;
            _openNftCollectionButton.interactable = false;

            _debugMessage.text = string.Empty;
            _shortWalletAddress.text = string.Empty;

            Debug.Log($"Previous wallet successful disconnected");
        }

        private void NftCollectionsLoaded(NftCollectionData collections)
        {
            Debug.Log($"Loaded nft collections: {collections.Items.Count}");
        }

        private void TargetNftCollectionLoaded(NftCollectionData nftCollection)
        {
            Debug.Log($"Loaded target nft collection with name: {nftCollection.Items[0].Collection.Name}");
        }

        private void TonTransactionSended(string transactionHash)
        {
            _latestTransactionHash = transactionHash;

            Debug.Log($"Latest transaction hash parsed: {_latestTransactionHash}");
        }

        private void TonTransactionConfirmed(SuccessTransactionData transactionData)
        {
            PrintSuccessTransactionData("TON", transactionData);
        }

        private void TonTransactionSendFailed(string errorMessage)
        {
            var message = $"Failed to send transaction, reason: {errorMessage}";

            Debug.LogError(message);

            _debugMessage.text = errorMessage;
        }

        private void JettonTransactionSended(string masterAddress, 
            SuccessTransactionData transactionData)
        {
            PrintSuccessTransactionData("JETTON", transactionData);
        }

        private void JettonTransactionSendFailed(
            string masterAddress, string errorMessage)
        {
            _debugMessage.text = $"Failed to send jetton transaction" +
                $" with token: {masterAddress}, reason: {errorMessage}";
        }

        private void NftTransactionSended(string nftItemAddress,
            SuccessTransactionData transactiionData)
        {
            PrintSuccessTransactionData("NFT", transactiionData);
        }

        private void NftTransactionSendFailed(
            string nftItemAddress, string errorMessage)
        {
            _debugMessage.text = $"Failed to send NFT item" +
                $" with address: {nftItemAddress}, reason: {errorMessage}";
        }
    }
}