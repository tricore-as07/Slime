using UnityEngine;

/// <summary>
/// 風のギミックの処理を行う
/// </summary>
public class WindGimmick : MonoBehaviour
{
    [SerializeField] float WindPower = 0f;      //風の強さ（加速度）
    [SerializeField] float limitSpeed = 0f;     //風で加速させる最大の速度
    Rigidbody playerRigidbody;                  //プレイヤーのRigidbody

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    void OnTriggerStay(Collider other)
    {
        // 衝突したオブジェクトがプレイヤーなら
        if (other.gameObject.tag == TagName.Player)
        {
            // Rigidbodyをキャッシュ
            playerRigidbody = playerRigidbody ?? other.gameObject.GetComponent<Rigidbody>();
            // 上方向に風の強さの力を加える
            playerRigidbody.AddForce(Vector3.up * WindPower);
            // プレイヤーの速度が最大速度より大きいなら
            if(playerRigidbody.velocity.y > limitSpeed)
            {
                // プレイヤーの上方向の速度だけ最大速度に抑える
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, limitSpeed, 0f);
            }
        }
    }
}
