using IceMilkTea.Core;
using UnityEngine;

/// <summary>
/// コンテキストを持つことのできるプレイヤー用のステートマシンクラスです
/// </summary>
/// <typeparam name="TContext">このステートマシンが持つコンテキストの型</typeparam>
public class PlayerStateMachine<TContext> : ImtStateMachine<TContext>
{
    PlayerState playerState = default;                          // プレイヤーのステートクラス
    PlayerState currentPlayerState                              // 現在のステートを取得する時に更新も兼ねるための変数
    {
        get
        {
            playerState = base.currentState as PlayerState;     // 基底クラスの現在のステートをプレイヤー用のステートにキャスト
            return playerState;
        }
    }

    /// <summary>
    /// PlayerStateMachine のインスタンスを初期化します
    /// </summary>
    /// <param name="context">このステートマシンが持つコンテキストの型</param>
    public PlayerStateMachine(TContext context) : base(context)
    {
        // 継承元クラスに必要な引数を渡すための処理なしコンストラクタ
    }

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    public void OnCollisionEnter(Collision collision)
    {
        currentPlayerState?.OnCollisionEnter(collision);
    }

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    public void OnCollisionStay(Collision collision)
    {
        currentPlayerState?.OnCollisionStay(collision);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    public void OnCollisionExit(Collision collision)
    {
        currentPlayerState?.OnCollisionExit(collision);
    }

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollider</param>
    public void OnTriggerEnter(Collider other)
    {
        currentPlayerState?.OnTriggerEnter(other);
    }

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollider</param>
    public void OnTriggerStay(Collider other)
    {
        currentPlayerState?.OnTriggerStay(other);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollider</param>
    public void OnTriggerExit(Collider other)
    {
        currentPlayerState?.OnTriggerExit(other);
    }

    /// <summary>
    /// プレイヤー用のステートクラス
    /// </summary>
    public class PlayerState : PlayerStateMachine<TContext>.State
    {
        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollision</param>
        protected internal virtual void OnCollisionEnter(Collision collision) { }

        /// <summary>
        /// ２つのColliderが衝突している最中に呼び出される
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollision</param>
        protected internal virtual void OnCollisionStay(Collision collision) { }

        /// <summary>
        /// ２つのColliderが衝突しなくなったフレームに呼び出される
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollision</param>
        protected internal virtual void OnCollisionExit(Collision collision) { }

        /// <summary>
        /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal virtual void OnTriggerEnter(Collider other) { }

        /// <summary>
        /// ２つのColliderが衝突している最中に呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal virtual void OnTriggerStay(Collider other) { }

        /// <summary>
        /// ２つのColliderが衝突しなくなったフレームに呼び出される（片方はisTriggerがtrueである時）
        /// </summary>
        /// <param name="collision">この衝突に含まれるその他のCollider</param>
        protected internal virtual void OnTriggerExit(Collider other) { }
    }
}

