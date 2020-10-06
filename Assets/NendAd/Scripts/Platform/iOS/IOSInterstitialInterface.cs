#if UNITY_IOS
using System.Runtime.InteropServices;

namespace NendUnityPlugin.Platform.iOS
{
	internal class IOSInterstitialInterface : NendAdInterstitialInterface
	{
		public void LoadInterstitialAd (int spotId, string apiKey, bool isOutputLog)
		{
			_LoadInterstitialAd (spotId, apiKey, isOutputLog);
		}

		public void ShowInterstitialAd (int spotId)
		{
			_ShowInterstitialAd (spotId);
		}
			
		public void DismissInterstitialAd ()
		{
			_DismissInterstitialAd ();
		}

		public void SetAutoReloadEnabled (bool enabled)
		{
			_SetAutoReloadEnabled (enabled);
		}

		[DllImport ("__Internal")]
		private static extern void _LoadInterstitialAd (int spotId, string apiKey, bool isOutputLog);

		[DllImport ("__Internal")]
		private static extern void _ShowInterstitialAd (int spotId);

		[DllImport ("__Internal")]
		private static extern void _DismissInterstitialAd ();

		[DllImport ("__Internal")]
		private static extern void _SetAutoReloadEnabled (bool enabled);
	}
}
#endif