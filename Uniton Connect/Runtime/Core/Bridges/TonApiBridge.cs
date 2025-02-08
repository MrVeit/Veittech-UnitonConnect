using System;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnitonConnect.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Common;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.ThirdParty
{
    internal static class TonApiBridge
    {
        private static UnitonConnectSDK UNITON_CONNECT => UnitonConnectSDK.Instance;

        private static string _walletAddress => UNITON_CONNECT.Wallet.ToString();

        internal async static Task<Texture2D> GetAssetIcon(string imageUrl)
        {
            var dAppData = ProjectStorageConsts.GetRuntimeAppStorage();

            if (string.IsNullOrEmpty(dAppData.Data.ServerApiLink))
            {
                UnitonConnectLogger.LogError("Uniton Connect backend is not " +
                    "connected, image parsing operation on the link is canceled");

                return null;
            }

            string apiUrl = dAppData.Data.ServerApiLink;
            string targetUrl = GetIconConvertURL(apiUrl, imageUrl);

            using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result != WebRequestUtils.SUCCESS)
                {
                    UnitonConnectLogger.LogError($"Failed to parse " +
                        $"item icon, reason: {request.error}");

                    return null;
                }

                byte[] imageData = request.downloadHandler.data;

                Texture2D texture = new(2, 2);

                if (texture.LoadImage(imageData))
                {
                    UnitonConnectLogger.Log($"Loaded image {texture.name} " +
                        $"with size: {texture.width}x{texture.height}");

                    return texture;
                }

                return null;
            }
        }

        internal static IEnumerator GetBalance(Action<long> walletBalanceClaimed)
        {
            var userEncodedAddress = ConvertAddressToEncodeURL(_walletAddress);
            var targetUrl = GetUserTonWalletUrl(userEncodedAddress);

            using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
            {
                yield return request.SendWebRequest();

                var responseResult = request.downloadHandler.text;

                if (request.result != WebRequestUtils.SUCCESS)
                {
                    var responseData = JsonConvert.DeserializeObject<
                        TonApiResponseErrorData>(responseResult);

                    UnitonConnectLogger.LogError($"Failed to request wallet " +
                        $"address data, possible reason: {responseData.Message}");

                    walletBalanceClaimed?.Invoke(0);

                    yield break;
                }

                var jsonResult = request.downloadHandler.text;
                var data = JsonConvert.DeserializeObject<UserAccountData>(jsonResult);

                walletBalanceClaimed?.Invoke(data.Balance);

                UnitonConnectLogger.Log($"Current toncoin balance " +
                    $"by address {_walletAddress} in nanotons: {data.Balance}");

                yield break;
            }
        }

        internal static IEnumerator GetTransactionData(string transactionHash,
            Action<SuccessTransactionData> dataClaimed, Action<string> fetchDataFailed)
        {
            var encodedTransactionHash = EscapeQueryParam(transactionHash);
            var targetUrl = GetTransactionDataUrl(encodedTransactionHash);

            using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
            {
                yield return request.SendWebRequest();

                var responseResult = request.downloadHandler.text;

                if (request.result != WebRequestUtils.SUCCESS)
                {
                    var responseData = JsonConvert.DeserializeObject<
                        TonApiResponseErrorData>(responseResult);

                    UnitonConnectLogger.LogError($"Failed to fetch transaction data" +
                        $" with hash: {transactionHash}, reason: {responseData.Message}");

                    fetchDataFailed?.Invoke(responseData.Message);

                    yield break;
                }

                var transactionData = JsonConvert.DeserializeObject<
                    SuccessTransactionData>(responseResult);

                UnitonConnectLogger.Log($"Claimed transaction data with hash: " +
                    $"{transactionHash}, data: {responseResult}");

                dataClaimed?.Invoke(transactionData);

                yield break;
            }
        }

        internal static string ConvertAddressToEncodeURL(string address)
        {
            return EscapeQueryParam(WalletConnectUtils.GetHEXAddress(address));
        }

        private static string EscapeQueryParam(string value)
        {
            return Uri.EscapeDataString(value);
        }

        private static string GetUserTonWalletUrl(string hexAddress)
        {
            return $"https://tonapi.io/v2/accounts/{hexAddress}";
        }

        private static string GetTransactionDataUrl(string transactionHash)
        {
            return $"https://tonapi.io/v2/blockchain/transactions/{transactionHash}";
        }

        internal static string GetIconConvertURL(string apiUrl, string iconUrl)
        {
            return $"{apiUrl}/api/uniton-connect/v1/assets/" +
                $"item-icon?url={UnityWebRequest.EscapeURL(iconUrl)}";
        }

        internal static class NFT
        {
            internal static IEnumerator GetCollections(string collectionAddress, 
                int limit, int offset, Action<NftCollectionData> collectionsClaimed)
            {
                var apiUrl = ProjectStorageConsts.GetRuntimeAppStorage().Data.ServerApiLink;

                if (string.IsNullOrEmpty(apiUrl))
                {
                    UnitonConnectLogger.LogError("Uniton Connect backend is not connected, " +
                        "the nft collections download operation on the connected wallet is canceled.");

                    collectionsClaimed?.Invoke(null);

                    yield break;
                }

                var encodedWalletAddress = ConvertAddressToEncodeURL(_walletAddress);

                string targetUrl = GetAllCollectionsUrl(
                    apiUrl, encodedWalletAddress, limit, offset);

                if (!string.IsNullOrEmpty(collectionAddress))
                {
                    var encodedCollectionAddress = ConvertAddressToEncodeURL(collectionAddress);

                    targetUrl = GetTargetCollectionUrl(apiUrl, encodedWalletAddress, 
                        encodedCollectionAddress, limit, offset);
                }

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
                {
                    yield return request.SendWebRequest();

                    var responseData = request.downloadHandler.text;

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        var errorData = JsonConvert.DeserializeObject<
                            ServerResponseData>(responseData);

                        UnitonConnectLogger.LogError($"Failed to request " +
                            $"nft collections, reason: {errorData.Message}");

                        collectionsClaimed?.Invoke(null);

                        yield break;
                    }

                    var data = JsonConvert.DeserializeObject<NftCollectionData>(responseData);

                    collectionsClaimed?.Invoke(data);

                    UnitonConnectLogger.Log($"Nft collections loaded: {responseData}");

                    yield break;
                }
            }

            internal static IEnumerator GetTransactionPayload(string recipient,
                string sender, Action<string> payloadLoaded)
            {
                var apiUrl = ProjectStorageConsts.GetRuntimeAppStorage().Data.ServerApiLink;

                if (string.IsNullOrEmpty(apiUrl))
                {
                    UnitonConnectLogger.LogError("Uniton Connect backend is not connected, " +
                        "payload generation operation for NFT transactions is canceled");

                    payloadLoaded?.Invoke(null);

                    yield break;
                }

                var payloadData = new NftTransactionPayloadData()
                {
                    Recipient = recipient,
                    Sender = sender,
                };

                var jsonData = JsonConvert.SerializeObject(payloadData);
                var targetUrl = GetTransactionPayloadUrl(apiUrl);

                UnitonConnectLogger.Log($"Nft transaciton data before create payload: {jsonData}");

                using (UnityWebRequest request = new(targetUrl, UnityWebRequest.kHttpVerbPOST))
                {
                    var bodyRaw = Encoding.UTF8.GetBytes(jsonData);

                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    request.downloadHandler = new DownloadHandlerBuffer();

                    WebRequestUtils.SetRequestHeader(request,
                        WebRequestUtils.HEADER_CONTENT_TYPE,
                        WebRequestUtils.HEADER_VALUNE_CONTENT_TYPE_JSON);

                    yield return request.SendWebRequest();

                    var responseData = request.downloadHandler.text;

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        var errorData = JsonConvert.DeserializeObject<
                             ServerResponseData>(responseData);

                        UnitonConnectLogger.LogError($"Failed to create NFT " +
                            $"transaction payload, reason: {errorData.Message}");

                        payloadLoaded?.Invoke(null);

                        yield break;
                    }

                    var loadedData = JsonConvert.DeserializeObject<
                        LoadedTransactionPayloadData>(responseData);

                    UnitonConnectLogger.Log($"NFT transaction " +
                        $"payload created: {loadedData.Payload}");

                    payloadLoaded?.Invoke(loadedData.Payload);

                    yield break;
                }
            }

            internal static string GetTransactionPayloadUrl(string apiUrl)
            {
                return $"{apiUrl}/api/uniton-connect/v1/assets/nft/payload";
            }

            internal static string GetTargetCollectionUrl(string apiUrl, 
                string hexAddress, string collectionAddress, int limit, int offset)
            {
                return $"{apiUrl}/api/uniton-connect/v1/account/{hexAddress}/" +
                    $"assets/nft/{collectionAddress}/limit/{limit}/offset/{offset}";
            }

            internal static string GetAllCollectionsUrl(string apiUrl,
                string hexAddress, int limit, int offset)
            {
                return $"{apiUrl}/api/uniton-connect/v1/account/{hexAddress}/" +
                    $"assets/nft/limit/{limit}/offset/{offset}";
            }
        }

        internal sealed class Jetton
        {
            internal static IEnumerator GetBalance(string tonAddress,
                string masterJettonAddress, Action<JettonBalanceData> jettonBalanceLoaded)
            {
                var apiUrl = ProjectStorageConsts.GetRuntimeAppStorage().Data.ServerApiLink;

                if (string.IsNullOrEmpty(apiUrl))
                {
                    UnitonConnectLogger.LogError("Uniton Connect backend is not connected, " +
                        "operation of receiving token wallet balance at master address is canceled.");

                    jettonBalanceLoaded?.Invoke(null);

                    yield break;
                }

                var targetUrl = GetBalanceUrl(apiUrl, tonAddress, masterJettonAddress);

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
                {
                    yield return request.SendWebRequest();

                    var responseData = request.downloadHandler.text;

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        var errorData = JsonConvert.DeserializeObject<
                            ServerResponseData>(responseData);

                        UnitonConnectLogger.LogError($"Failed to fetch " +
                            $"jetton balance, reason: {errorData.Message}");

                        jettonBalanceLoaded?.Invoke(null);

                        yield break;
                    }

                    var loadedWallet = JsonConvert.DeserializeObject<JettonConfigData>(responseData);

                    var jettonBalanceConfig = loadedWallet.JettonConfig;

                    jettonBalanceLoaded?.Invoke(jettonBalanceConfig);

                    UnitonConnectLogger.Log($"Loaded jetton {jettonBalanceConfig.Configuration.Name} " +
                        $"with balance in nano: {jettonBalanceConfig.BalanceInNano}");

                    yield break;
                }
            }

            internal static IEnumerator GetTransactionPayload(JettonTypes jettonType,
                decimal amount, decimal forwardFee, string senderTonAddress,
                string recipientTonAddress, string message, Action<string> payloadLoaded)
            {
                var apiUrl = ProjectStorageConsts.GetRuntimeAppStorage().Data.ServerApiLink;

                if (string.IsNullOrEmpty(apiUrl))
                {
                    UnitonConnectLogger.LogWarning("Uniton Connect backend is not connected, " +
                        "the payload generation operation for the transaction token is canceled");

                    payloadLoaded?.Invoke(null);

                    yield break;
                }

                var payloadData = new JettonTransactionPayloadData()
                {
                    Amount = amount,
                    GasFeeInTon = forwardFee,
                    RecipientTonAddress = recipientTonAddress,
                    SenderTonAddress = senderTonAddress,
                    ShortName = jettonType.ToString(),
                    Comment = message
                };

                var jsonData = JsonConvert.SerializeObject(payloadData);
                var targetUrl = GetTransactionPayloadUrl(apiUrl);

                UnitonConnectLogger.Log($"Jetton transaciton data " +
                    $"before create payload: {jsonData}");

                using (UnityWebRequest request = new(targetUrl, UnityWebRequest.kHttpVerbPOST))
                {
                    var bodyRaw = Encoding.UTF8.GetBytes(jsonData);

                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    request.downloadHandler = new DownloadHandlerBuffer();

                    WebRequestUtils.SetRequestHeader(request,
                        WebRequestUtils.HEADER_CONTENT_TYPE,
                        WebRequestUtils.HEADER_VALUNE_CONTENT_TYPE_JSON);

                    yield return request.SendWebRequest();

                    var responseData = request.downloadHandler.text;

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        var errorData = JsonConvert.DeserializeObject<
                            ServerResponseData>(responseData);

                        UnitonConnectLogger.LogError($"Failed to create " +
                            $"transaction payload, reason: {errorData.Message}");

                        payloadLoaded?.Invoke(null);

                        yield break;
                    }

                    var loadedData = JsonConvert.DeserializeObject<
                        LoadedTransactionPayloadData>(responseData);

                    UnitonConnectLogger.Log($"Jetton transaction " +
                        $"payload created: {loadedData.Payload}");

                    payloadLoaded?.Invoke(loadedData.Payload);

                    yield break;
                }
            }

            internal static string GetBalanceUrl(string apiUrl,
                string tonAddress, string masterJettonAddress)
            {
                return $"{apiUrl}/api/uniton-connect/v1/account/{tonAddress}" +
                    $"/assets/jetton/{masterJettonAddress}/balance";
            }

            internal static string GetTransactionPayloadUrl(string apiUrl)
            {
                return $"{apiUrl}/api/uniton-connect/v1/assets/jetton/payload";
            }
        }
    }
}