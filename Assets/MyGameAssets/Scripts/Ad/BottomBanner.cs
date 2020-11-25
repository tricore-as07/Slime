using UnityEngine;
using VMUnityLib;

/// <summary>
/// 下に表示するバナー広告用のクラス
/// </summary>
public class BottomBanner : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        NendAdController.Inst.ShowBottomBanner(AdMobManager.Inst.IsShowAd);
    }
}
