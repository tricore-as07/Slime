using UnityEngine;

/// <summary>
/// 氷のギミックの処理をする
/// </summary>
public class IceGimmick : MonoBehaviour
{
    static Player player;           //プレイヤーのクラス

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    private void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトがプレイヤーなら
        if (other.tag == TagName.Player)
        {
            // プレイヤーのクラスを持っていなければキャッシュする 
            player = player ?? other.GetComponent<Player>();
            player.OnIceGimmickEnter();
        }
    }
}
