using UnityEngine;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーがジャンプしている時のステート
    /// </summary>
    private class PlayerJumpState : PlayerStateMachine<Player>.PlayerState
    {
        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // プレイヤーにジャンプのための力を加える
            Context.rigidbody.AddForce(Vector3.up * Context.jumpPower);
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            //ジャンプの入力がやめられたら自由落下に切り替える
            if(!Input.GetKey(KeyCode.Space))
            {
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
        }

        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal override void OnTriggerEnter(Collider other)
        {
            // 接地したとき
            if (other.tag == TagName.GroundTrigger)
            {
                // 何もしてない状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}

