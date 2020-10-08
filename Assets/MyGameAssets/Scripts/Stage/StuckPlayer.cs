using UnityEngine;
using UniRx;

/// <summary>
/// プレイヤーをゲーム開始まで足止めする
/// </summary>
public class StuckPlayer : MonoBehaviour
{
    // インスペクターに表示する変数
    [SerializeField] GameStartTap gameStartTap = default;

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {
        // nullチェックとキャッシュ
        gameStartTap = gameStartTap ?? GameObject.FindGameObjectWithTag("TapToStart").GetComponent<GameStartTap>();
        // ゲームが開始されたら自分を非アクティブにする
        gameStartTap?.OnGameStarted.Subscribe(Unit => gameObject.SetActive(false));
    }
}
