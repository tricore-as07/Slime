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
        bool canJump = false;                                               //ジャンプできるかどうか

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            disposableList.Add(Context.OnCollisionExitEvent.Subscribe(obj => OnCollisionExit(obj)));
            disposableList.Add(Context.OnCollisionEnterEvent.Subscribe(obj => OnCollisionEnter(obj)));
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                stateMachine.SendEvent((int)PlayerStateEventId.Jump);
            }
        }

        /// <summary>
        /// 状態から脱出する時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            foreach(var disposable in disposableList)
            {
                disposable.Dispose();
            }
            disposableList.Clear();
        }

        /// <summary>
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="gameObject">衝突したゲームオブジェクト</param>
        void OnCollisionExit(GameObject gameObject)
        {
            if(gameObject.tag == "Ground")
            {
                canJump = false;
            }
        }

        /// <summary>
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="gameObject">衝突したゲームオブジェクト</param>
        void OnCollisionEnter(GameObject gameObject)
        {
            if (gameObject.tag == "Ground")
            {
                canJump = true;
            }
        }
    }
}