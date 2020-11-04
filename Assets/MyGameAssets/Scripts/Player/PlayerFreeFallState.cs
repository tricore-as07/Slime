using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが落下している時のステート
    /// </summary>
    private class PlayerFreeFallState : PlayerStateMachine<Player>.PlayerState
    {
        bool isReleaseInput;                //入力を離したかどうか
        bool canHookAction;                 //フックのアクションができる時
        bool calledFoundHookTargetEvent;    //フックのターゲットが見つかったイベントを呼んだかどうか
            
        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            isReleaseInput = false;
            canHookAction = false;
            calledFoundHookTargetEvent = false;
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
#if UNITY_EDITOR
            // 入力が離されていない状態で、何かしらの入力がまだされていたら
            if (!isReleaseInput && Input.anyKey)
            {
                return;
            }
            // 入力が離されていない状態で、何も入力されていなければ
            else if(!isReleaseInput && !Input.anyKey)
            {
                isReleaseInput = true;
            }
#elif UNITY_IOS || UNITY_ANDROID
            // 入力が離されていない状態で、何かしらの入力がまだされていたら
            if (!isReleaseInput && Input.touchCount != 0)
            {
                return;
            }
            // 入力が離されていない状態で、何も入力されていなければ
            else if(!isReleaseInput && Input.touchCount == 0)
            {
                isReleaseInput = true;
            }
#endif
            var isExtendHookInput = Context.IsTapInputMoment();              //フックを伸ばす入力がされたか
            // フックを伸ばす入力がされて、プレイヤーが氷の状態じゃなければ
            if (isExtendHookInput && !Context.IsFrozen)
            {
                canHookAction = true;
            }
            else
            {
                canHookAction = false;
            }
        }

        /// <summary>
        /// フックのアクションができるか確認する
        /// </summary>
        void CheckToHookAction(GameObject gameObject)
        {
            // フックのオブジェクトのタグじゃなけれな早期リターン
            if(gameObject.tag != TagName.HookPoint)
            {
                return;
            }
            // フックのオブジェクトのタグでフックが見つかったイベントを呼んでいなければ
            else if(!calledFoundHookTargetEvent)
            {
                Context.hookObject = gameObject;
                Context.targetHookPosition = gameObject.transform.position - new Vector3(0f, gameObject.transform.localScale.y * 0.5f);
                EventManager.Inst.InvokeEvent(SubjectType.OnFoundHook);
                calledFoundHookTargetEvent = true;
            }
            // フックのエリアに入っていて、フックのアクションが可能なら
            if(canHookAction)
            {
                // フックを引っ掛けてる状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Hook);
            }
        }

        /// <summary>
        /// 接地したかどうかを確認する
        /// </summary>
        /// <param name="gameObject"></param>
        void CheckOnGround(GameObject gameObject)
        {
            // 接地したとき
            if (gameObject.tag == TagName.GroundTrigger)
            {
                // フックのターゲットが見つかったイベントを呼んでいたら
                if(calledFoundHookTargetEvent)
                {
                    // フックが使えなくなるので見つからなくなった時のイベントを呼ぶ
                    EventManager.Inst.InvokeEvent(SubjectType.OnNotFoundHook);
                }
                // 何もしてない状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }

        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal override void OnTriggerEnter(Collider other)
        {
            CheckToHookAction(other.gameObject);
            CheckOnGround(other.gameObject);
        }

        /// <summary>
        /// ２つのColliderが衝突している最中に呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal override void OnTriggerStay(Collider other)
        {
            // 入力を一度離しているかどうか
            if (!isReleaseInput)
            {
                // 入力しっぱなしの場合、早期リターン
                return;
            }
            CheckToHookAction(other.gameObject);
            CheckOnGround(other.gameObject);
        }

        /// <summary>
        /// ２つのColliderが衝突しなくなったフレームに呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal override void OnTriggerExit(Collider other)
        {
            // フックのターゲットが見つかったイベントを呼んでいて、衝突しなくなったオブジェクトがフックポイントなら
            if (calledFoundHookTargetEvent && other.tag == TagName.HookPoint)
            {
                calledFoundHookTargetEvent = false;
                // フックが使えなくなるので見つからなくなった時のイベントを呼ぶ
                EventManager.Inst.InvokeEvent(SubjectType.OnNotFoundHook);
            }
        }
    }
}