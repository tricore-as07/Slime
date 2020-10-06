using NendUnityPlugin.Platform;
using System;

namespace NendUnityPlugin.AD.Video
{
	using UnityEngine;

    /// <summary>
    /// Video ad.
    /// </summary>
    public abstract class NendAdInterstitialVideo : NendAdVideo
    {

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="spotId">Spot id.</param>
        /// <param name="apiKey">API key.</param>
        public static NendAdInterstitialVideo NewVideoAd(int spotId, string apiKey)
        {
            return NendAdNativeInterfaceFactory.CreateInterstitialVideoAd(spotId, apiKey);
        }

		[Obsolete ("Use newer one that specified \"spot id\" parameter as int.", false)]
		public static NendAdInterstitialVideo NewVideoAd (string spotId, string apiKey)
		{
			return NewVideoAd (int.Parse (spotId), apiKey);
		}

        /// <summary>
        /// Add fallback fullboard ad.
        /// </summary>
        public void AddFallbackFullboard(int spotId, string apiKey)
        {
            AddFallbackFullboard(spotId, apiKey, Color.black);
        }

		[Obsolete ("Use newer one that specified \"spot id\" parameter as int.", false)]
        public void AddFallbackFullboard(string spotId, string apiKey)
        {
            AddFallbackFullboard(int.Parse (spotId), apiKey);
        }

        /// <summary>
        /// Adds the fallback fullboard.
        /// </summary>
        /// <param name="spotId">Spot id.</param>
        /// <param name="apiKey">API key.</param>
        /// <param name="iOSBackgroundColor">background color.</param>
        public void AddFallbackFullboard(int spotId, string apiKey, Color iOSBackgroundColor)
        {
            AddFallbackFullboardInternal(spotId, apiKey, iOSBackgroundColor.r, iOSBackgroundColor.g, iOSBackgroundColor.b, iOSBackgroundColor.a);
        }

		[Obsolete ("Use newer one that specified \"spot id\" parameter as int.", false)]
        public void AddFallbackFullboard(string spotId, string apiKey, Color iOSBackgroundColor)
        {
            AddFallbackFullboard(int.Parse (spotId), apiKey, iOSBackgroundColor);
        }

        public bool IsMuteStartPlaying
        {
            set {
                SetMuteStartPlayingInternal(value);
            }
        }

        internal abstract void AddFallbackFullboardInternal (int spotId, string apiKey, float r, float g, float b, float a);
        internal abstract void SetMuteStartPlayingInternal(bool mute);
    }
}

