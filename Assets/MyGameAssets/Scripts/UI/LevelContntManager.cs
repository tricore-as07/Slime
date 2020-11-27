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
        EventManager.Inst.Subscribe(SubjectType.OnHome,Unit => SwitchLevels());
        EventManager.Inst.Subscribe(SubjectType.OnRetry,Unit => SwitchLevels());
        EventManager.Inst.Subscribe(SubjectType.OnNextStage,Unit => SwitchLevels());
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
            // クリアしたステージ数以下ならアクティブにする
            if(i <= clearStageNum)
            {
                levels[i].gameObject.SetActive(true);
            }
            // それ以外は非アクティブにする
            else
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// コンテンツを更新する
    /// </summary>
    /// <param name="updateStageNum">更新するコンテンツのステージ数</param>
    public void UpdateContent(int updateStageNum)
    {
        levels[updateStageNum - 1].StageElementUpdate();
    }
}
