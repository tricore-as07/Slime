using IceMilkTea.Core;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが何もアクションを起こしていない時のステート
    /// </summary>
    public class PlayerNormalState : ImtStateMachine<Player>.State
    {
        IDisposable disposable;

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            disposable = Context.OnCollisionExitEvent.Subscribe(obj => OnCollisionExit(obj));
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SendEvent((int)PlayerStateEventId.Jump);
            }
        }

        /// <summary>
        /// 状態から脱出する時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            disposable.Dispose();
        }

        /// <summary>
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="gameObject">衝突したゲームオブジェクト</param>
        void OnCollisionExit(GameObject gameObject)
        {
        }
    }
}