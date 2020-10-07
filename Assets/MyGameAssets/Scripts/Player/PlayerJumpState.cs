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
    /// プレイヤーがジャンプしている時ステート
    /// </summary>
    public class PlayerJumpState : ImtStateMachine<Player>.State
    {
        IDisposable disposable;         //購読を解除するために使用

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            //タップを促すためのテキストをアクティブにする
            disposable = Context.OnCollisionEnterEvent.Subscribe(obj => OnCollisionEnter(obj));
            Context.rigidbody.AddForce(Vector3.up * Context.jumpPower);
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
        void OnCollisionEnter(GameObject gameObject)
        {
            if(gameObject.tag == "Ground")
            {
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}

