//
//  NADUnityInterstitialVideoAd.h
//  Unity-iPhone
//

#import <NendAd/NADInterstitialVideo.h>
#import <NendAd/NADVideo.h>

#import "NADUnityVideoAd.h"

extern "C" {
    NendIOSVideoAdPtr _CreateInterstitialVideoAd(int spotId, const char* apiKey);
    void _LoadInterstitialVideoAd(NendUnityVideoAdPtr unityPtr, NendIOSVideoAdPtr iOSPtr, NendUnityVideoAdNormalCallback callback, NendUnityVideoAdErrorCallback errorCallback);
    int _AdTypeInterstitial(NendIOSVideoAdPtr iOSPtr);
    bool _IsLoadedInterstitial(NendIOSVideoAdPtr iOSPtr);
    void _ShowInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr);
    void _ReleaseInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr);
    void _SetInterstitialMediationName (NendIOSVideoAdPtr iOSPtr, const char* mediationName);
    void _SetInterstitialUserId (NendIOSVideoAdPtr iOSPtr, const char* userId);
    void _SetInterstitialUserFeature (NendIOSVideoAdPtr iOSPtr, NendUnityUserFeaturePtr iOSUserFeaturePtr);
    void _SetInterstitialLocationEnabled (NendIOSVideoAdPtr iOSPtr, BOOL enabled);
    void _SetInterstitialOutputLog (BOOL isOutputLog);
    void _AddFallbackFullboard (NendIOSVideoAdPtr iOSPtr, int spotId, const char* apiKey, float r, float g, float b, float a);
    void _SetMuteStartPlaying (NendIOSVideoAdPtr iOSPtr, BOOL mute);
    void _DisposeInterstitialVideoAd(NendIOSVideoAdPtr iOSPtr);
}
