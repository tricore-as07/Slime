namespace NendUnityPlugin.Platform
{
	internal interface NendAdInterstitialInterface
	{
		void LoadInterstitialAd (int spotId, string apiKey, bool isOutputLog);

		void ShowInterstitialAd (int spotId);

		void DismissInterstitialAd ();

		void SetAutoReloadEnabled (bool enabled);
	}
}
