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
            // 自分のOnCollisionが呼ばれるように、状態が変化した時には呼ばれないように購読解除用の変数に保存
            disposable = Context.OnCollisionEnterEvent.Subscribe(obj => OnCollisionEnter(obj));
            // プレイヤーにジャンプのための力を加える
            Context.rigidbody.AddForce(Vector3.up * Context.jumpPower);
        }

        /// <summary>
        /// 状態から脱出する時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            // 購読解除
            disposable.Dispose();
        }

        /// <summary>
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="collision">衝突に関する情報</param>
        void OnCollisionEnter(Collision collision)
        {
            // 接地したとき
            if(collision.gameObject.tag == "Ground")
            {
                // 何もしてない状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}

