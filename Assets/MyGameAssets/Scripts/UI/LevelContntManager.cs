using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// レベル選択のコンテンツを管理する
/// </summary>
public class LevelContntManager : SingletonMonoBehaviour<LevelContntManager>
{
    [SerializeField] List<StageElementUpdater> levels = default;        //レベル選択に使用する要素のリスト

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        SwitchLevels();
        // アクティブを切り替える可能性があるイベントに更新関数を登録
        EventManager.Inst.Subscribe(SubjectType.OnHome,Unit => SwitchLevels(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnRetry,Unit => SwitchLevels(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnNextStage,Unit => SwitchLevels(), gameObject);
    }

    /// <summary>
    /// レベルのアクティブ、非アクティブを切り替える
    /// </summary>
    void SwitchLevels()
    {
        // クリアしたステージ数を取得
        int clearStageNum = SaveDataManager.Inst.GetClearStageNum();
        for (int i = 0; i < levels.Count; i++)
        {
            // クリアしたステージ数以下ならアクティブ、そうでない場合は非アクティブにする
            levels[i].gameObject.SetActive(i <= clearStageNum);
        }
    }

    /// <summary>
    /// コンテンツを更新する
    /// </summary>
    /// <param name="updateStageNum">更新するコンテンツのステージ数</param>
    public void UpdateContent(int updateStageNum)
    {
        //更新するステージ数がUIで用意されているステージの数以内なら
        if(updateStageNum < levels.Count)
        {
            levels[updateStageNum - 1].UpdateStageElement();
        }
        else
        {
            Debug.LogError("設定されているステージ数が用意されているUIの数をオーバーしています。\n現在のシーンに設定されているステージ数を見直すか、UIのステージ選択用のプレハブを増やしてください。");
        }
    }
}
