using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ゲームのUI全般の共通クラス
/// </summary>
public class GameUI : MonoBehaviour
{
    [SerializeField] List<SubjectType> enableEvents = default;
    [SerializeField] List<SubjectType> disableEvents = default;

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
        foreach(var enableEvent in enableEvents)
        {
            EventManager.Inst.Subscribe(enableEvent, Unit => gameObject.SetActive(true));
        }
        foreach (var disableEvent in disableEvents)
        {
            EventManager.Inst.Subscribe(disableEvent, Unit => gameObject.SetActive(false));
        }
    }
}
