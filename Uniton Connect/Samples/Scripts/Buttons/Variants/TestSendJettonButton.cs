using UnityEngine;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        public sealed override void OnClick()
        {
            decimal gasFee = (decimal)0.1f;
            var gasFeeInNano = $"{gasFee.ToNanoton()}";

            var jettonWalletContract = "EQCm_De6XYgW5eh1Btl6XflRi2f5xycbEc1bXlQhznMhPvJZ"; // MY USDT JETTON WALLET
            var payload = "te6cckEBAQEAWQAArg+KfqUAAAAAAAAAADB6EggBmIXSoQ8FMzUOUUqXFbQci+" +
                "0muieb5E7kkN/Zk1UMX+sAKb8N7pdiBbl6HUG2Xpd+VGLZ/nHJxsRzVteVCHOcyE+IC+vCAErLvxQ=";

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