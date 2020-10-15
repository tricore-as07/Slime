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
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            bool isDisconnectHook = Input.GetKeyDown(KeyCode.Space);        //フックを切断するか
            // フックを切断するなら
            if (isDisconnectHook)
            {
                // フックを切断して、状態を自由落下に変化させる
                joint.connectedBody = null;
                stateMachine.SendEvent((int)PlayerStateEventId.FreeFall);
            }
        }

        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal override void OnTriggerEnter(Collider other)
        {
            // 接地したとき
            if (other.tag == TagName.GroundTrigger)
            {
                // 何もしてない状態に変化させる
                stateMachine.SendEvent((int)PlayerStateEventId.Normal);
            }
        }
    }
}