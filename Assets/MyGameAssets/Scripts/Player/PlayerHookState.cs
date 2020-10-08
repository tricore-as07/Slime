using IceMilkTea.Core;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    public class PlayerHookState : ImtStateMachine<Player>.State
    {
        IDisposable disposable;         //購読を解除するために使用
        GameObject hookObj;
        HingeJoint joint;

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // 自分のOnCollisionが呼ばれるように、状態が変化した時には呼ばれないように購読解除用の変数に保存
            disposable = Context.OnCollisionEnterEvent.Subscribe(obj => OnCollisionEnter(obj));

            hookObj = Context.hit.transform.gameObject;
            joint = hookObj?.GetComponent<HingeJoint>();
            if(joint != null)
            {
                joint.connectedBody = Context.rigidbody;
            }
            else
            {
                stateMachine.SendEvent((int)PlayerStateEventId.Jump);
            }
            joint.useMotor = true;
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            bool isDisconnectHook = Input.GetKeyDown(KeyCode.Space);
            if (isDisconnectHook)
            {
                joint.connectedBody = null;
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
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
            if (collision.gameObject.tag == "Ground")
            {
                // 何もしてない状態に変化させる
                joint.connectedBody = null;
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}