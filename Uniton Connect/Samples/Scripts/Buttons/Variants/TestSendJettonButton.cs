using UnitonConnect.Core.Utils;
using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestSendJettonButton : TestBaseButton
    {
        public sealed override void OnClick()
        {
            var sender = UnitonConnectSDK.Instance.Wallet.ToHex();
            var recipient = "UQDPwEk-cnQXEfFaaNVXywpbKACUMwVRupkgWjhr_f4Ursw6";
            var amount = "0.5";

            TonConnectBridge.SendJetton(amount, 
                sender, recipient, (transactionHash) =>
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