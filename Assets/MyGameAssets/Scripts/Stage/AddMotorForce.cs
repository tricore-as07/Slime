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
            // 加速させる必要があるかどうか（X軸の速度の絶対値が加速しない時の速度以下になっていないかの判定）
            var needAccelerate = Mathf.Abs(joint.connectedBody.velocity.x) > deadZoneVelocity;
            // 加速させる必要があれば
            if(needAccelerate)
            {
                AccelerateMotor(ref motor, joint.connectedBody.velocity.x);
            }
            // 加速させる必要がなければ
            else
            {
                motor.force = 0f;
                motor.targetVelocity = 0f;
            }
            // 計算したモータークラスの情報に書き換え
            joint.motor = motor;
        }
    }

    /// <summary>
    /// モーターを加速させる
    /// </summary>
    /// <param name="motor">加速させるモータークラス</param>
    /// <param name="jointVelocityX">繋がれているオブジェクトのX軸の速度</param>
    void AccelerateMotor(ref JointMotor motor,float jointVelocityX)
    {
        // 加速させる力を設定されている値に変更
        motor.force = force;
        // オブジェクトのX軸の速度からプラス方向に動かすかマイナス方向に加速力を働かせるかを決める
        var accelerateDirection = jointVelocityX < 0 ? 1f : -1f;
        // 決めた方向に加速させる
        motor.targetVelocity = targetVelocity * accelerateDirection;
    }
}
