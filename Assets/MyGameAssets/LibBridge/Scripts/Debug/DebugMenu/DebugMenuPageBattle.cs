/******************************************************************************/
/*!    \brief  デバッグメニューページ：バトル.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using VMUnityLib;

public class DebugMenuPageBattle : DebugMenuPage 
{
    public override string GetPageName() { return "バトル"; } // ページ名.
    
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
        float w = 250;
        if(GUI.Button(GUIHelper.GetScaledRect(x,y,w,h) , "テスト"))
        {
            Debug.Log("もどりたい！");
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
