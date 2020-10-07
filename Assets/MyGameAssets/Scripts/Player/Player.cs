using IceMilkTea.Core;
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
        Normal,
        Jump
    }
    ImtStateMachine<Player> stateMachine = default;                             //ステートマシン
    Subject<GameObject> onCollisionStaySubject = new Subject<GameObject>();     //プレイヤーが何かに衝突したことを知らせるSubject
    IObservable<GameObject> OnCollisionStayEvent => onCollisionStaySubject;     //プレイヤーが何かに衝突したら呼ばれるイベント
    Subject<GameObject> onCollisionEnterSubject = new Subject<GameObject>();     //プレイヤーが何かに衝突したことを知らせるSubject
    IObservable<GameObject> OnCollisionEnterEvent => onCollisionEnterSubject;     //プレイヤーが何かに衝突したら呼ばれるイベント
    Subject<GameObject> onCollisionExitSubject = new Subject<GameObject>();     //プレイヤーが何かに衝突したことを知らせるSubject
    IObservable<GameObject> OnCollisionExitEvent => onCollisionExitSubject;     //プレイヤーが何かに衝突したら呼ばれるイベント

    // インスペクターに表示する変数
    [SerializeField] new Rigidbody rigidbody = default;                         //自分のRigidbody
    [SerializeField] GameStartTap gameStartTap = default;                       //ゲーム開始を知るためのクラス
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
        //nullチェックとキャッシュ
        rigidbody = rigidbody ?? GetComponent<Rigidbody>();
        gameStartTap = gameStartTap ?? GameObject.FindGameObjectWithTag("TapToStart").GetComponent<GameStartTap>();

        //ゲームが開始するまで重力が働かないようにする
        rigidbody.useGravity = false;
        //ゲームが開始されたら重力を有効にする
        gameStartTap?.OnGameStarted.Subscribe(Unit => rigidbody.useGravity = true);
    }

    /// <summary>
    /// ステートマシーンの遷移テーブルの作成
    /// </summary>
    void CreateStateTable()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new ImtStateMachine<Player>(this);   // 自身がコンテキストになるので自身のインスタンスを渡す
        stateMachine.AddTransition<PlayerNormalState, PlayerJumpState>((int)PlayerStateEventId.Jump);
        stateMachine.AddTransition<PlayerJumpState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        // 起動ステートを設定（起動ステートは PlayerNormalState）
        stateMachine.SetStartState<PlayerNormalState>();
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    private void Update()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    private void Start()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    private void OnCollisionStay(Collision collision)
    {
        onCollisionStaySubject.OnNext(collision.gameObject);
    }

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnterSubject.OnNext(collision.gameObject);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    private void OnCollisionExit(Collision collision)
    {
        onCollisionExitSubject.OnNext(collision.gameObject);
    }
}
