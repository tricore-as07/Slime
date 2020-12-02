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
        gameObject.SetActive(AdMobManager.Inst.IsShowAd);
    }
    /// <summary>
    /// 購入処理が完了したときに呼ばれる
    /// </summary>
    public void OnPurchaseComplete(Product product)
    {
        // 購入されたものと広告削除用の名前が一致しているなら
        if(product.transactionID == AdMobManager.Inst.AdRemovingName)
        {
            // 広告削除ボタンを非表示にする
            gameObject.SetActive(false);
        }
    }
}
