//
//  NADUnityInterstitialAd.h
//  Unity-iPhone
//

extern "C" {
    void _LoadInterstitialAd(int spotId, const char* apiKey, BOOL isOutputLog);
    void _ShowInterstitialAd(int spotId);
    void _DismissInterstitialAd();
    void _SetAutoReloadEnabled(BOOL enabled);
}
