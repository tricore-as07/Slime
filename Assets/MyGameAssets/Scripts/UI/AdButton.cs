using UnityEngine;
using UnityEngine.Purchasing;

/// <summary>
/// 広告削除ボタンの処理
/// </summary>
public class AdButton : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        this.gameObject.SetActive(AdMobManager.Inst.IsShowAd);
    }
    /// <summary>
    /// 購入処理が完了したときに呼ばれる
    /// </summary>
    public void OnPurchaseComplete(Product product)
    {
        //this.gameObject.SetActive(false);
    }
}
