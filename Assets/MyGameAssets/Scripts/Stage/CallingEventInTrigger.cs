using UnityEngine;
using VMUnityLib;

/// <summary>
/// 設定したタグがトリガーに入ってきたらイベントを呼ぶ
/// </summary>
public class CallingEventInTrigger : MonoBehaviour
{
    [SerializeField, TagName] string tagName = default;     //入ってきたことを検知するタグの名前
    [SerializeField] SubjectType subjectType = default;     //設定したタグがトリガーに入ってきた時に呼ぶイベント

    /// <summary>
    /// ２つのColliderが衝突した時に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagName)
        {
            EventManager.Inst.InvokeEvent(subjectType);
        }
    }
}
