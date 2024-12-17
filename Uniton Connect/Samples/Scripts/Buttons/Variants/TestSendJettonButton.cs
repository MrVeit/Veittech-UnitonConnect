using UnityEngine;
using UnitonConnect.Core.Utils;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        public sealed override void OnClick()
        {
            decimal gasFee = (decimal)0.05f;
            var gasFeeInNano = $"{gasFee.ToNanoton()}";

            var jettonMasterAddress = "EQCxE6mUtQJKFnGfaROTKOt1lZbDiiX1kCixRv7Nw2Id_sDs"; // USDT JETTON
            var payload = "te6cckEBAQEAWQAArg+KfqUAAAAAAAAAADB6EggBmIXSoQ8FMzUOUUqXFbQci" +
                "+0muieb5E7kkN/Zk1UMX+sAKb8N7pdiBbl6HUG2Xpd+VGLZ/nHJxsRzVteVCHOcyE+IBfXhAB1EkPQ=";

            TonConnectBridge.SendJetton(jettonMasterAddress, gasFeeInNano, 
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