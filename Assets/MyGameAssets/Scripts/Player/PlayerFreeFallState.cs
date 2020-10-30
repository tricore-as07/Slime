using UnityEngine;

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
        Vector3 hookDir;                //フックを飛ばす方向
        bool isReleaseInput;            //入力を離したかどうか

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // フックを伸ばす方向
            hookDir = (Vector3.right + Vector3.up).normalized;
            isReleaseInput = false;
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
                ExtendHook();
            }
        }

        /// <summary>
        /// フックを飛ばす処理
        /// </summary>
        void ExtendHook()
        {
            //Rayを飛ばして衝突したオブジェクトの情報を保存する
            RaycastHit hit;
            //Rayを飛ばして衝突したものがあれば
            if (Physics.Raycast(Context.transform.position, hookDir, out hit, Context.playerSettingsData.TentacleMaxLength, LayerName.HookPointMask)) 
            {
                //Rayに衝突したオブジェクトがHookを引っ掛けられるところなら
                if (hit.transform.tag == TagName.HookPoint)
                {
                    Context.hit = hit;
                    // フックを引っ掛けてる状態に変化させる
                    stateMachine.SendEvent((int)PlayerStateEventId.Hook);
                }
            }
        }

        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollision</param>
        protected internal override void OnCollisionEnter(Collision collision)
        {
            // 接地したとき
            if (collision.gameObject.tag == TagName.Ground)
            {
                // 何もしてない状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}