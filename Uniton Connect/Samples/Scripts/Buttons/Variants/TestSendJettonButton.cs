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
            var payload = "te6cckEBAQEAWgAAsJpaofgAAAAAAAAAAEHc1lAIAZ+Aknzk6C4j4" +
                "rTRqq+WFLZQAShmCqN1MkC0cNf7/CldADB2p0iHYcDK3Yq1kdliitRFaOK9LIy" +
                "nUgk+yXLZXmc2SAX14QBAquv5";

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