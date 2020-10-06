/******************************************************************************/
/*!    \brief  共通のシーンUI.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using VMUnityLib;

public sealed class CommonSceneUI : SingletonMonoBehaviour<CommonSceneUI> 
{
    // 共通シーンUIを表示するパラメータ.
    [System.Serializable]
    public struct CommonSceneUIParam
    {
        public bool                 showPlayerStatus;
        public bool                 showNavigationBar;
        public bool                 showSceneTitle;
        public bool                 showSceneBackButton;
    }

    [SerializeField]
    UIPlayerStatusBar playerStatusBar = default;

    [SerializeField]
    UISceneBG sceneBG = default;

    [SerializeField]
    UINavigationBar navigationBar = default;

    [SerializeField]
    UISceneTitle sceneTitle = default;

    [SerializeField]
    UISceneBackButton sceneBackButton = default;

    /// <summary>
    /// 共通シーンUIの切り替え.
    /// </summary>
    public void ChangeCommonSceneUI(CommonSceneUIParam param, UISceneBG.SceneBgKind sceneBgKind)
    {
        sceneBG.ChangeBG(sceneBgKind);

        // FIXME:仮で即時切替しているが、実際はTweenを使用して滑らかにワイプさせる必要あり.
        playerStatusBar.gameObject.SetActive(param.showPlayerStatus);
        navigationBar.gameObject.SetActive(param.showNavigationBar);
        sceneTitle.gameObject.SetActive(param.showSceneTitle);
        sceneBackButton.gameObject.SetActive(param.showSceneBackButton);
    }

    /// <summary>
    /// シーンタイトルラベルの切り替え.
    /// </summary>
    /// <param name="localizeId"></param>
    public void ChangeSceneTitleLabel(string localizeId)
    {
        sceneTitle.ChangeSceneTitleLabel(localizeId);
    }
}
