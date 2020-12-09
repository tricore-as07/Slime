using VMUnityLib;
using UniRx;
/// <summary>
/// ゲーム共通のデータをもつ
/// </summary>
public class CommonGameData : SingletonMonoBehaviour<CommonGameData>
{
    bool isReward;
    int gameOverCount = 0;                          //ゲームオーバーの回数のカウンター
    public int GameOverCount => gameOverCount;      //外部に公開するためのプロパティ
    int gameClearCount = 0;                         //ゲームクリアの回数のカウンター
    public int GameClearCount => gameClearCount;    //外部に公開するためのプロパティ

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        EventManager.Inst.Subscribe(SubjectType.OnGameClear, Unit => gameClearCount++);
        EventManager.Inst.Subscribe(SubjectType.OnGameOver, Unit => gameOverCount++);
    }

    /// <summary>
    /// 動画広告視聴完了時処理.
    /// </summary>
    public void OnCompleteReward()
    {
        isReward = true;
    }
    /// <summary>
    /// 広告結果受け渡し時処理.
    /// </summary>
    public bool OnSendReward()
    {
        bool flag = isReward;
        isReward  = false;
        return flag;
    }
}
