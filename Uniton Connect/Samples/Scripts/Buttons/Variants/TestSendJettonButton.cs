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
        [SerializeField] private TMP_InputField _forwardFeeBar;
        [SerializeField, Space] private TMP_InputField _amountBar;
        [SerializeField, Space] private TestWalletAddressBarView _walletAddressView;

        private const string USDT_MASTER_WALLET_ADDRESS = "0:b113a994b5024a16719f69139328eb759596c38a25f59028b146fecdc3621dfe";

        public sealed override void OnClick()
        {
            Debug.Log($"Parsed amount: {_amountBar.text} and fee: {_gasFeeBar.text}");

            decimal gasFee = GetTransactionAmount(_gasFeeBar.text);
            decimal amount = GetTransactionAmount(_amountBar.text);
            decimal forwardFee = GetTransactionAmount(_forwardFeeBar.text);

            var gasFeeInNano = gasFee.ToNanoton();

#if UNITY_EDITOR
            var senderAddress = "0:c1da9d221d87032b762ad647658a2b5115a38af4b2329d4824fb25cb65799cd9";
#else
            var senderAddress = UnitonConnectSDK.Instance.Wallet.ToHex();
#endif

            string recipientJettonAddress = string.Empty;
            string senderJettonAddress = string.Empty;

            Debug.Log($"Parsed gas in nano: {gasFeeInNano}, master address: {USDT_MASTER_WALLET_ADDRESS}");

            GetJettonWallet(_walletAddressView.FullAddress, USDT_MASTER_WALLET_ADDRESS, (recipient) =>
            {
                if (string.IsNullOrEmpty(recipient))
                {
                    return;
                }

                recipientJettonAddress = recipient.ToLower();

                Debug.Log($"Parsed recipient jetton address: {recipientJettonAddress}");

                GetJettonWallet(senderAddress, USDT_MASTER_WALLET_ADDRESS, (sender) =>
                {
                    if (string.IsNullOrEmpty(sender))
                    {
                        return;
                    }

                    senderJettonAddress = sender;

                    Debug.Log($"Parsed sender jetton address: {senderJettonAddress}");

                    var recipientTonHexAddress = WalletConnectUtils.GetHEXAddress(_walletAddressView.FullAddress);

                    StartCoroutine(TonApiBridge.GetTransactionPayload(amount, forwardFee,
                        senderAddress, recipientTonHexAddress, (parsedPayload) =>
                    {
                        if (string.IsNullOrEmpty(parsedPayload))
                        {
                            return;
                        }

                        TransactionPayloadParsed(senderJettonAddress, 
                            $"{gasFeeInNano}", parsedPayload);
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
            Debug.Log($"Transaction data before send, sender jetton address: {senderJettonAddress}," +
                $"gas fee in nano: {gasFeeInNano}, payload: {payload}");

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

        private decimal GetTransactionAmount(string textBar)
        {
            return decimal.Parse(textBar);
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