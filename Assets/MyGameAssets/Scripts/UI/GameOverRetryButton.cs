using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバー時のリトライボタン
/// </summary>
public class GameOverRetryButton : MonoBehaviour
{
    [SerializeField] int firstShowAd = default;
    [SerializeField] int intervalShowAd = default;

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
