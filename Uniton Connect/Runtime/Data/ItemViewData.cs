using System;
using UnityEngine;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public abstract class ItemViewData
    {
        public string Name;
        public Texture2D Icon;
    }
}