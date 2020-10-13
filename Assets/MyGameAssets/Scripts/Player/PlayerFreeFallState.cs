using UnityEngine;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが落下している時のステート
    /// </summary>
    public class PlayerFreeFallState : PlayerStateMachine<Player>.PlayerState
    {
        Vector3 hookDir;                //フックを飛ばす方向

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // フックを伸ばす方向
            hookDir = (Vector3.right + Vector3.up).normalized;
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            var isExtendHookInput = Input.GetKeyDown(KeyCode.Space);              //フックを伸ばす入力がされたか
            if (isExtendHookInput)
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
            if (Physics.Raycast(Context.transform.position, hookDir, out hit, 100))
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
        /// 他のオブジェクトと衝突した時に呼ばれる
        /// </summary>
        /// <param name="collision">衝突に関する情報</param>
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