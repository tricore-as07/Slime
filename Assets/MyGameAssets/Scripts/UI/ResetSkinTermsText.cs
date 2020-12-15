using UnityEngine;
using TMPro;

/// <summary>
/// スキン開放条件のテキストをリセットする
/// </summary>
public class ResetSkinTermsText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro = default;
    /// <summary>
    /// オブジェクトがアクエィブになった時
    /// </summary>
    private void OnEnable()
    {
        textMeshPro.text = "";
    }
}
