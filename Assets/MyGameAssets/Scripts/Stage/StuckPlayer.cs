using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーをゲーム開始まで足止めする
/// </summary>
public class StuckPlayer : MonoBehaviour
{
    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {
        // ゲームが開始されたら自分を非アクティブにする
        EventManager.Inst.GetObservable(SubjectType.OnGameStart)?.Subscribe(Unit => gameObject.SetActive(false));
    }
}
