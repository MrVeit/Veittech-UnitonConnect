using System;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnitonConnect.Core;
using UnitonConnect.Core.Data;
using UnitonConnect.Core.Utils;
using UnitonConnect.Core.Utils.Debugging;

namespace UnitonConnect.ThirdParty
{
    internal static class TonCenterApiBridge
    {
        private const string API_URL = "https://tonapi.io/v2";

        private static UnitonConnectSDK UNITON_CONNECT => UnitonConnectSDK.Instance;

        internal sealed class NFT
        {

        }

        internal sealed class Jetton
        {
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

                using (UnityWebRequest request = UnityWebRequest.Get(GetJettonWalletUlr(tonAddress)))
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

            internal static string GetJettonWalletUlr(string tonAddress)
            {
                return $"https://toncenter.com/api/v3/jetton/wallets?owner_address={tonAddress}" +
                    $"&exclude_zero_balance=false&limit=50&offset=0";
            }
        }
    }
}