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
            var isJumpInput = Input.GetKeyDown(KeyCode.Space);              //ジャンプする入力がされたか
            if (isJumpInput)
            {
                Context.rigidbody.AddForce(Vector3.up * Context.jumpPower);
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