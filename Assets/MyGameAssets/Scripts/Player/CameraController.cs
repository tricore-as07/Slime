using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

/// <summary>
/// カメラの制御をする
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] ProCamera2D proCamera = default;       //ProCamera2Dのコンポーネント
    float cameraOffsetX;                                    //ターゲットからのオフセット
    float cameraOffsetY;                                    //ターゲットからのオフセット
    GameObject player;                                      //プレイヤー

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // 必要な情報のキャッシュ
        cameraOffsetX = proCamera.OffsetX;
        cameraOffsetY = proCamera.OffsetY;
        // イベントの登録
        EventManager.Inst.Subscribe(SubjectType.OnGameOver, Unit => OnGameOver(), gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnRetry, Unit => OnRetry(), gameObject);
        // カメラのターゲットにプレイヤーを追加
    }

    /// <summary>
    /// Updateの後に呼ばれる
    /// </summary>
    void LateUpdate()
    {
        if(proCamera.CameraTargets.Count == 0 && proCamera.OffsetX != 0f && proCamera.OffsetY != 0f)
        {
            proCamera.AddCameraTarget(PlayerAccessor.Inst.GetPlayer().transform);
        }
    }

    /// <summary>
    /// ゲームオーバー時のカメラ処理
    /// </summary>
    void OnGameOver()
    {
        // カメラのターゲットを無くす
        proCamera.RemoveAllCameraTargets();
        // カメラのオフセットを0にする
        proCamera.OffsetX = 0f;
        proCamera.OffsetY = 0f;
    }

    /// <summary>
    /// リトライ時のカメラの処理
    /// </summary>
    void OnRetry()
    {
        // カメラのターゲットにプレイヤーを追加
        proCamera.AddCameraTarget(PlayerAccessor.Inst.GetPlayer().transform);
        // カメラのオフセットを設定
        proCamera.OffsetX = cameraOffsetX;
        proCamera.OffsetY = cameraOffsetY;
    }
}
