using UnityEngine;

/// <summary>
/// 風のギミックの処理を行う
/// </summary>
public class WindGimmick : MonoBehaviour
{
    static float windPower = 0f;                                //風の強さ（加速度）
    static float limitSpeed = 0f;                               //風で加速させる最大の速度
    Rigidbody playerRigidbody;                                  //プレイヤーのRigidbody

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        // ステージの設定を反映させる
        windPower = StageSettingsOwner.Inst.StageSettingsData.WindPower;
        limitSpeed = StageSettingsOwner.Inst.StageSettingsData.LimitSpeedOnWindGimmick;
    }

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
            playerRigidbody.AddForce(transform.up * windPower);
            // プレイヤーの速度が最大速度より大きいなら
            if(playerRigidbody.velocity.y > limitSpeed)
            {
                // プレイヤーの上方向の速度だけ最大速度に抑える
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, limitSpeed, 0f);
            }
        }
    }
}
