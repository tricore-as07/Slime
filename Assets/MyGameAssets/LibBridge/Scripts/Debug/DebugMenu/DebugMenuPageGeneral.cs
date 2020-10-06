/******************************************************************************/
/*!    \brief  デバッグメニューページ：一般.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using VMUnityLib;

public class DebugMenuPageGeneral : DebugMenuPage 
{
    public override string GetPageName() { return "一般"; } // ページ名.
    
    /// <summary>
    /// 描画本体.
    /// </summary>
    /// <param name="startY">描画開始Y座標.</param>
    public override bool OnGUI( float startY )
    {
        bool closeRequest = false;
        float x = 5; 
        float y = startY;
        float h = 40;
        float w = 400;
        if(GUI.Button(GUIHelper.GetScaledRect(x,y,w,h) , "デバッグルートシーンにもどる"))
        {
            SceneManager.SceneChangeFadeParam noTimeFade = new SceneManager.SceneChangeFadeParam(0, 0, CmnFadeManager.FadeType.FADE_TIMEONLY, new Color(0, 0, 0, 0), LibBridgeInfo.LoadingType.COMMON);
            SceneManager.Instance.PopSceneToAnchor(LibBridgeInfo.AnchorName.DEBUG_ROOT, noTimeFade, SceneManager.Instance.UnloadAllOtherScene);

            closeRequest = true;
        }
        y += h + 10;
        if (GUI.Button(GUIHelper.GetScaledRect(x, y, w, h), "タイトルにもどる"))
        {
            SceneManager.Instance.PopSceneToAnchor(LibBridgeInfo.AnchorName.TITLE, LibBridgeInfo.DefaultSceneChangeFadeParam, SceneManager.Instance.UnloadAllOtherScene);

            closeRequest = true;
        }
        return closeRequest;
    }

    /// <summary>
    /// 有効・無効時イベント.
    /// </summary>
    public override void OnActive(){}
    public override void OnDeactive(){}
}
