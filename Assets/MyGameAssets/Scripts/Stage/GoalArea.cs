using UnityEngine;

/// <summary>
/// ゴールエリアに関する処理を行う
/// </summary>
public class GoalArea : MonoBehaviour
{
    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        // イベントマネージャーでSubjectを作成する
        EventManager.Inst.CreateSubject(SubjectType.OnGameClear);
    }

    /// <summary>
    /// ２つのColliderが衝突した時に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == TagName.Player)
        {
            EventManager.Inst.InvokeEvent(SubjectType.OnGameClear);
        }
    }
}
