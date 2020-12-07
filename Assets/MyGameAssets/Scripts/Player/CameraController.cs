using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

/// <summary>
/// カメラの制御をする
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] new ProCamera2D camera = default;      //ProCamera2Dのコンポーネント
    float cameraOffsetX;                                    //ターゲットからのオフセット
    float cameraOffsetY;                                    //ターゲットからのオフセット
    GameObject player;                                      //プレイヤー

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // 必要な情報のキャッシュ
        player = GameObject.FindGameObjectWithTag(TagName.Player);
        cameraOffsetX = camera.OffsetX;
        cameraOffsetY = camera.OffsetY;
        // イベントの登録
        EventManager.Inst.Subscribe(SubjectType.OnGameOver, Unit => OnGameOver());
        EventManager.Inst.Subscribe(SubjectType.OnHome, Unit => OnRetry());
        EventManager.Inst.Subscribe(SubjectType.OnRetry, Unit => OnRetry());
    }

    /// <summary>
    /// ゲームオーバー時のカメラ処理
    /// </summary>
    void OnGameOver()
    {
        // カメラのターゲットを無くす
        camera.RemoveAllCameraTargets();
        // カメラのオフセットを0にする
        camera.OffsetX = 0f;
        camera.OffsetY = 0f;
    }

    /// <summary>
    /// リトライ時のカメラの処理
    /// </summary>
    void OnRetry()
    {
        // カメラのターゲットにプレイヤーを追加
        camera.AddCameraTarget(player.transform);
        // カメラのオフセットを設定
        camera.OffsetX = cameraOffsetX;
        camera.OffsetY = cameraOffsetY;
    }
}
