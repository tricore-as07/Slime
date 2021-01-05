using UnityEngine;

/// <summary>
/// レベルスキップボタン
/// </summary>
public class LevelSkipButton : MonoBehaviour
{
    int levelGameOverCount = 0;                             //同じレベルでゲームオーバーになった回数
    SceneSetting sceneSetting;                              //シーンの設定
    [SerializeField] int showSkipButtonNum = default;       //スキップボタンを表示するまでの回数

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        EventManager.Inst.Subscribe(SubjectType.OnGameOver,Unit => OnGameOver(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnGameClear,Unit => OnGameClear(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnNextStage,Unit => ResetGameOverCount(), gameObject);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// レベルスキップボタンが押された時
    /// </summary>
    public void OnLevelSkipButton()
    {
        AdMobManager.Inst.ShowMovieAd();
        EventManager.Inst.InvokeEvent(SubjectType.OnGameClear);
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
        // シーンの設定のキャッシュ
        sceneSetting = sceneSetting ?? GameObject.FindGameObjectWithTag(TagName.SceneSettings).GetComponent<SceneSetting>();
        // 今のステージ数
        var nowStageNum = sceneSetting.StageNumber;
        // 今のステージがまだクリアできていないステージだったら
        if (nowStageNum == SaveDataManager.Inst.GetClearStageNum() + 1)
        {
            levelGameOverCount++;
        }
        // 今のステージがすでにクリアしているステージだったら
        else
        {
            gameObject.SetActive(false);
        }
        // ゲームオーバーになった回数がスキップボタンを表示するまでの回数以上だったら
        if (levelGameOverCount >= showSkipButtonNum)
        {
            gameObject.SetActive(true);
        }
        // そうじゃない時
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
        // まだクリアしていないステージを初めてクリアしたらリセット
        if(nowStageNum == SaveDataManager.Inst.GetClearStageNum())
        {
            levelGameOverCount = 0;
        }
    }
}
