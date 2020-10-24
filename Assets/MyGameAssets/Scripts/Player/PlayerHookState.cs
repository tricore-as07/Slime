using UnityEngine;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーがフックを引っ掛けている時のステート
    /// </summary>
    private class PlayerHookState : PlayerStateMachine<Player>.PlayerState
    {
        GameObject hookObj;             //フックを引っ掛けるオブジェクト
        HingeJoint joint;               //フックを繋げるためのクラス

        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // フックを引っ掛けるオブジェクトをキャッシュ
            hookObj = Context.hit.transform.gameObject;
            // フックを引っ掛けるオブジェクトに繋げるためのコンポーネントがあるならキャッシュ
            joint = hookObj?.GetComponent<HingeJoint>();
            // フックの繋げるためのクラスがキャッシュがあるなら
            if (joint != null)
            {
                // 自分とフックを引っ掛けるオブジェクトを接続する
                joint.connectedBody = Context.rigidbody;
                joint.useMotor = true;
            }
            // フックを繋げるためのクラスがキャッシュされてなければ
            else
            {
                // 自由落下の状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
            Context.playerTentacle.ExtendTentacle(Context.gameObject, joint.transform.position - new Vector3(0f,joint.transform.localScale.y / 2,0f));
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            bool isDisconnectHook = !Input.GetKey(KeyCode.Space);        //フックを切断するか
            // フックを切断するなら
            if (isDisconnectHook)
            {
                // フックを切断して、状態を自由落下に変化させる
                joint.connectedBody = null;
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
        }

        /// <summary>
        /// 別の状態に変更される時の処理はこのExitで行う
        /// </summary>
        protected internal override void Exit()
        {
            Context.playerTentacle.ShrinkTentacle();
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
                // フックを切断して、状態を自由落下に変化させる
                joint.connectedBody = null;
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}