using UnityEngine;
using System.Collections.Generic;
using UniRx;
using System;

/// <summary>
/// ゲームオーバーUIの機能
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [SerializeField] List<SubjectType> enableEvents = default;          //UIをアクティブにするイベントのリスト
    [SerializeField] List<SubjectType> disableEvents = default;         //UIを非アクティブにするイベントのリスト
    [SerializeField] float delayTime = default;                         //遅延時間

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        Initialize();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {
        // アクティブにするイベントを登録する
        foreach (var enableEvent in enableEvents)
        {
            EventManager.Inst.Subscribe(enableEvent, Unit =>
            {
                Observable.Timer(TimeSpan.FromSeconds(delayTime)).Subscribe(_ => gameObject.SetActive(true));
            }, gameObject);
        }
        // 非アクティブにするイベントを登録する
        foreach (var disableEvent in disableEvents)
        {
            EventManager.Inst.Subscribe(disableEvent, Unit => gameObject.SetActive(false), gameObject);
        }
    }
}
