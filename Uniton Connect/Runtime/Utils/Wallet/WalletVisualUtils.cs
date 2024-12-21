using System;
using System.Collections;
using System.Threading.Tasks;
using UnitonConnect.ThirdParty;
using UnityEngine;

namespace UnitonConnect.Core.Utils.View
{
    public static class WalletVisualUtils
    {
        private static readonly UnitonConnectSDK _unitonConnect = UnitonConnectSDK.Instance;

        /// <summary>
        /// Return the first and last characters of the wallet address
        /// </summary>
        /// <param name="address">Address of the connected wallet account</param>
        /// <param name="charAmount">Number of characters to display among the first and last</param>
        [Obsolete("Starting from 0.3.0 version this method is not relevant. Use `UnitonConnectSDK.Instance.Wallet.ToShort()` to get a short address after connection")]
        internal static string ProcessWalletAddress(
            string address, int charAmount)
        {
            if (address.Length < 8)
            {
                return address;
            }

            string firstFourChars = address.Substring(0, charAmount);
            string lastFourChars = address.Substring(address.Length - charAmount);

            return firstFourChars + "..." + lastFourChars;
        }

        internal static async Task<Texture2D> GetWalletIconFromServerAsync(string imageUrl)
        {
            return await TonApiBridge.GetAssetIcon(imageUrl);
        }

        /// <summary>
        /// Get a sprite from a 2D texture
        /// </summary>
        /// <param name="texture">Texture to be converted to a sprite</param>
        public static Sprite GetSpriteFromTexture(Texture2D texture)
        {
            return Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}