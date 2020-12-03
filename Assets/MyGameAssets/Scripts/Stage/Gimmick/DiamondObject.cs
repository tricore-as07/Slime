using UnityEngine;

/// <summary>
/// ダイヤモンドのオブジェクトに関する処理をする
/// </summary>
public class DiamondObject : MonoBehaviour
{
    bool isAcquisition = false;     //獲得したかどうか

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        EventManager.Inst.Subscribe(SubjectType.OnGameStart,Unit => isAcquisition = gameObject.activeSelf);
        EventManager.Inst.Subscribe(SubjectType.OnGameOver, Unit => gameObject.SetActive(isAcquisition));
    }

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagName.Player)
        {
            gameObject.SetActive(false);
        }
    }
}