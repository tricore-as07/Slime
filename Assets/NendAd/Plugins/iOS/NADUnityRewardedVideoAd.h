//
//  NADUnityRewardedVideoAd.h
//  Unity-iPhone
//

#import <NendAd/NADRewardedVideo.h>
#import <NendAd/NADVideo.h>

#import "NADUnityVideoAd.h"

typedef const void *NendIOSRewardItemPtr;
typedef void (*NendUnityRewardedVideoAdCallback)(NendUnityVideoAdPtr unityPtr, NSInteger result, const char* currencyName, NSInteger currencyAmount);

extern "C" {
    NendIOSVideoAdPtr _CreateRewardedVideoAd(int spotId, const char* apiKey);
    void _LoadRewardedVideoAd(NendUnityVideoAdPtr unityPtr, NendIOSVideoAdPtr iOSPtr, NendUnityVideoAdNormalCallback callback, NendUnityVideoAdErrorCallback errorCallback);
    int _AdTypeRewarded(NendIOSVideoAdPtr iOSPtr);
    bool _IsLoadedRewarded(NendIOSVideoAdPtr iOSPtr);
    void _ShowRewardedVideoAd(NendIOSVideoAdPtr iOSPtr, NendUnityRewardedVideoAdCallback rewardCallback);
    void _ReleaseRewardedVideoAd(NendIOSVideoAdPtr iOSPtr);
    void _SetRewardedMediationName (NendIOSVideoAdPtr iOSPtr, const char* mediationName);
    void _SetRewardedUserId (NendIOSVideoAdPtr iOSPtr, const char* userId);
    void _SetRewardedUserFeature (NendIOSVideoAdPtr iOSPtr, NendUnityUserFeaturePtr iOSUserFeaturePtr);
    void _SetRewardedLocationEnabled (NendIOSVideoAdPtr iOSPtr, BOOL enabled);
    void _SetRewardedOutputLog (BOOL isOutputLog);
    void _DisposeRewardedVideoAd(NendIOSVideoAdPtr iOSPtr);
}
