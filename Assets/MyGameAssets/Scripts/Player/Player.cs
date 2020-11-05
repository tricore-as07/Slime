using UnityEngine;
using UniRx;
using System;
using System.Collections;

// 必要なコンポーネントを定義
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

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
    float tapMomentTime;                                                        //タップされた瞬間を判定する時の誤差の許容範囲をカウントする変数

    // インスペクターに表示する変数
    [SerializeField] new Rigidbody rigidbody = default;                         //自分のRigidbody
    [SerializeField] GameObject playerIce = default;                            //プレイヤーが氷の状態になったら表示するゲームオブジェクト
    [SerializeField] PlayerTentacle playerTentacle = default;                   //プレイヤーがフックを引っ掛けている状態の時に使う触手のゲームオブジェクト
    [SerializeField] PlayerSettingsData playerSettingsData = default;           //プレイヤーの設定データ
    [SerializeField] PhysicMaterial physicMaterial = default;                   //物理特性を設定するマテリアル

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
        physicMaterial = physicMaterial ?? GetComponent<SphereCollider>().material;
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
        physicMaterial.dynamicFriction = playerSettingsData.NormalPlayerFriction;
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
        physicMaterial.dynamicFriction = playerSettingsData.NormalPlayerFriction;
    }

    /// <summary>
    /// ステートマシーンの遷移テーブルの作成
    /// </summary>
    void CreateStateTable()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new PlayerStateMachine<Player>(this);   // 自身がコンテキストになるので自身のインスタンスを渡す
        // 静止状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerStayState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
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
        physicMaterial.dynamicFriction = playerSettingsData.FrozenPlayerFriction;
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
        if (meltIceCoroutine == null)
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
        physicMaterial.dynamicFriction = playerSettingsData.NormalPlayerFriction;
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

#if UNITY_EDITOR
    /// <summary>
    /// タップ入力されているかどうか
    /// </summary>
    /// <returns>タップ入力されている : true,タップ入力されていない : false</returns>
    bool IsTapInput()
    {
        // タップ判定の入力がされているとき
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// タップ入力された瞬間かどうか（誤差許容あり）
    /// </summary>
    /// <returns>タップ入力された瞬間の判定 : true,タップ入力された瞬間の判定じゃない : false</returns>
    bool IsTapInputMoment()
    {
        // タップ判定の入力がされているとき
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            // 前にタップされた時間が記録されてないなら
            if (tapMomentTime == 0f)
            {
                // 前にタップされた時間を記録してtrueを返す
                tapMomentTime = Time.realtimeSinceStartup;
                return true;
            }
            // 前にタップされた時間から経った時間が誤差許容時間なら
            if (Time.realtimeSinceStartup - tapMomentTime < playerSettingsData.TapErrorToleranceTime)
            {
                return true;
            }
            // 誤差許容時間以上の時間が経っていたなら
            else
            {
                return false;
            }
        }
        // 前にタップされた時間の記録をリセット
        tapMomentTime = 0f;
        return false;
    }
#elif UNITY_IOS || UNITY_ANDROID
    /// <summary>
    /// タップ入力されているかどうか
    /// </summary>
    /// <returns>タップ入力されている : true,タップ入力されていない : false</returns>
    bool IsTapInput()
    {
        if (Input.touchCount > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// タップ入力された瞬間かどうか（誤差許容あり）
    /// </summary>
    /// <returns>タップ入力された瞬間の判定 : true,タップ入力された瞬間の判定じゃない : false</returns>
    bool IsTapInputMoment()
    {
        // タップ判定の入力がされているとき
        if (Input.touchCount > 0)
        {
            // 前にタップされた時間が記録されてないなら
            if (tapMomentTime == 0f)
            {
                // 前にタップされた時間を記録してtrueを返す
                tapMomentTime = Time.realtimeSinceStartup;
                return true;
            }
            // 前にタップされた時間から経った時間が誤差許容時間なら
            if (Time.realtimeSinceStartup - tapMomentTime < playerSettingsData.TapErrorToleranceTime)
            {
                return true;
            }
            // 誤差許容時間以上の時間が経っていたなら
            else
            {
                return false;
            }
        }
        tapMomentTime = 0f;
        return false;
    }
#endif
}
