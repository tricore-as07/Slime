using UnityEngine;
using UniRx;
using System;
using System.Collections;
using Kalagaan;

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
        FreeFall,       //自由落下している状態
        DoNotAcceptInput//入力を受け付けない状態
    }
    PlayerStateMachine<Player> stateMachine = default;                          //ステートマシン
    GameObject hookObject;                                                      //フックのオブジェクト
    Vector3 targetHookPosition;                                                 //ターゲットのフックのポジション
    public Vector3 TargetHookPosition => targetHookPosition;                    //ターゲットのフックのポジションを外部に公開するためのプロパティ   
    Vector3 startPosition;                                                      //リトライ時に最初のポジションに戻すキャッシュとして使用
    bool isFrozen = false;                                                      //プレイヤーが凍っているかどうか
    public bool IsFrozen => isFrozen;                                           //プレイヤーが凍っているかどうかを外部に公開するためのプロパティ
    Coroutine meltIceCoroutine;                                                 //氷を溶かすコルーチンのための変数
    float jumpPower;                                                            //ジャンプする時の力
    float jumpPowerByIceConditionFactor;                                        //氷の状態の時のジャンプ力の係数
    float meltIceTime;                                                          //溶ける時間
    float tapMomentTime;                                                        //タップされた瞬間を判定する時の誤差の許容範囲をカウントする変数
    bool isGamePlay;                                                            //ゲームプレイ中かどうか
    GameObject playerLooks = default;                                           //プレイヤーの見た目のオブジェクト
    VertExmotionSensorBase sensor = default;                                    //プレイヤーのVertMotion用センサー

    // インスペクターに表示する変数
    [SerializeField] new Rigidbody rigidbody = default;                         //自分のRigidbody
    [SerializeField] GameObject playerIce = default;                            //プレイヤーが氷の状態になったら表示するゲームオブジェクト
    [SerializeField] PlayerTentacle playerTentacle = default;                   //プレイヤーがフックを引っ掛けている状態の時に使う触手のゲームオブジェクト
    [SerializeField] PlayerSettingsData playerSettingsData = default;           //プレイヤーの設定データ
    [SerializeField] PhysicMaterial physicMaterial = default;                   //物理特性を設定するマテリアル
    [SerializeField] GameObject gameOverEffectBySpine = default;                //棘でゲームオーバーになった時のエフェクトオブジェクト
    [SerializeField] GameObject gameOverEffectByFlame = default;                //炎でゲームオーバーになった時のエフェクトオブジェクト
    [SerializeField] GameObject freezeEffect = default;                         //凍る時のエフェクトオブジェクト
    [SerializeField] GameObject collisionEffect = default;                      //衝突した時に表示するオブジェクト
    [SerializeField] TrailRenderer trailRenderer = default;                     //トレールレンダラー

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
        EventManager.Inst.Subscribe(SubjectType.OnGameStart, action, gameObject);
        // 開始時のポジションを記憶しておく
        startPosition = transform.position;
        // ゲームオーバーになったらリスタートの関数が呼ばれるようにする
        EventManager.Inst.Subscribe(SubjectType.OnRetry, Unit => Restart(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnNextStage, Unit => Restart(), gameObject);
        // 呼ばれたイベントに応じてゲーム中かどうかのフラグを切り替える
        EventManager.Inst.Subscribe(SubjectType.OnChangeSkin, Unit => ChangeSkin(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnGameOver, Unit => stateMachine.SendEvent((int)PlayerStateEventId.DoNotAcceptInput), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnGameClear, Unit => stateMachine.SendEvent((int)PlayerStateEventId.DoNotAcceptInput), gameObject);
        // プレイヤーの設定データを反映させる
        jumpPower = playerSettingsData.JumpPower;
        jumpPowerByIceConditionFactor = playerSettingsData.JumpPowerByIceConditionFactor;
        meltIceTime = playerSettingsData.MeltIceTime;
        physicMaterial.dynamicFriction = playerSettingsData.NormalPlayerFriction;
        ChangeSkin();
    }

    /// <summary>
    /// 同じステージ内でリスタートする時の処理
    /// </summary>
    void Restart()
    {
        trailRenderer.enabled = false;
        // プレイヤーのポジションをスタート地点に戻す
        transform.position = startPosition;
        // プレイヤーのRigidbodyのVelocityを0にする
        rigidbody.velocity = Vector3.zero;
        // ゲームが再開されるまで重力を無効にしておく
        rigidbody.useGravity = false;
        // 氷状態の情報を初期化
        isFrozen = false;
        playerIce.SetActive(false);
        playerLooks.SetActive(true);
        meltIceCoroutine = null;
        physicMaterial.dynamicFriction = playerSettingsData.NormalPlayerFriction;
        stateMachine.SendEvent((int)PlayerStateEventId.Stay);
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
        stateMachine.AddTransition<PlayerStayState, PlayerStayState>((int)PlayerStateEventId.Stay);
        // 通常状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerNormalState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
        stateMachine.AddTransition<PlayerNormalState, PlayerDoNotAcceptInputState>((int)PlayerStateEventId.DoNotAcceptInput);
        stateMachine.AddTransition<PlayerNormalState, PlayerStayState>((int)PlayerStateEventId.Stay);
        // フックを使用している状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerHookState, PlayerFreeFallState>((int)PlayerStateEventId.FreeFall);
        stateMachine.AddTransition<PlayerHookState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        stateMachine.AddTransition<PlayerHookState, PlayerDoNotAcceptInputState>((int)PlayerStateEventId.DoNotAcceptInput);
        stateMachine.AddTransition<PlayerHookState, PlayerStayState>((int)PlayerStateEventId.Stay);
        // 自由落下している状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerFreeFallState, PlayerHookState>((int)PlayerStateEventId.Hook);
        stateMachine.AddTransition<PlayerFreeFallState, PlayerNormalState>((int)PlayerStateEventId.Normal);
        stateMachine.AddTransition<PlayerFreeFallState, PlayerDoNotAcceptInputState>((int)PlayerStateEventId.DoNotAcceptInput);
        stateMachine.AddTransition<PlayerFreeFallState, PlayerStayState>((int)PlayerStateEventId.Stay);
        // 入力を受け付けない状態からの状態遷移の記述
        stateMachine.AddTransition<PlayerDoNotAcceptInputState, PlayerStayState>((int)PlayerStateEventId.Stay);
        //stateMachine.AddAnyTransition<PlayerStayState>((int)PlayerStateEventId.Stay);
        // 起動ステートを設定（起動ステートは PlayerNormalState）
        stateMachine.SetStartState<PlayerStayState>();
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        stateMachine.Update();
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
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
        Instantiate(freezeEffect, transform.parent);
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
        // エフェクトを作成
        var obj = Instantiate(gameOverEffectByFlame, transform.parent);
        obj.transform.position = transform.position;
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
        CreateCollisionEffect(collision);
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

    /// <summary>
    /// スキンを変更させる
    /// </summary>
    void ChangeSkin()
    {
        Destroy(playerLooks);
        playerLooks = Instantiate(SkinManager.Inst.GetNowSkin(), transform.parent);
        // プレイヤーのスキンオブジェクトの子オブジェクトが１つじゃなかったら
        if(playerLooks.transform.childCount != 1)
        {
            Debug.LogError("プレイヤースキンオブジェクトの子オブジェクトが一つではありません。\r\nプレイヤーのシステムが正常に動作しない可能性があります。");
        }
        sensor = playerLooks.transform.GetChild(0).GetComponent<VertExmotionSensorBase>();
    }

    /// <summary>
    /// タップ入力されているかどうか
    /// </summary>
    /// <returns>タップ入力されている : true,タップ入力されていない : false</returns>
    bool IsTapInput()
    {
        // タップ判定の入力がされているとき
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
#endif
        {
            Debug.Log("入力!!!!!!!!!!!!");
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
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
#endif
        {
            // 前にタップされた時間が記録されてないなら
            if (tapMomentTime == 0f)
            {
                // 前にタップされた時間を記録してtrueを返す
                tapMomentTime = Time.realtimeSinceStartup;
                Debug.Log("入力!!!!!!!!!!!!");
                return true;
            }
            // 前にタップされた時間から経った時間が誤差許容時間なら
            if (Time.realtimeSinceStartup - tapMomentTime < playerSettingsData.TapErrorToleranceTime)
            {
                Debug.Log("入力!!!!!!!!!!!!");
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

    /// <summary>
    /// 棘のオブジェクトでゲームオーバーになった時に呼ばれる
    /// </summary>
    public void OnGameOverBySpine()
    {
        if(playerLooks.activeSelf)
        {
            // エフェクトを作成
            var obj = Instantiate(gameOverEffectBySpine, transform.parent);
            obj.transform.position = transform.position;
            playerLooks.SetActive(false);
            trailRenderer.enabled = false;
        }
    }

    /// <summary>
    /// 炎のオブジェクトでゲームオーバーになった時に呼ばれる
    /// </summary>
    public void OnGameOverByFlame()
    {
        // エフェクトを作成
        var obj = Instantiate(gameOverEffectByFlame, transform.parent);
        obj.transform.position = transform.position;
        playerLooks.SetActive(false);
        trailRenderer.enabled = false;
    }

    /// <summary>
    /// 衝突エフェクトを生成する
    /// </summary>
    /// <param name="collision">衝突情報クラス</param>
    void CreateCollisionEffect(Collision collision)
    {
        foreach(var contact in collision.contacts)
        {
            if(contact.otherCollider.tag == TagName.Ground)
            {
                var obj = Instantiate(collisionEffect,transform.parent);
                obj.transform.position = contact.point;
                obj.transform.up = contact.normal;
            }
        }
    }
}
