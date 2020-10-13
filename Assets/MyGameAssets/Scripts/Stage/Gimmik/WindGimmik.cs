using UnityEngine;

/// <summary>
/// 風のギミックの処理を行う
/// </summary>
public class WindGimmik : MonoBehaviour
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
        if (other.gameObject.tag == TagName.Player)
        {
            playerRigidbody = playerRigidbody ?? other.gameObject.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.up * WindPower);
            if(playerRigidbody.velocity.y > limitSpeed)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, limitSpeed, 0f);
            }
        }
    }
}
