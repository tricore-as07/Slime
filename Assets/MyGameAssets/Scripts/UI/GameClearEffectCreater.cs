using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ゲームクリア時のエフェクトを作成する
/// </summary>
public class GameClearEffectCreater : MonoBehaviour
{
    [SerializeField] GameObject createEffect = default;     //作成するエフェクト
    [SerializeField] int startCreateNum = default;          //開始時に作成するエフェクトの数
    [SerializeField] float createIntervalTime = default;    //作成する間隔
    [SerializeField] float randomRange = default;           //作成する間隔
    [SerializeField] GameObject createArea = default;       //作成するエリア
    bool isCreateEffect;                                    //エフェクトを作成するかどうか
    List<IDisposable> disposables = new List<IDisposable>();//イベント解除用のリスト

    /// <summary>
    /// オブジェクトがアクティブになった時
    /// </summary>
    void OnEnable()
    {
        isCreateEffect = false;
        // ゲームクリア時にエフェクトの作成を始めるようにイベントを登録
        disposables.Add(EventManager.Inst.Subscribe(SubjectType.OnGameClear,Unit => StartCreatingGameClearEffect()));
        // エフェクトの作成をやめる際のイベントを登録
        disposables.Add(EventManager.Inst.Subscribe(SubjectType.OnNextStage,Unit => isCreateEffect = false));
        disposables.Add(EventManager.Inst.Subscribe(SubjectType.OnHome,Unit => isCreateEffect = false));
        disposables.Add(EventManager.Inst.Subscribe(SubjectType.OnRetry,Unit => isCreateEffect = false));
    }

    /// <summary>
    /// オブジェクトが非アクティブになった時
    /// </summary>
    void OnDisable()
    {
        foreach (var disposable in disposables)
        {
            disposable.Dispose();
        }
        disposables.Clear();
    }

    /// <summary>
    /// ゲームクリアエフェクトの作成を開始する
    /// </summary>
    void StartCreatingGameClearEffect()
    {
        isCreateEffect = true;
        // 最初に作成する分のエフェクトを一気に作成
        for (int i = 0; i < startCreateNum; i++)
        {
            CreateGameClearEffect();
        }
        // エフェクトを作成し続けるコルーチンを開始
        StartCoroutine(CreatingGameClearEffect());
    }

    /// <summary>
    /// ゲームクリアエフェクトを作成し続ける
    /// </summary>
    IEnumerator CreatingGameClearEffect()
    {
        // エフェクトを作成するフラグが立っているままなら
        while (isCreateEffect)
        {
            // エフェクトを作成する
            CreateGameClearEffect();
            // 作成の間隔分待つ
            yield return new WaitForSeconds(createIntervalTime + UnityEngine.Random.Range(-randomRange,randomRange));
        }
        yield break;
    }

    /// <summary>
    /// ゲームクリアエフェクトを作成
    /// </summary>
    void CreateGameClearEffect()
    {
        // ポジションをエリア内でランダムで選出
        var randomPosition = new Vector3
            (UnityEngine.Random.Range(createArea.transform.localScale.x * -0.5f, createArea.transform.localScale.x * 0.5f),
            UnityEngine.Random.Range(createArea.transform.localScale.y * -0.5f, createArea.transform.localScale.y * 0.5f),
            UnityEngine.Random.Range(createArea.transform.localScale.z * -0.5f, createArea.transform.localScale.z * 0.5f));
        // 作成するポジション
        var createPosition = createArea.transform.position + randomPosition;
        // エフェクトを作成
        var effect = Instantiate(createEffect,createPosition,Quaternion.identity);
        effect.transform.SetParent(transform, true);
    }
}
