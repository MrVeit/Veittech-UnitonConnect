using UnityEngine;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        public sealed override void OnClick()
        {
            decimal gasFee = (decimal)0.03f;
            var gasFeeInNano = $"{gasFee.ToNanoton()}";

            var jettonWalletContract = "0:a6fc37ba5d8816e5e87506d97a5df9518b67f9c7271b11cd5b5e5421ce73213e"; // MY USDT JETTON WALLET
            var payload = "te6cckEBAQEAVwAAqg+KfqUABil93n2F+CE4iAGYhdKhDwUzNQ5RSpcVtByL7Sa6J5vkTuSQ39mTVQx" +
                "f6wAwdqdIh2HAyt2KtZHZYorURWjivSyMp1IJPsly2V5nNkcxLQAuWkzP";

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