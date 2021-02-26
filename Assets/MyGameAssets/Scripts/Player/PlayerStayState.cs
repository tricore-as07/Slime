using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが静止している時のステート
    /// </summary>
    public class PlayerStayState : PlayerStateMachine<Player>.PlayerState
    {
        Vector3 stayPosition;           //このステートに入ってきた時のポジション

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            stayPosition = Context.transform.position;
            Context.rigidbody.useGravity = false;
            // ゲームが開始されたらプレイヤーの状態をNormalに変更する
            Action<Unit> action = Unit => stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            EventManager.Inst.Subscribe(SubjectType.OnGameStart, action);
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            // 同じ場所に留めておくための処理
            Context.transform.position = stayPosition;
            Context.rigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// 別の状態に変更される時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            Context.rigidbody.useGravity = true;
            Context.trailRenderer.enabled = true;
        }
    }
}
