using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;
using UnitonConnect.Editor.Common;

namespace UnitonConnect.ThirdParty
{
    internal static class TonCenterApiBridge
    {
        internal sealed class Jetton
        {
            internal static IEnumerator GetJettonWalletByOwner(string tonAddress,
                string jettonMasterAddress, Action<LoadedJettonWalletData> jettonWalletsLoaded)
            {
                var dAppData = ProjectStorageConsts.GetRuntimeAppStorage();

                var apiUrl = dAppData.Data.ServerApiLink;

                if (string.IsNullOrEmpty(apiUrl))
                {
                    UnitonConnectLogger.LogError("Uniton Connect backend is not connected, " +
                        "wallet token loading operation is not available");

                    jettonWalletsLoaded?.Invoke(null);

                    yield break;
                }

                string targetUrl = GetJettonWalletUlr(apiUrl,
                    tonAddress, jettonMasterAddress);

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
                {
                    yield return request.SendWebRequest();

                    var responseResult = request.downloadHandler.text;

                    UnitonConnectLogger.Log($"Parsed result for loading " +
                        $"jetton wallet: {responseResult}");

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        var errorData = JsonConvert.DeserializeObject<
                            ServerResponseData>(responseResult);

                        UnitonConnectLogger.LogError($"Failed to parsed jetton " +
                            $"wallets, reason: {errorData.Message}");

                        jettonWalletsLoaded?.Invoke(null);

                        yield break;
                    }

                    var jettonWalletsData = JsonConvert.DeserializeObject<
                        LoadedJettonWalletData>(responseResult);

                    if (jettonWalletsData == null)
                    {
                        UnitonConnectLogger.LogWarning($"Jetton wallet with master: " +
                            $"{jettonMasterAddress} is not exist by address: {tonAddress}");

                        jettonWalletsLoaded?.Invoke(null);

                        yield break;
                    }

                    jettonWalletsLoaded?.Invoke(jettonWalletsData);

                    yield break;
                }
            }

            internal static IEnumerator GetLastTransactions(string ownerAddress,
                string directionTag, int limit, Action<List<JettonTransactionData>> transactionsLoaded)
            {
                var dAppData = ProjectStorageConsts.GetRuntimeAppStorage();

                var apiUrl = dAppData.Data.ServerApiLink;

                if (string.IsNullOrEmpty(apiUrl))
                {
                    UnitonConnectLogger.LogError("Uniton Connect backend is not connected, " +
                        "the operation of downloading the last successful token transactions is not available");

                    transactionsLoaded?.Invoke(null);

                    yield break;
                }

                var targetUrl = GetLastJettonTransactionsUrl(
                    apiUrl, ownerAddress, directionTag, limit, 0);

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
                {
                    yield return request.SendWebRequest();

                    var responseResult = request.downloadHandler.text;

                    UnitonConnectLogger.Log($"Parsed result for loading " +
                        $"last jetton transactions: {responseResult}");

                    if (request.result != WebRequestUtils.SUCCESS)
                    {
                        var errorData = JsonConvert.DeserializeObject<
                            ServerResponseData>(responseResult);

                        UnitonConnectLogger.LogError($"Failed to load last " +
                            $"jetton transactions, reason: {errorData.Message}");

                        transactionsLoaded?.Invoke(null);

                        yield break;
                    }

                    var loadedTransactionsList = JsonConvert.DeserializeObject<
                        JettonTransactionsListData>(responseResult);

                    if (loadedTransactionsList.Transfers == null ||
                        loadedTransactionsList.Transfers.Count == 0)
                    {
                        UnitonConnectLogger.Log($"Jetton transactions is not " +
                            $"detected at wallet: {ownerAddress}");

                        transactionsLoaded?.Invoke(null);

                        yield break;
                    }

                    transactionsLoaded?.Invoke(loadedTransactionsList.Transfers);

                    yield break;
                }
            }

            internal static string GetJettonWalletUlr(string apiUrl,
                string tonAddress, string jettonMaster)
            {
                return $"{apiUrl}/api/uniton-connect/v1/account/{tonAddress}/" +
                    $"assets/jetton/{jettonMaster}/limit/1/offset/0/wallet";
            }

            internal static string GetLastJettonTransactionsUrl(string apiUrl,
                string ownerAddress, string directionTag, int limit, int offset)
            {
                return $"{apiUrl}/api/uniton-connect/v1/account/{ownerAddress}/" +
                    $"assets/jetton/transactions/{directionTag}/limit/{limit}/offset/{offset}";
            }
        }
    }
}