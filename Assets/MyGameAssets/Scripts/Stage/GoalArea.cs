using UnityEngine;
using UniRx;

/// <summary>
/// ゴールエリアに関する処理を行う
/// </summary>
public class GoalArea : MonoBehaviour
{
    Subject<Unit> gameClearSubject = default;               //ゲームがクリアしたことを知らせるSubject

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        // イベントマネージャーに登録
        gameClearSubject = EventManager.Inst.CreateSubject(SubjectType.OnGameClear);
    }

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
