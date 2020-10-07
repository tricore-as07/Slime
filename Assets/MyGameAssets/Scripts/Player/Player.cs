using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public class Player : MonoBehaviour
{
    // インスペクターに表示する変数
    [SerializeField] new Rigidbody rigidbody = default;
    [SerializeField] GameStartTap gameStartTap = default;

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        Initialize();
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
}
