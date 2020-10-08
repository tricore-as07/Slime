using IceMilkTea.Core;
using UnityEngine;
using UniRx;
using System;
using TMPro;

/// <summary>
/// タップされたらゲームが開始されるようにする
/// </summary>
public class GameStartTap : MonoBehaviour
{
    // ステートマシーンの状態遷移に使用する列挙型
    enum StateEventId
    {
        StartGame,          //ゲームがスタートされた時
        Restart             //ゲームがリスタートされた時
    }
    ImtStateMachine<GameStartTap> stateMachine;                         //ステートマシン
    Subject<Unit> gameStartSubject = new Subject<Unit>();               //ゲームが開始したことを知らせるSubject
    public IObservable<Unit> OnGameStarted => gameStartSubject;         //ゲームが開始されたら呼ばれるイベント　※購読側だけを公開

    // インスペクターに表示する変数
    [SerializeField] TMP_Text topToStartText = default;                 //TMPのタップを促すために表示するテキスト

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    private void Awake()
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
        topToStartText = topToStartText ?? GetComponent<TMP_Text>();
    }

    /// <summary>
    /// ステートマシーンの遷移テーブルの作成
    /// </summary>
    void CreateStateTable()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new ImtStateMachine<GameStartTap>(this); // 自身がコンテキストになるので自身のインスタンスを渡す
        stateMachine.AddTransition<BeforeGameStartState, AfterGameStartState>((int)StateEventId.StartGame);
        stateMachine.AddTransition<AfterGameStartState, BeforeGameStartState>((int)StateEventId.Restart);
        // 起動ステートを設定（起動ステートは EnableState）
        stateMachine.SetStartState<BeforeGameStartState>();
    }

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        stateMachine.Update();
    }

    /// <summary>
    /// ゲームが開始される前の処理
    /// </summary>
    private class BeforeGameStartState : ImtStateMachine<GameStartTap>.State
    {
        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // タップを促すためのテキストをアクティブにする
            Context.topToStartText.enabled = true;
        }

        /// <summary>
        /// 状態の更新はこのUpdateで行う
        /// </summary>
        protected internal override void Update()
        {
            // タップされているか、何かのキーが入力されている時
            if(Input.touchCount > 0 || Input.anyKey)
            {
                stateMachine.SendEvent((int)StateEventId.StartGame);
            }
        }
    }

    /// <summary>
    /// ゲームが開始された後の処理
    /// </summary>
    private class AfterGameStartState : ImtStateMachine<GameStartTap>.State
    {
        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // タップを促すためのテキストを非アクティブにする
            Context.topToStartText.enabled = false;
            // ゲームが開始したことを知らせる
            Context.gameStartSubject.OnNext(Unit.Default);
        }
    }
}
