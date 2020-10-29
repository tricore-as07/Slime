using UnityEngine;

/// <summary>
/// ダイヤモンドのオブジェクトに関する処理をする
/// </summary>
public class DiamondObject : MonoBehaviour
{
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