using System.Collections.Generic;
using UnityEngine;

namespace UnitonConnect.Core.Demo
{
    public sealed class TestAvailableJettonsBar : TestBasePanel
    {
        [SerializeField, Space] private List<TestSelectableJettonBar> _selectableJettons;

        private void Start()
        {
            foreach (var jettonBar in _selectableJettons)
            {
                jettonBar.Init();
            }
        }
    }
}