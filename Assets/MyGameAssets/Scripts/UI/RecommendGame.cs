using UnityEngine;

/// <summary>
/// おすすめゲームに関するクラス
/// </summary>
public class RecommendGame : MonoBehaviour
{
    [SerializeField] string url = default;  // おすすめゲームをクリックされた時に開くURL

    /// <summary>
    /// クリックされた時
    /// </summary>
    public void OnClick()
    {
        Application.OpenURL(url);
    }
}
