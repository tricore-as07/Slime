using UnityEngine;

/// <summary>
/// ゲームオーバー時のリトライボタン
/// </summary>
public class GameOverRetryButton : MonoBehaviour
{
    [SerializeField] int firstShowAd = default;         //最初に広告を表示する回数
    [SerializeField] int intervalShowAd = default;      //広告を表示する間隔

    /// <summary>
    /// リトライボタンが押された時の処理
    /// </summary>
    public void OnClickRetryButton()
    {
        // 最初に広告を表示するタイミング
        if((CommonGameData.Inst.GameOverCount - firstShowAd) == 0)
        {
            AdMobManager.Inst.ShowMovieAd();
        }
        // 最初を除いた広告を表示するタイミング
        else if((CommonGameData.Inst.GameOverCount - firstShowAd) % intervalShowAd == 0)
        {
            AdMobManager.Inst.ShowMovieAd();
        }
    }
}
