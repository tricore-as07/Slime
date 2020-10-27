using UnityEngine;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが何もアクションを起こしていない時のステート
    /// </summary>
    private class PlayerNormalState : PlayerStateMachine<Player>.PlayerState
    {
        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            var isJumpInput = Context.IsTapInput();              //ジャンプする入力がされたか
            if (isJumpInput)
            {
                // ジャンプ力
                float jumpPower;
                jumpPower = Context.jumpPower * Context.GetJumpPowerFactor();
                // ジャンプ力を元に上方向に力を加える
                Context.rigidbody.AddForce(Vector3.up * jumpPower);
                // 自由落下している状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
        }

        /// <summary>
        /// ２つのColliderが衝突しなくなったフレームに呼び出される
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollision</param>
        protected internal　override void OnCollisionExit(Collision collision)
        {
            //　接地状態じゃなくなったとき
            if(collision.gameObject.tag == TagName.Ground)
            {
                // 自由落下している状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
        }
    }
}