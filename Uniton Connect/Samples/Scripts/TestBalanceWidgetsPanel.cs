using System.Collections.Generic;
using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestBalanceWidgetsPanel : TestBasePanel
    {
        [SerializeField, Space] private TestWalletInterfaceAdapter _uiController;
        [SerializeField, Space] private List<TestBalanceWidget> _widgets;

        public void Init()
        {
            var jettonsStorage = _uiController
                .UnitonSDK.JettonStorage.Jettons;
            var jettonWallet = _uiController.JettonModule;

            for (var i = 0; i < _widgets.Count; i++)
            {
                var masterAddress = jettonsStorage[i].MasterAddress;

                _widgets[i].Init(jettonWallet, masterAddress);
            }
        }

        public void UpdateBalances()
        {
            foreach (var widget in _widgets)
            {
                widget.LoadBalance();
            }
        }
    }
}