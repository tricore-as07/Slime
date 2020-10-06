/******************************************************************************/
/*!    \brief  自社製品をオススメするウインドウのボタン.
*******************************************************************************/
// プラットフォームによって変わるが値だけは保持しておきたいため警告無視
#pragma warning disable 0414

using UnityEngine;

public sealed class SelfRecommendContent : MonoBehaviour 
{
    [SerializeField]
    string androidURL = default;

    [SerializeField]
    string iosURL = default;

    public void OpenUrl()
    {
#if UNITY_IOS
        Application.OpenURL(iosURL);
#else
        Application.OpenURL(androidURL);
#endif
    }
}
