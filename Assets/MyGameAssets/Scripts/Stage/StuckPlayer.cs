using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーをゲーム開始まで足止めする
/// </summary>
public class StuckPlayer : MonoBehaviour
{
    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {
        // ゲームが開始されたら自分を非アクティブにする
        Action<Unit> action = Unit => gameObject.SetActive(false);
        EventManager.Inst.Subscribe(SubjectType.OnGameStart, action);
    }
}
