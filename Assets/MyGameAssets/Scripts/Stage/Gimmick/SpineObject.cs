using UnityEngine;

/// <summary>
/// 棘のオブジェクトの処理をする
/// </summary>
/// FIXME: orimoto 氷のギミック実装時にゲームオーバーの条件追加予定
public class SpineObject : MonoBehaviour
{
    static Player player;           //プレイヤーのクラス

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトがプレイヤーなら
        if (collision.gameObject.tag == TagName.Player)
        {
            // プレイヤーのクラスを持っていなければキャッシュする 
            player = player ?? collision.gameObject.GetComponent<Player>();
            // ゲームオーバー処理を実行する
            EventManager.Inst.InvokeEvent(SubjectType.OnGameOver);
        }
    }
}
