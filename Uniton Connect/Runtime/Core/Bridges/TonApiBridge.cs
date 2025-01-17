using System;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnitonConnect.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.ThirdParty
{
    internal static class TonApiBridge
    {
        private const string API_URL = "https://tonapi.io/v2";

        private static UnitonConnectSDK UNITON_CONNECT => UnitonConnectSDK.Instance;

        private static string _walletAddress => UNITON_CONNECT.Wallet.ToString();

        internal async static Task<Texture2D> GetAssetIcon(string imageUrl)
        {
            var dAppData = ProjectStorageConsts.GetRuntimeAppStorage();

            if (string.IsNullOrEmpty(dAppData.Data.ServerApiLink))
            {
                UnitonConnectLogger.LogError("For loading nft or wallet icon from the cache storage," +
                    " you need to run the API Server to successfully convert to the desired format");

                return null;
            }

            string apiUrl = GetIconConvertURL(imageUrl);

            Debug.Log($"APi url response: {apiUrl}");

            using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result != WebRequestUtils.SUCCESS)
                {
                    UnitonConnectLogger.LogError($"Failed to load image by api server: {request.error}");

                    return null;
                }

                byte[] imageData = request.downloadHandler.data;

                Texture2D texture = new(2, 2);

                if (texture.LoadImage(imageData))
                {
                    UnitonConnectLogger.Log($"Loaded image {texture.name} with sise: {texture.width}x{texture.height}");

                    return texture;
                }

                return null;
            }
        }

        internal static IEnumerator GetBalance(Action<long> walletBalanceClaimed)
        {
            var userEncodedAddress = ConvertAddressToEncodeURL(_walletAddress);
            var targetUrl = GetUserWalletUrl(userEncodedAddress);

            using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
            {
                yield return request.SendWebRequest();

                if (request.result != WebRequestUtils.SUCCESS)
                {
                    UnitonConnectLogger.LogError($"Failed to request wallet address data," +
                        $" possible reason: {request.error}");

                    yield break;
                }

                var jsonResult = request.downloadHandler.text;
                var data = JsonConvert.DeserializeObject<UserAccountData>(jsonResult);

                walletBalanceClaimed?.Invoke(data.Balance);

                UnitonConnectLogger.Log($"Current TON balance in nanotons: {data.Balance}");
            }
        }

        internal static IEnumerator GetTransactionPayload(decimal amount,
            decimal forwardFee, string senderTonAddress, 
            string recipientTonAddress, Action<string> payloadLoaded)
        {
            var apiUrl = ProjectStorageConsts.GetRuntimeAppStorage().Data.ServerApiLink;
            
            if (string.IsNullOrEmpty(apiUrl))
            {
                UnitonConnectLogger.LogWarning("Server API url is not detected");

                payloadLoaded?.Invoke(null);

                yield break;
            }

            var responseUrl = $"{apiUrl}/api/uniton-connect/v1/assets/jetton/payload";

            var payloadData = new TransactionPayloadComponentsData()
            {
                JettonAmount = amount,
                GasFeeInTon = forwardFee,
                RecipientJettonAddress = recipientTonAddress,
                SenderTonAddress = senderTonAddress,
            };

            var jsonData = JsonConvert.SerializeObject(payloadData);

            Debug.Log($"Transaciton data before create payload: {jsonData}");

            using (UnityWebRequest request = new(responseUrl, UnityWebRequest.kHttpVerbPOST))
            {
                var bodyRaw = Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();

                WebRequestUtils.SetRequestHeader(request, WebRequestUtils.HEADER_CONTENT_TYPE,
                    WebRequestUtils.HEADER_VALUNE_CONTENT_TYPE_JSON);

                yield return request.SendWebRequest();

                var responseData = request.downloadHandler.text;

                if (request.result == WebRequestUtils.SUCCESS)
                {
                    var loadedData = JsonConvert.DeserializeObject<LoadedTransactionPayloadData>(responseData);

                    UnitonConnectLogger.Log($"Jetton transaction payload created: {loadedData.Payload}");

                    payloadLoaded?.Invoke(loadedData.Payload);

                    yield break;
                }

                var errorData = JsonConvert.DeserializeObject<ServerResponseData>(responseData);

                UnitonConnectLogger.LogError($"Failed to create transaction`" +
                    $" payload, reason: {errorData.Message}");

                payloadLoaded?.Invoke(null);
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
                    UnitonConnectLogger.LogError($"Failed to fetch transaction data" +
                        $" with hash: {transactionHash}, reason: {request.error}");

                    var responseData = JsonConvert.DeserializeObject<
                        TonApiResponseErrorData>(responseResult);

                    fetchDataFailed?.Invoke(responseData.Message);

                    yield break;
                }

                var transactionData = JsonConvert.DeserializeObject<
                    SuccessTransactionData>(responseResult);

                UnitonConnectLogger.Log($"Claimed transaction data with hash: " +
                    $"{transactionHash}, data: {responseResult}");

                dataClaimed?.Invoke(transactionData);
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

        private static string GetUserWalletUrl(string hexAddress)
        {
            return $"{API_URL}/accounts/{hexAddress}";
        }

        private static string GetTransactionDataUrl(string transactionHash)
        {
            return $"{API_URL}/blockchain/transactions/{transactionHash}";
        }

        internal static string GetIconConvertURL(string iconUrl)
        {
            var runtimeData = ProjectStorageConsts.GetRuntimeAppStorage().Data;

            string apiUrl = $"{runtimeData.ServerApiLink}" +
                $"/api/uniton-connect/v1/assets/item-icon?url={UnityWebRequest.EscapeURL(iconUrl)}";

            return apiUrl;
        }

        internal static class NFT
        {
            internal static IEnumerator GetCollections(string apiURL,
                Action<NftCollectionData> collectionsClaimed)
            {
                using (UnityWebRequest request = UnityWebRequest.Get(apiURL))
                {
                    yield return request.SendWebRequest();

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        UnitonConnectLogger.LogError($"Failed to request nft collections," +
                            $" possible reason: {request.error}");

                        yield break;
                    }

                    var jsonResult = request.downloadHandler.text;
                    var data = JsonConvert.DeserializeObject<NftCollectionData>(jsonResult);

                    collectionsClaimed?.Invoke(data);

                    UnitonConnectLogger.Log($"Nft collections loaded: {jsonResult}");
                }

                yield break;
            }

            internal static string GetTargetCollectionUrl(
                string hexAddress, string collectionAddress, int limit, int offset)
            {
                return $"{GetUserWalletUrl(hexAddress)}" +
                    $"/nfts?collection={collectionAddress}&limit={limit}" +
                    $"&offset={offset}&indirect_ownership=false";
            }

            internal static string GetAllCollectionsUrl(
                string hexAddress, int limit, int offset)
            {
                return $"{GetUserWalletUrl(hexAddress)}" +
                    $"/nfts?limit={limit}" +
                    $"&offset={offset}&indirect_ownership=false";
            }
        }

        internal sealed class Jetton
        {
            internal static IEnumerator GetBalance(string tonAddress,
                string masterJettonAddress, Action<JettonBalanceData> jettonBalanceLoaded)
            {
                var targetUrl = GetBalanceUrl(tonAddress, masterJettonAddress);

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
                {
                    yield return request.SendWebRequest();

                    var responseJson = request.downloadHandler.text;

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        UnitonConnectLogger.LogError($"Failed to fetch jetton balance, reason: {responseJson}");

                        jettonBalanceLoaded?.Invoke(null);

                        yield break;
                    }

                    var responseData = JsonConvert.DeserializeObject<JettonBalanceData>(responseJson);

                    jettonBalanceLoaded?.Invoke(responseData);

                    UnitonConnectLogger.Log($"Loaded jetton {responseData.Configuration.Name} " +
                        $"with balance in nano: {responseData.BalanceInNano}");

                    yield break;
                }
            }

            internal static string GetBalanceUrl(string tonAddress,
                string masterJettonAddress)
            {
                return $"{API_URL}/accounts/{tonAddress}/jettons/{masterJettonAddress}";
            }
        }
    }
}