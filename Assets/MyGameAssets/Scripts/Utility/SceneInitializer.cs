using UnityEngine;

/// <summary>
/// シーンの初期化処理をする
/// </summary>
public class SceneInitializer : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // シーンの元にホーム画面に移った時のイベントを呼ぶ
        EventManager.Inst.InvokeEvent(SubjectType.OnHome);
        // 表示するUIのパラメータを設定
        CommonSceneUI.CommonSceneUIParam commonSceneUIParam;
        commonSceneUIParam.showCommonUI = true;
        commonSceneUIParam.showNavigationBar = false;
        commonSceneUIParam.showPlayerStatus = false;
        commonSceneUIParam.showSceneBackButton = false;
        commonSceneUIParam.showSceneTitle = false;
        // 上記で設定したパラメータを元にUIを表示させる
        CommonSceneUI.Inst.ChangeCommonSceneUI(commonSceneUIParam, UISceneBG.SceneBgKind.NONE);
    }
}
