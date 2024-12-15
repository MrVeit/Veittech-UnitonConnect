using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        public sealed override void OnClick()
        {
            var xEmpireJettonAddress = "EQB4zZusHsbU2vVTPqjhlokIOoiZhEdCMT703CWEzhTOo__X";
            var recipient = "UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6";
            var amount = 1000;
            var gasFee = 0.05f;
            var message = "Sending Jetton by Uniton Connect";

            TonConnectBridge.SendJetton(xEmpireJettonAddress, 
                recipient, amount, (decimal)gasFee, (transactionHash) =>
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