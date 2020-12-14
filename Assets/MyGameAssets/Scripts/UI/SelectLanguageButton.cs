using UnityEngine;
using I2.Loc;

/// <summary>
/// 言語設定用のボタン
/// </summary>
public class SelectLanguageButton : MonoBehaviour
{
    [SerializeField] string languageName = default;     //ボタンが押された時に設定する言語の名前

    /// <summary>
    /// ボタンがクリックされた時
    /// </summary>
    public void OnClickButton()
    {
        LocalizationManager.CurrentLanguage = languageName;
    }
}
