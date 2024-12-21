using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.ThirdParty
{
    internal static class TonCenterApiBridge
    {
        private const string API_URL = "https://toncenter.com/api";

        internal sealed class NFT
        {

        }

        internal sealed class Jetton
        {
            internal static IEnumerator GetJettonWalletByOwner(string tonAddress,
                string jettonMasterAddress, Action<JettonWalletsListData> jettonWalletsLoaded)
            {
                if (string.IsNullOrEmpty(tonAddress))
                {
                    UnitonConnectLogger.LogWarning("Loading jetton wallets " +
                        "failed, ton address is required");

                    jettonWalletsLoaded?.Invoke(null);

                    yield break;
                }

                var targetUrl = GetJettonWalletUlr(tonAddress, jettonMasterAddress);

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
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

                    var errorData = JsonConvert.DeserializeObject<TonCenterResponseErrorData>(responseResult);

                    UnitonConnectLogger.LogError($"Failed to parsed jetton wallets, reason: {errorData.Message}");

                    jettonWalletsLoaded?.Invoke(null);
                }
            }

            internal static IEnumerator GetLastTransactions(string ownerAddress,
                string directionTag, int limit, Action<List<JettonTransactionData>> transactionsLoaded)
            {
                var targetUrl = GetLastJettonTransactionsUrl(ownerAddress, directionTag, limit);

                using (UnityWebRequest request = UnityWebRequest.Get(targetUrl))
                {
                    yield return request.SendWebRequest();

                    var responseResult = request.downloadHandler.text;

                    UnitonConnectLogger.Log($"Parsed response for loading " +
                        $"last jetton transactions: {responseResult}");

                    if (request.result == WebRequestUtils.SUCCESS)
                    {
                        var loadedTransactionsList = JsonConvert.DeserializeObject<JettonTransactionsListData>(responseResult);

                        if (loadedTransactionsList.Transfers == null || 
                            loadedTransactionsList.Transfers.Count == 0)
                        {
                            UnitonConnectLogger.Log($"Jetton transactions is not detected at wallet: {ownerAddress}");

                            transactionsLoaded?.Invoke(null);

                            yield break;
                        }

                        transactionsLoaded?.Invoke(loadedTransactionsList.Transfers);

                        yield break;
                    }

                    var errorData = JsonConvert.DeserializeObject<TonCenterResponseErrorData>(responseResult);

                    UnitonConnectLogger.LogError($"Failed to load last jetton transactions, reason: {errorData.Message}");

                    transactionsLoaded?.Invoke(null);
                }
            }

            internal static string GetJettonWalletUlr(string tonAddress, string jettonMaster)
            {
                return $"{API_URL}/v3/jetton/wallets?owner_address={tonAddress}&" +
                    $"jetton_address={jettonMaster}&exclude_zero_balance=false&limit=50&offset=0";
            }

            internal static string GetLastJettonTransactionsUrl(string ownerAddress,
                string directionTag, int limit)
            {
                return $"{API_URL}/v3/jetton/transfers?owner_address={ownerAddress}" +
                    $"&direction={directionTag}&limit={limit}&offset=0";
            }
        }
    }
}