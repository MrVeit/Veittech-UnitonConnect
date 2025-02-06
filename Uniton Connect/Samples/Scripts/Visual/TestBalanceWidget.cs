using System;
using UnityEngine;
using TMPro;
using UnitonConnect.DeFi;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestBalanceWidget : MonoBehaviour
    {
        [SerializeField, Space] private TextMeshProUGUI _balanceBar;

        private UserAssets.Jetton _jettonAssets;

        private string _contractAddress;

        private void OnDestroy()
        {
            _jettonAssets.OnBalanceLoaded -= JettonBalanceLoaded;
        }

        public void Init(UserAssets.Jetton jettonAssets, string address)
        {
            _contractAddress = address;
            _jettonAssets = jettonAssets;

            _jettonAssets.OnBalanceLoaded += JettonBalanceLoaded;

            LoadBalance();
        }

        public void LoadBalance()
        {
            _jettonAssets.GetBalance(_contractAddress);
        }

        private void JettonBalanceLoaded(decimal balance, 
            string jettonName, string masterAddress)
        {
            if (masterAddress != _contractAddress)
            {
                Debug.LogWarning($"Received balance of " +
                    $"non-target name jetton: {jettonName}");

                return;
            }

            var shortedBalance = Math.Round(balance, 2);

            _balanceBar.text = $"{shortedBalance}";

            Debug.Log($"Loaded jetton balance by name: " +
                $"{jettonName}, value: {shortedBalance}");
        }
    }
}