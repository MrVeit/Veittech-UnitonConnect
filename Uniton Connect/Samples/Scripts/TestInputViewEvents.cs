using System;

namespace UnitonConnect.Core.Demo
{
    public static class TestInputViewEvents
    {
        public static event Action<string> OnNftItemSelected;

        public static void NftItemSelected(string itemAddress)
        {
            OnNftItemSelected?.Invoke(itemAddress);
        }
    }
}