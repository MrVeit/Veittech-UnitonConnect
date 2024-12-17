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
            var payload = "te6cckEBAQEAVQAApg+KfqUABil5fEAFQDA9CQgBmIXSoQ8FMzUOUUqXFbQci+0muieb5E7kkN/Zk1UMX" +
                "+sAKb8N7pdiBbl6HUG2Xpd+VGLZ/nHJxsRzVteVCHOcyE+AKzIOag==";

            TonConnectBridge.SendJetton(jettonWalletContract, gasFeeInNano, 
                payload, (transactionHash) =>
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