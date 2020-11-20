using UnityEngine;
using VMUnityLib;
using UnityEngine.Purchasing;

/// <summary>
/// Nendの下に表示するバナー用のクラス
/// </summary>
public class NendButtonBanner : MonoBehaviour
{
    private void Awake()
    {
        if(CodelessIAPStoreListener.initializationComplete)
        {
            var product = CodelessIAPStoreListener.Instance.GetProduct("slimedelad");
            if(product.hasReceipt)
            {
                NendAdController.Inst.ShowBottomBanner(false);
                this.gameObject.SetActive(false);
            }
        }
        
    }

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        NendAdController.Inst.ShowBottomBanner(true);
    }

    public void OnComplete(Product product)
    {
        NendAdController.Inst.ShowBottomBanner(false);
        gameObject.SetActive(false);
    }
}
