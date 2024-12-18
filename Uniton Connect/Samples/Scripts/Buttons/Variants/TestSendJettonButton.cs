using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnitonConnect.Core.Utils;
using UnitonConnect.ThirdParty.TonAPI;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        [SerializeField, Space] private TMP_InputField _gasFeeBar;
        [SerializeField] private TMP_InputField _amountBar;
        [SerializeField, Space] private TestWalletAddressBarView _walletAddressView;

        private const string USDT_MASTER_WALLET_ADDRESS = "0:b113a994b5024a16719f69139328eb759596c38a25f59028b146fecdc3621dfe";

        public sealed override void OnClick()
        {
            decimal gasFee = decimal.Parse(_gasFeeBar.text);
            var gasFeeInNano = $"{gasFee.ToNanoton()}";

            decimal amount = decimal.Parse(_amountBar.text);
            var amountInNano = $"{amount.ToNanoton()}";

            var hexMasterAddress = USDT_MASTER_WALLET_ADDRESS;

#if UNITY_EDITOR
            var senderAddress = "UQDB2p0iHYcDK3Yq1kdliitRFaOK9LIynUgk-yXLZXmc2V5I";
#else
            var senderAddress = UnitonConnectSDK.Instance.Wallet.ToNonBounceable();
#endif

            string recipientJettonAddress = string.Empty;
            string senderJettonAddress = string.Empty;
            string jettonTransactionPayload = string.Empty;

            Debug.Log($"Parsed gas in nano: {gasFeeInNano}, amount in nano: {amountInNano}, master address: {hexMasterAddress}");

            GetJettonWallet(_walletAddressView.FullAddress, hexMasterAddress, (address) =>
            {
                if (string.IsNullOrEmpty(address))
                {
                    return;
                }

                recipientJettonAddress = address.ToLower();

                Debug.Log($"Parsed recipient jetton address: {recipientJettonAddress}");

                GetJettonWallet(senderAddress, hexMasterAddress, (address) =>
                {
                    if (string.IsNullOrEmpty(address))
                    {
                        return;
                    }

                    senderJettonAddress = address;

                    Debug.Log($"Parsed sender jetton address: {senderJettonAddress}");

                    StartCoroutine(TonApiBridge.GetTransactionPayload(amountInNano, gasFeeInNano,
                    senderAddress, recipientJettonAddress, (transactionPayload) =>
                    {
                        TransactionPayloadParsed(senderJettonAddress, gasFeeInNano, transactionPayload);
                    }));
                });
            });
        }

        private void TransactionPayloadParsed(string senderJettonAddress,
            string gasFeeInNano, string payload)
        {
            if (string.IsNullOrEmpty(payload))
            {
                Debug.LogWarning("Transaction cancelled, uncorrect payload");

                return;
            }

            Debug.Log($"Parsed jetton transaction payload: {payload}");

            TonConnectBridge.SendJetton(senderJettonAddress,
                gasFeeInNano, payload, (transactionHash) =>
            {
                Debug.Log($"[UNITON CONNECT] Jetton transaction successfully sended, hash: {transactionHash}");
            },
            (error) =>
            {
                Debug.LogError($"[UNITON CONNECT] Failed to send jetton transaction, reason: {error}");
            });
        }

        private void GetJettonWallet(string address, string masterAddress, Action<string> addressParsed)
        {
            StartCoroutine(TonApiBridge.GetJettonWalletByOwner(address, (parsedJettonWallets) =>
            {
                Debug.Log($"Master address: {masterAddress}");

                var foundedTargetJettonWallet = parsedJettonWallets.JettonWallets.FirstOrDefault(
                    jettonWallet => jettonWallet.MasterAddress == masterAddress.ToUpper());

                if (string.IsNullOrEmpty(foundedTargetJettonWallet.MasterAddress))
                {
                    Debug.LogWarning("Transaction cancelled, jetton wallet address is not available");

                    return;
                }

                var targetJettonAddress = foundedTargetJettonWallet.Address.ToLower();

                Debug.Log($"Parsed jetton wallet address: {targetJettonAddress} for USDT");

                addressParsed?.Invoke(targetJettonAddress);
            }));
        }
    }
}