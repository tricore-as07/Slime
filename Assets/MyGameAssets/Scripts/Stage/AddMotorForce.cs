using UnityEngine;

/// <summary>
/// モーターの力を加える処理をする
/// </summary>
public class AddMotorForce : MonoBehaviour
{
    [SerializeField] HingeJoint joint = default;                //フックを繋げるためのクラス
    [SerializeField] float force = 0f;                          //加速する力
    [SerializeField] float targetVelocity = 0f;                 //加速する目標
    [SerializeField] float deadZoneVelocity = 0f;               //加速しない時の速度（速度が一定以下になると逆方向に加速させるため）

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        // 繋がっているオブジェクトがある時
        if(joint.connectedBody != null)
        {
            // モータークラスを取得
            var motor = joint.motor;
            // xがプラス方向の速度が加速しない時の速度より大きい時
            if (joint.connectedBody.velocity.x > deadZoneVelocity)
            {
                // xのプラス方向に加速させる
                motor.force = force;
                motor.targetVelocity = -targetVelocity;
            }
            // xがマイナス方向の速度が加速しない時の速度より大きい時
            else if (-joint.connectedBody.velocity.x > deadZoneVelocity)
            {
                // xのマイナス方向に加速させる
                motor.force = force;
                motor.targetVelocity = targetVelocity;
            }
            // 速度が小さすぎる場合は加速しない
            else
            {
                motor.force = 0f;
                motor.targetVelocity = 0f;
            }
            // 計算したモータークラスの情報に書き換え
            joint.motor = motor;
        }
    }
}
