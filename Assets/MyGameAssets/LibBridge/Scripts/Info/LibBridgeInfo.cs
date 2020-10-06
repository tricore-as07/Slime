/******************************************************************************/
/*!    \brief  ライブラリ群からも参照される、ゲームの固有パラメータ.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VMUnityLib;

public static class LibBridgeInfo
{
    public const float FIXED_SCREEN_W = 960.0f;
    public const int   FIXED_SCREEN_WI = (int)FIXED_SCREEN_W;
    public const float FIXED_SCREEN_H = 640.0f;
    public const int   FIXED_SCREEN_HI = (int)FIXED_SCREEN_H;

    // 共用のWait.
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();


    // フェード所要時間.
    public const float NORMAL_FADEIN_TIME = 0.3f;
    public const float NORMAL_FADEOUT_TIME = 0.5f;

    // フェードのデフォルトパラメータ.
    public static readonly SceneManager.SceneChangeFadeParam DefaultSceneChangeFadeParam = new SceneManager.SceneChangeFadeParam(NORMAL_FADEOUT_TIME, NORMAL_FADEIN_TIME, CmnFadeManager.FadeType.FADE_NORMAL, new Color(1, 1, 1, 0.5f), LibBridgeInfo.LoadingType.COMMON);

    // アプリURL.
    public const string APP_URL_IOS = "https://goo.gl/GKxFDL";
    public const string APP_URL_ANDROID = "http://goo.gl/8RQZbK";
#if UNITY_IPHONE
    public static string APP_URL
    {
        get
        {
            return APP_URL_IOS;
        }
    }
#else
    public static string APP_URL
    {
        get
        {
            return APP_URL_ANDROID;
        }
    }
#endif

    // レビュー用url.
#if UNITY_IPHONE
    //public const string REVIEW_URL = "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=xxxxxxxxxxxxx";
    public const string REVIEW_URL = "itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=1127234473";
#else
    //public const string REVIEW_URL = "market://details?id=xxxxxxxxxxxxxxxx";
    public const string REVIEW_URL = "http://play.google.com/store/apps/details?id=com.vikingmaxx.btmanKick";
#endif

    // シェア用テキスト.
    public static string SHARE_TEXT
    {
        get
        {
            if (Application.systemLanguage == SystemLanguage.Japanese)
            {
                return "田園の救世主!?害虫どもを必殺キックで撃退！";
            }
            else
            {
                return "A HERO of rice paddy!? Let's use HISSATSU KICK to defeat pest insects!";
            }
        }
    }

    // ツイッター用タグ.
    public const string TWITTER_TAG = " #BattaManKick";

    /// <summary>
    /// アンカー名を定数で管理するクラス
    /// </summary>
    public static class AnchorName
    {
        public const string DEBUG_ROOT = SceneName.debugSceneRoot;    // デバッグルート(コレだけ特殊ルールでシーン名).
        public const string TITLE = "title";                     // タイトル.
    }

    public enum LoadingType
    {
        COMMON,     // 共通.
        BATTLE      // バトル用.
    }

    /// <summary>
    /// デバッグメニューのページ.
    /// </summary>
    public static List<DebugMenuPage> GetDebugMenuPages()
    {
        List<DebugMenuPage> pages = new List<DebugMenuPage>();
        pages.Add(new DebugMenuPageGeneral());
        pages.Add(new DebugMenuPageBattle());
        return pages;
    }
}
