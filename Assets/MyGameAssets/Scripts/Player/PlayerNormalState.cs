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
        float stretch;
        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            stretch = Context.sensor.m_params.fx.stretch;
            Context.sensor.m_params.fx.stretch = 0f;
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            var isJumpInput = Context.IsTapInputMoment();              //ジャンプする入力がされたか
            if (isJumpInput)
            {
                // ジャンプ力
                float jumpPower;
                jumpPower = Context.jumpPower * Context.GetJumpPowerFactor();
                // ジャンプ力を元に上方向に力を加える
                Context.rigidbody.AddForce(Vector3.up * jumpPower);
                // 自由落下している状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
                // ジャンプのイベントを発行
                EventManager.Inst.InvokeEvent(SubjectType.OnJump);
            }
        }

        /// <summary>
        /// 別の状態に変更される時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            Context.sensor.m_params.fx.stretch = stretch;
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