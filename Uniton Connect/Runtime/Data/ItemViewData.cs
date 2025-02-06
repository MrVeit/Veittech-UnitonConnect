using System;
using UnityEngine;

namespace UnitonConnect.Core.Data
{
    [Serializable]
    public abstract class ItemViewData
    {
        public string Name { get; set; }
        public string NFTAddress { get; set; }

        public Texture2D Icon { get; set; }
    }
}