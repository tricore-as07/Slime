using UnityEngine;

/// <summary>
/// ゲームクリアした時のUIのボタン
/// </summary>
public class GameClearUIButton : MonoBehaviour
{
    [SerializeField] int movieAdInterval = default;         //動画広告を表示する間隔
    [SerializeField] int myCompanyAdInterval = default;     //自社広告を表示する間隔

    /// <summary>
    /// ゲームクリアのUIのボタンが押された時
    /// </summary>
    public void OnClickGameClearUIButton()
    {
        // 自社広告を表示するタイミング
        if((CommonGameData.Inst.GameClearCount % movieAdInterval) - myCompanyAdInterval == 0)
        {
            Debug.Log("自社広告を表示予定");
        }
        // 動画広告を表示するタイミング
        else if(CommonGameData.Inst.GameClearCount % movieAdInterval == 0)
        {
            AdMobManager.Inst.ShowMovieAd();
        }
        // 上記以外はインタースティシャル広告を表示
        else
        {
            AdMobManager.Inst.ShowInterstitial();
        }
    }
}
