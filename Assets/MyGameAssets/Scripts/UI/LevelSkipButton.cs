using UnityEngine;

/// <summary>
/// レベルスキップボタン
/// </summary>
public class LevelSkipButton : MonoBehaviour
{
    int levelGameOverCount = 0;
    SceneSetting sceneSetting;
    [SerializeField] int showSkipButtonNum = default;

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        EventManager.Inst.Subscribe(SubjectType.OnGameOver,Unit => OnGameOver());
        EventManager.Inst.Subscribe(SubjectType.OnGameClear,Unit => OnGameClear());
        EventManager.Inst.Subscribe(SubjectType.OnNextStage,Unit => ResetGameOverCount());
        gameObject.SetActive(false);
    }

    /// <summary>
    /// レベルスキップボタンが押された時
    /// </summary>
    public void OnLevelSkipButton()
    {
        AdMobManager.Inst.ShowMovieAd();
    }

    /// <summary>
    /// ゲームオーバーのカウントをリセットする時の処理
    /// </summary>
    void ResetGameOverCount()
    {
        sceneSetting = null;
        levelGameOverCount = 0;
    }

    /// <summary>
    /// ゲームオーバーになった時の処理
    /// </summary>
    void OnGameOver()
    {
        sceneSetting = sceneSetting ?? GameObject.FindGameObjectWithTag(TagName.SceneSettings).GetComponent<SceneSetting>();
        var nowStageNum = sceneSetting.StageNumber;
        if (nowStageNum == SaveDataManager.Inst.GetClearStageNum())
        {
            levelGameOverCount++;
        }
        else
        {
            gameObject.SetActive(false);
        }
        if(levelGameOverCount >= showSkipButtonNum)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ゲームクリアになった時の処理
    /// </summary>
    void OnGameClear()
    {
        sceneSetting = sceneSetting ?? GameObject.FindGameObjectWithTag(TagName.SceneSettings).GetComponent<SceneSetting>();
        var nowStageNum = sceneSetting.StageNumber;
        if(nowStageNum == SaveDataManager.Inst.GetClearStageNum())
        {
            levelGameOverCount = 0;
        }
    }
}
