using IceMilkTea.Core;
using UnityEngine;
using UniRx;
using System;
using System.Collections.Generic;

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
        List<IDisposable> disposableList = new List<IDisposable>();         //購読を解除するために使用
        bool isOnGround = false;                                            //地面に接地しているかどうか

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // 自分のOnCollisionが呼ばれるように、状態が変化した時には呼ばれないように購読解除用のリストに追加
            disposableList.Add(Context.OnCollisionExitEvent.Subscribe(obj => OnCollisionExit(obj)));
            disposableList.Add(Context.OnCollisionEnterEvent.Subscribe(obj => OnCollisionEnter(obj)));
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            var isJumpInput = Input.GetKeyDown(KeyCode.Space);              //ジャンプする入力がされたか
            // ジャンプする入力がされていて、接地していたら
            if (isJumpInput && isOnGround)
            {
                // ジャンプしている状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Jump);
            }
        }

        /// <summary>
        /// 状態から脱出する時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            // 購読解除
            foreach (var disposable in disposableList)
            {
                disposable.Dispose();
            }
            disposableList.Clear();
        }

        /// <summary>
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="collision">衝突に関する情報</param>
        void OnCollisionExit(Collision collision)
        {
            //　接地状態じゃなくなったとき
            if(collision.gameObject.tag == "Ground")
            {
                isOnGround = false;
            }
        }

        /// <summary>
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="collision">衝突に関する情報</param>
        void OnCollisionEnter(Collision collision)
        {
            // 接地したとき
            if (collision.gameObject.tag == "Ground")
            {
                isOnGround = true;
            }
        }
    }
}