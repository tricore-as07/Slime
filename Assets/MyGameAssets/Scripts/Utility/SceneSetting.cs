using UnityEngine;
using VMUnityLib;

/// <summary>
/// シーンの設定
/// </summary>
public class SceneSetting : MonoBehaviour
{
    [SerializeField] int stageNumber = 1;           //ステージの番号
    public int StageNumber => stageNumber;          //外部に公開するためのプロパティ

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // ホーム画面のイベントを呼ぶ
        EventManager.Inst.InvokeEvent(SubjectType.OnHome);   
        // ゲームクリアしたらクリアしたステージ数をセーブする
        EventManager.Inst.Subscribe(SubjectType.OnGameClear,Unit => SaveDataManager.Inst.SaveClearStageNum(stageNumber),gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnSkipStage,Unit => SaveDataManager.Inst.SaveClearStageNum(stageNumber), gameObject);
        EventManager.Inst.Subscribe(SubjectType.StartFadeOut,Unit => Destroy(gameObject), gameObject);
        // シーンのアンロードイベントを登録する
        SceneManager.Instance.SubscribeSceneUnloadEvent();
    }
}
