using UnityEngine;

/// <summary>
/// リトライのボタンに関する処理をする
/// </summary>
public class RetryButton : MonoBehaviour
{
    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        // ゲームオーバーになったら自分のアクティブをtrueにする
        EventManager.Inst.Subscribe(SubjectType.OnGameOver,Unit => gameObject.SetActive(true));
        // 自分のアクティブをfalseにする
        gameObject.SetActive(false);
    }

    /// <summary>
    /// リトライボタンが押された時
    /// </summary>
    public void OnPressRetryButton()
    {
        EventManager.Inst.InvokeEvent(SubjectType.OnRetry);
        gameObject.SetActive(false);
    }
}
