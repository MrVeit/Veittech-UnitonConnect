using UnityEngine;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        public sealed override void OnClick()
        {
            decimal gasFee = (decimal)0.025f;
            var gasFeeInNano = $"{gasFee.ToNanoton()}";

            var jettonWalletContract = "0:a6fc37ba5d8816e5e87506d97a5df9518b67f9c7271b11cd5b5e5421ce73213e"; // MY USDT JETTON WALLET
            var payload = "te6cckEBAgEAcQABrg+KfqUABil6tYP5UDA9CQgBj4REKcfIvku+vT471N1zL8sxSQGYQ4MvXbMqXb851" +
                "asAMHanSIdhwMrdirWR2WKK1EVo4r0sjKdSCT7JctleZzZIAvrwgQEAKgAAAABBcmUgdSBraWRkaW5nIG1lP7b4WXk=";

            TonConnectBridge.SendJetton(jettonWalletContract, 
                gasFeeInNano, payload, (transactionHash) =>
                {
                    Debug.Log($"[UNITON CONNECT] Jetton transaction successfully sended, hash: {transactionHash}");
                },
                (error) =>
                {
                    Debug.LogError($"[UNITON CONNECT] Failed to send jetton transaction, reason: {error}");
                });
        }
    }
}