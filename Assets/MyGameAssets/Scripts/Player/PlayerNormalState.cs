using UnityEngine;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが何もアクションを起こしていない時のステート
    /// </summary>
    public class PlayerNormalState : PlayerStateMachine<Player>.PlayerState
    {
        bool isOnGround = false;                                            //地面に接地しているかどうか

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            Context.MoveHorizontalPlayer();
            var isJumpInput = Input.GetKeyDown(KeyCode.Space);              //ジャンプする入力がされたか
            // ジャンプする入力がされていて、接地していたら
            if (isJumpInput && isOnGround)
            {
                // ジャンプしている状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Jump);
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
                isOnGround = false;
            }
        }

        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollision</param>
        protected internal override void OnCollisionEnter(Collision collision)
        {
            // 接地したとき
            if (collision.gameObject.tag == TagName.Ground)
            {
                isOnGround = true;
            }
        }
    }
}