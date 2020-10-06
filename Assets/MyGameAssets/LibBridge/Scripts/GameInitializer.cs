/******************************************************************************/
/*!    \brief  ゲーム初期化時の設定を記述する.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using VMUnityLib;

public sealed class GameInitializer : MonoBehaviour 
{
    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        // UnityAds初期化.
        //Advertisement.Initialize("d343800c-f186-4e3f-ba55-0549fc87056a"); ;

        // 画面サイズ情報の初期化.
        GameWindowSize.Init (LibBridgeInfo.FIXED_SCREEN_WI, LibBridgeInfo.FIXED_SCREEN_HI);
        
        // TODO:以下はすべて適切なタイミングで行う.
        // TODO:削除　ダミーのユーザーデータをロードする.
        GameDataManager.Inst.LoadDefaultData();

        // TODO:削除 不変データをロードする.
        GameDataManager.Inst.LoadStaticData();

        // TODO:ローカライズのシステム.
        //Localization.language = "Japanese";
    }

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start () 
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        //Logger.Log("Application.targetFrameRate:" + Application.targetFrameRate + " " + QualitySettings.GetQualityLevel() + " " + QualitySettings.vSyncCount);

        // 画面解像度をLibBridgeInfo.FIXED_SCREEN_Hで固定.
        /*
        float screenRate = (float)LibBridgeInfo.FIXED_SCREEN_H / Screen.height;
        if( screenRate > 1 ) screenRate = 1;
        int width = (int)(Screen.width * screenRate);
        int height = (int)(Screen.height * screenRate);
        Screen.SetResolution( width , height, true);
        */

    }
}
