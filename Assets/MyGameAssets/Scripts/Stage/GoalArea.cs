using UnityEngine;
using UniRx;
using System;

/// <summary>
/// ゴールエリアに関する処理を行う
/// </summary>
public class GoalArea : MonoBehaviour
{
    Subject<Unit> gameClearSubject = new Subject<Unit>();               //ゲームがクリアしたことを知らせるSubject
    public IObservable<Unit> OnGameClear => gameClearSubject;           //ゲームがクリアされたら呼ばれるイベント　※購読側だけを公開

    /// <summary>
    /// ２つのColliderが衝突した時に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            gameClearSubject.OnNext(Unit.Default);
        }
    }
}
