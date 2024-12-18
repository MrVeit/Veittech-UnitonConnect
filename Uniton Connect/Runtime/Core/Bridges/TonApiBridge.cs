using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnitonConnect.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Runtime.Data;
using UnitonConnect.Editor.Common;
using System.Text;

namespace UnitonConnect.ThirdParty.TonAPI
{
    internal static class TonApiBridge
    {
        private const string API_URL = "https://tonapi.io/v2";

        private static UnitonConnectSDK UNITON_CONNECT => UnitonConnectSDK.Instance;
        private static string _walletAddress => UNITON_CONNECT.Wallet.ToString();

        internal static IEnumerator GetBalance(Action<long> walletBalanceClaimed)
        {
            if (!UNITON_CONNECT.IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning("Failed to request the balance," +
                    " connect the wallet and repeat the operation later");

                walletBalanceClaimed?.Invoke(0);

                yield break;
            }

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

        internal static IEnumerator GetJettonWalletByOwner(string tonAddress,
            Action<JettonWalletsListData> jettonWalletsLoaded)
        {
            if (string.IsNullOrEmpty(tonAddress))
            {
                UnitonConnectLogger.LogWarning("Loading jetton wallets " +
                    "failed, ton address is required");

                jettonWalletsLoaded?.Invoke(null);

                yield break;
            }

            var url = $"https://toncenter.com/api/v3/jetton/wallets?owner_address={tonAddress}";

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                var responseResult = request.downloadHandler.text;

                UnitonConnectLogger.Log($"Parsed jetton wallets response: {responseResult}");

                if (request.result == WebRequestUtils.SUCCESS)
                {
                    var jettonWalletsData = JsonConvert.DeserializeObject<JettonWalletsListData>(responseResult);
                
                    if (jettonWalletsData == null || jettonWalletsData.JettonWallets.Count == 0)
                    {
                        UnitonConnectLogger.Log($"Jetton wallets is not exist by address: {tonAddress}");

                        jettonWalletsLoaded?.Invoke(null);

                        yield break;
                    }

                    jettonWalletsLoaded?.Invoke(jettonWalletsData);

                    yield break;
                }

                var errorData = JsonConvert.DeserializeObject<TonCenterErrorData>(responseResult);

                UnitonConnectLogger.LogError($"Failed to parsed jetton wallets, reason: {errorData.Message}");

                jettonWalletsLoaded?.Invoke(null);
            }
        }

        internal static IEnumerator GetTransactionPayload(string amountInNano,
            string gasFeeInNano, string senderTonAddress, 
            string recipientJettonAddress, Action<string> payloadLoaded)
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
                JettonAmount = amountInNano,
                GasFeeInTon = gasFeeInNano,
                RecipientJettonAddress = recipientJettonAddress,
                SenderTonAddress = senderTonAddress,
            };

            var jsonData = JsonConvert.SerializeObject(payloadData);

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

        internal static IEnumerator GetTransactionData(string transactionHash, float awaitDelay,
            Action<SuccessTransactionData> dataClaimed, Action<string> fetchDataFailed)
        {
            if (!UNITON_CONNECT.IsWalletConnected)
            {
                UnitonConnectLogger.LogWarning("Failed to request the balance," +
                    " connect the wallet and repeat the operation later");

                yield break;
            }

            yield return new WaitForSeconds(awaitDelay);

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

        internal static class NFT
        {
            internal static IEnumerator GetNftCollections(string apiURL,
                Action<NftCollectionData> collectionsClaimed)
            {
                if (!UNITON_CONNECT.IsWalletConnected)
                {
                    UnitonConnectLogger.LogWarning("Failed to load user nft" +
                        " collections, wallet is not connected.");

                    yield break;
                }

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

            internal static string GetTargetNftCollectionUrl(
                string hexAddress, string collectionAddress, int limit, int offset)
            {
                return $"{GetUserWalletUrl(hexAddress)}" +
                    $"/nfts?collection={collectionAddress}&limit={limit}" +
                    $"&offset={offset}&indirect_ownership=false";
            }

            internal static string GetAllNftCollectionsUrl(
                string hexAddress, int limit, int offset)
            {
                return $"{GetUserWalletUrl(hexAddress)}" +
                    $"/nfts?limit={limit}" +
                    $"&offset={offset}&indirect_ownership=false";
            }
        }

        internal sealed class Jetton
        {
            // SOON :D
        }
    }
}