/******************************************************************************/
/*!    \brief  複数Sceneを跨いで使う共通のData FIXME:GameDataと競合。福留作.
*******************************************************************************/

using VMUnityLib;

public class CommonGameData : SingletonMonoBehaviour<CommonGameData>
{
    bool isReward;


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
