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

        private string _latestTransactionHash;

        private UnitonConnectSDK _unitonSDK;

        private UserAssets.NFT _nftModule => _unitonSDK.Assets.Nft;
        private UserAssets.Jetton _jettonModule => _unitonSDK.Assets.Jettons;

        public UnitonConnectSDK UnitonSDK => _unitonSDK;
        public UserAssets.NFT NftStorage => _nftModule;
        public UserAssets.Jetton JettonModule => _jettonModule;

        private void Awake()
        {
            _unitonSDK = UnitonConnectSDK.Instance;

            _unitonSDK.OnInitiliazed += Initialize;

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
            _unitonSDK.OnInitiliazed -= Initialize;

            _unitonSDK.OnWalletConnected -= WalletConnectionFinished;
            _unitonSDK.OnWalletConnectFailed -= WalletConnectionFailed;
            _unitonSDK.OnWalletConnectRestored -= WalletConnectionRestored;

            _unitonSDK.OnNativeWalletDisconnected -= WalletDisconnected;

            _unitonSDK.OnNativeSendingTonFinished -= TonTransactionSended;
            _unitonSDK.OnNativeTransactionConfirmed -= TonTransactionConfirmed;

            _unitonSDK.OnNativeTransactionSendingFailed -= TonTransactionSendFailed;

            _nftModule.OnNftCollectionsClaimed -= NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed -= TargetNftCollectionLoaded;
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

            _nftModule.OnNftCollectionsClaimed += NftCollectionsLoaded;
            _nftModule.OnTargetNftCollectionClaimed += TargetNftCollectionLoaded;
        }

        private void Initialize(bool isSuccess)
        {
            if (!isSuccess)
            {
                Debug.Log("Failed tto initialize sdk, something wrong...");

                return;
            }

            _connectButton.interactable = true;
        }

        private void WalletConnectionFinished(NewWalletConfig wallet)
        {
            if (_unitonSDK.IsWalletConnected)
            {
                var userAddress = wallet.Address;

                var successConnectMessage = $"Wallet is connected, " +
                    $"full account address: {userAddress}, \n" +
                    $"Public Key: {wallet.PublicKey}";

                var shortWalletAddress = _unitonSDK.Wallet.ToShort(8);

                _debugMessage.text = successConnectMessage;
                _shortWalletAddress.text = shortWalletAddress;

                Debug.Log($"Connected wallet short address: {shortWalletAddress}");

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
            var status = transactionData.IsSuccess;
            var newBalance = transactionData.EndBalance.FromNanoton();
            var fee = transactionData.TotalFees.FromNanoton();
            var sendedAmount = transactionData.OutMessages[0].Value.FromNanoton();
            var recipientAddress = transactionData.OutMessages[0].Recipient.Address;
            var convertedAddress = WalletConnectUtils.GetNonBounceableAddress(recipientAddress);
            var message = transactionData.OutMessages[0].DecodedBody.MessageText;

            _debugMessage.text = $"Loaded transaction data: \n" +
                $"STATUS: {transactionData.IsSuccess},\n" +
                $"HASH: {_latestTransactionHash},\n" +
                $"NEW BALANCE: {newBalance} TON,\n" +
                $"FEE: {fee} TON,\n" +
                $"SENDED AMOUNT: {sendedAmount} TON,\n" +
                $"RECIPIENT ADDRESS: {convertedAddress},\n" +
                $"MESSAGE: {message}";
        }

        private void TonTransactionSendFailed(string errorMessage)
        {
            var message = $"Failed to send transaction, reason: {errorMessage}";

            Debug.LogError(message);

            _debugMessage.text = errorMessage;
        }
    }
}