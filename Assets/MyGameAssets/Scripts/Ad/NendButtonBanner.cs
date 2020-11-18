using UnityEngine;
using VMUnityLib;

/// <summary>
/// Nendの下に表示するバナー用のクラス
/// </summary>
public class NendButtonBanner : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        NendAdController.Inst.ShowBottomBanner(true);
    }
}
