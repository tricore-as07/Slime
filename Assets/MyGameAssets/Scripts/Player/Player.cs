using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    // ステートマシーンの状態遷移に使用する列挙型
    enum PlayerStateEventId
    {
        Normal,         //何もしてない状態
        Jump,           //ジャンプしている状態
        Hook,
        FreeFall
    }
    PlayerStateMachine<Player> stateMachine = default;                          //ステートマシン
    RaycastHit hit;                                                             //プレイヤーステート間でRayに衝突したオブジェクトの情報を共有するための変数

    // インスペクターに表示する変数
    [SerializeField] new Rigidbody rigidbody = default;                         //自分のRigidbody
    [SerializeField] float jumpPower = 0f;                                      //ジャンプする時の力

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        Initialize();
        CreateStateTable();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {
        // nullチェックとキャッシュ
        rigidbody = rigidbody ?? GetComponent<Rigidbody>();
        // ゲームが開始するまで重力が働かないようにする
        rigidbody.useGravity = false;
        // ゲームが開始されたら重力を有効にする
        Action<Unit> action = Unit => rigidbody.useGravity = true;
        EventManager.Inst.Subscribe(SubjectType.OnGameStart, action);
    }

    /// <summary>
    /// ステートマシーンの遷移テーブルの作成
    /// </summary>
    void CreateStateTable()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new PlayerStateMachine<Player>(this);   // 自身がコンテキストになるので自身のインスタンスを渡す
        // 通常状態からの状態遷移も記述
        stateMachine.AddTransition<PlayerNormalState, PlayerJumpState>((int)PlayerStateEventId.Jump);
        // ジャンプしている状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerJumpState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        stateMachine.AddTransition<PlayerJumpState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
        // フックを使用している状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerHookState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
        stateMachine.AddTransition<PlayerHookState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        // 自由落下している状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerFreeFallState, PlayerHookState>((int)PlayerStateEventId.Hook);
        stateMachine.AddTransition<PlayerFreeFallState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        // 起動ステートを設定（起動ステートは PlayerNormalState）
        stateMachine.SetStartState<PlayerNormalState>();
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    void OnCollisionStay(Collision collision)
    {
        stateMachine.OnCollisionStay(collision);
    }

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    void OnCollisionEnter(Collision collision)
    {
        stateMachine.OnCollisionEnter(collision);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    void OnCollisionExit(Collision collision)
    {
        stateMachine.OnCollisionExit(collision);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    void OnTriggerEnter(Collider other)
    {
        stateMachine.OnTriggerEnter(other);
    }

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollider</param>
    void OnTriggerStay(Collider other)
    {
        stateMachine.OnTriggerStay(other);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollider</param>
    void OnTriggerExit(Collider other)
    {
        stateMachine.OnTriggerExit(other);
    }
}
