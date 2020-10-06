/******************************************************************************/
/*!    \brief  デバッグルートシーン.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VMUnityLib;

public sealed class DebugSceneRoot : CmnMonoBehaviour 
{
    // 処理なし。メッセージ受信エラー避け.
    protected override void OnSceneDeactive()   { }
    public    override void Start()             { }
    protected override void FixedUpdate()       { }

    bool bVisible = false;

    /// <summary>
    /// シーン切り替え初期化.
    /// </summary>
    protected override void InitSceneChange() { bVisible = false; }

    /// <summary>
    /// フェードイン終了.
    /// </summary>
    protected override void OnFadeInEnd()     { bVisible = true; }
    
    /// <summary>
    /// Raises the GU event.
    /// </summary>
    void OnGUI()
    {
        if (bVisible == false) return;

        GUIHelper.SetAllFontSizeToGameScreen();

        // キャラクタースポーンテスト.
        float x = 0; float y = 0;
        float w = 600; float h = 485;

        float startY = LibBridgeInfo.FIXED_SCREEN_HI / 2 - h / 2;
        Rect rect = GUIHelper.GetScaledRect(LibBridgeInfo.FIXED_SCREEN_WI / 2 - w / 2 + x, startY, w, h);
        GUI.Box(rect, "");

        y = - h/2 - 20;
        h = 30;

        rect = GUIHelper.GetScaledRect(LibBridgeInfo.FIXED_SCREEN_WI / 2 - w / 2 + x, LibBridgeInfo.FIXED_SCREEN_HI / 2 - h / 2 + y, w, h);
        GUI.Box(rect, "デバッグブートメニュー");

        w = w - 20; 
        h = 115;
        float space = 5;

        Dictionary<string, System.Action> selection = new Dictionary<string, System.Action>();
        selection.Add("通常起動", NormalBoot);
        selection.Add("タイトル", TitleDebug);

        int i = 0;
        foreach(KeyValuePair<string, System.Action> pair in selection)
        {
            y = startY + (h + space) * i + space;
            rect = GUIHelper.GetScaledRect(LibBridgeInfo.FIXED_SCREEN_WI / 2 - w / 2 + x, y, w, h);
            if (GUI.Button(rect, pair.Key))
            {
                bVisible = false;
                if (pair.Value != null)
                {
                    pair.Value();
                }
            }
            ++i;
        }
    }

    void NormalBoot()
    {
        SceneManager.Instance.PushScene(SceneName.demo, LibBridgeInfo.DefaultSceneChangeFadeParam, null, LibBridgeInfo.AnchorName.TITLE);
    }
    void TitleDebug()
    {
        SceneManager.Instance.PushScene(SceneName.title, LibBridgeInfo.DefaultSceneChangeFadeParam);
    }
}
