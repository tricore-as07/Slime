/******************************************************************************/
/*!    \brief  タイトルシーン.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VMUnityLib;

public sealed class UITitle : CmnMonoBehaviour
{
#if USE_TWEEN
    uTweenAlpha tweenAlphe;
#endif

    // 処理なし。メッセージ受信エラー避け.
    protected override void FixedUpdate() { }
    protected override void InitSceneChange() { }
    protected override void OnSceneDeactive() { }

    /// <summary>
    /// 初期化.
    /// </summary>
    public override void Start()
    {
        GameServiceUtil.Auth();
#if USE_TWEEN
        tweenAlphe = GetComponent<uTweenAlpha>();
#endif
    }

    /// <summary>
    /// フェードイン終了.
    /// </summary>
    protected override void OnFadeInEnd()
    {
#if USE_TWEEN
        tweenAlphe.Play(PlayDirection.Forward);

        //if (PlayerPrefs.GetInt(ReviewIndictor.REVIEW_FLAG_NAME, 0) == 0)
        //{
        //    int reviewCnt = PlayerPrefs.GetInt(ReviewIndictor.REVIEW_CNT_NAME, 0);

        //    if (reviewCnt >= 1)
        //    {
        //        PlayerPrefs.SetInt(ReviewIndictor.REVIEW_CNT_NAME, 0);
        //        review.gameObject.SetActive(true);
        //        review.FadeIn();
        //    }
        //    else
        //    {
        //        PlayerPrefs.SetInt(ReviewIndictor.REVIEW_CNT_NAME, reviewCnt + 1);
        //    }
        //}

#if USE_NEND
        NendAdController.Inst.ShowBottomBanner(true);
        NendAdController.Inst.ShowTopBanner(true);
#endif
#endif
    }

    /// <summary>
    /// ゲーム開始.
    /// </summary>
    public void StartGame()
    {
    }

    /// <summary>
    /// リーダーボード表示.
    /// </summary>
    public void ShowLeaderboard()
    {
        GameServiceUtil.Auth();
        GameServiceUtil.ShowLeaderboardUI();
    }

    /// <summary>
    /// 実績表示.
    /// </summary>
    public void ShowAchive()
    {
        GameServiceUtil.Auth();
        GameServiceUtil.ShowAchivementUI();
    }

    ///// <summary>
    ///// ツイッターでシェア.
    ///// </summary>
    //public void ShareOnTwitter()
    //{
    //    ShareHelper.Inst.CaptureScreenShot();
    //    ShareHelper.Inst.ShareOnTwitter(LibBridgeInfo.SHARE_TEXT + LibBridgeInfo.TWITTER_TAG, LibBridgeInfo.APP_URL);
    //}

    ///// <summary>
    ///// フェイスブックでシェア.
    ///// </summary>
    //public void ShareOnFaceBook()
    //{
    //    ShareHelper.Inst.CaptureScreenShot();
    //    ShareHelper.Inst.ShareOnFaceBook();
    //}

    ///// <summary>
    ///// LINEでシェア.
    ///// </summary>
    //public void ShareOnLine()
    //{
    //    ShareHelper.Inst.CaptureScreenShot();
    //    ShareHelper.Inst.ShareOnLine(LibBridgeInfo.SHARE_TEXT + "\n" + LibBridgeInfo.APP_URL);
    //}

    ///// <summary>
    ///// インスタグラムでシェア.
    ///// </summary>
    //public void ShareOnInstagram()
    //{
    //    ShareHelper.Inst.CaptureScreenShot();
    //    ShareHelper.Inst.ShareOnInstagram();
    //}
}
