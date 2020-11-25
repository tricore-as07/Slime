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
        //IAPの初期化が完了していたら
        if (CodelessIAPStoreListener.initializationComplete)
        {
            // slimedeladが購入されているかを調べる
            var product = CodelessIAPStoreListener.Instance.GetProduct("slimedelad");
            // slimedeladが購入されたレシートが存在しているなら
            if (product.hasReceipt)
            {
                // 広告削除ボタンを非表示にする
                this.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 購入処理が完了したときに呼ばれる
    /// </summary>
    public void OnPurchaseComplete(Product product)
    {
        //this.gameObject.SetActive(false);
    }
}
