using UnityEngine;
using UniRx;
using System;
using System.Collections;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    // ステートマシーンの状態遷移に使用する列挙型
    enum PlayerStateEventId
    {
        Stay,           //静止状態（ゲームが開始されていない時の状態）
        Normal,         //何もしてない状態
        Hook,           //フックを引っ掛けている状態
        FreeFall        //自由落下している状態
    }
    PlayerStateMachine<Player> stateMachine = default;                          //ステートマシン
    RaycastHit hit;                                                             //プレイヤーステート間でRayに衝突したオブジェクトの情報を共有するための変数
    Vector3 startPosition;                                                      //リトライ時に最初のポジションに戻すキャッシュとして使用
    bool isFrozen = false;                                                      //プレイヤーが凍っているかどうか
    public bool IsFrozen => isFrozen;                                           //プレイヤーが凍っているかどうかを外部に公開するためのプロパティ
    Coroutine meltIceCoroutine;                                                 //氷を溶かすコルーチンのための変数
    float jumpPower;                                                            //ジャンプする時の力
    float jumpPowerByIceConditionFactor;                                        //氷の状態の時のジャンプ力の係数
    float meltIceTime;                                                          //溶ける時間

    // インスペクターに表示する変数
    [SerializeField] new Rigidbody rigidbody = default;                         //自分のRigidbody
    [SerializeField] GameObject playerIce = default;                            //プレイヤーが氷の状態になったら表示するゲームオブジェクト
    [SerializeField] PlayerSettingsData playerSettingsData = default;           //プレイヤーの設定データ

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
        // 開始時のポジションを記憶しておく
        startPosition = transform.position;
        // ゲームオーバーになったらリスタートの関数が呼ばれるようにする
        EventManager.Inst.Subscribe(SubjectType.OnRetry, Unit => Restart());
        // プレイヤーの設定データを反映させる
        jumpPower = playerSettingsData.JumpPower;
        jumpPowerByIceConditionFactor = playerSettingsData.JumpPowerByIceConditionFactor;
        meltIceTime = playerSettingsData.MeltIceTime;
    }

    /// <summary>
    /// 同じステージ内でリスタートする時の処理
    /// </summary>
    void Restart()
    { 
        // プレイヤーのポジションをスタート地点に戻す
        transform.position = startPosition;
        // プレイヤーのRigidbodyのVelocityを0にする
        rigidbody.velocity = Vector3.zero;
        // ゲームが再開されるまで重力を無効にしておく
        rigidbody.useGravity = false;
        // 氷状態の情報を初期化
        isFrozen = false;
        playerIce.SetActive(false);
        meltIceCoroutine = null;
    }

    /// <summary>
    /// ステートマシーンの遷移テーブルの作成
    /// </summary>
    void CreateStateTable()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new PlayerStateMachine<Player>(this);   // 自身がコンテキストになるので自身のインスタンスを渡す
        // 静止状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerStayState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        // 通常状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerNormalState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
        // フックを使用している状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerHookState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
        stateMachine.AddTransition<PlayerHookState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        // 自由落下している状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerFreeFallState, PlayerHookState>((int)PlayerStateEventId.Hook);
        stateMachine.AddTransition<PlayerFreeFallState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        // 起動ステートを設定（起動ステートは PlayerNormalState）
        stateMachine.SetStartState<PlayerStayState>();
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
    /// 氷のギミックに入った時
    /// </summary>
    public void OnIceGimmickEnter()
    {
        // プレイヤーを凍っている状態にする
        isFrozen = true;
        playerIce.SetActive(true);
        // 氷を溶かしている途中だったら
        if (meltIceCoroutine != null)
        {
            StopCoroutine(meltIceCoroutine);
            meltIceCoroutine = null;
        }
    }

    /// <summary>
    /// 氷の状態で炎のギミックに入った時
    /// </summary>
    public void OnFlameGimmickEnterByIceCondition()
    {
        // 氷を溶かしている途中じゃなければ
        if(meltIceCoroutine == null)
        {
            // 氷を溶かす処理を開始する
            meltIceCoroutine = StartCoroutine(MeltIce());
        }
    }

    /// <summary>
    /// 氷を溶かす処理
    /// </summary>
    IEnumerator MeltIce()
    {
        // 設定されてる氷が溶ける時間待つ
        yield return new WaitForSeconds(meltIceTime);
        // 氷状態を解除して、溶かしている最中をやめる
        isFrozen = false;
        playerIce.SetActive(false); 
        meltIceCoroutine = null;
    }

    /// <summary>
    /// ジャンプ力にかける係数を取得する
    /// </summary>
    /// <returns>ジャンプ力にかける係数</returns>
    float GetJumpPowerFactor()
    {
        // 氷の状態なら
        if(IsFrozen)
        {
            // 氷の状態のジャンプ力にかける係数を返す
            return jumpPowerByIceConditionFactor;
        }
        // 通常状態なら1を返す
        return 1f;
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
    /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    void OnTriggerEnter(Collider other)
    {
        stateMachine.OnTriggerEnter(other);
    }

    /// <summary>
    /// ２つのColliderが衝突している最中に呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    void OnTriggerStay(Collider other)
    {
        stateMachine.OnTriggerStay(other);
    }

    /// <summary>
    /// ２つのColliderが衝突しなくなったフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    void OnTriggerExit(Collider other)
    {
        stateMachine.OnTriggerExit(other);
    }
}
