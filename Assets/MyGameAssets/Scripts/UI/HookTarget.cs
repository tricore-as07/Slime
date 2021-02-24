using UnityEngine;

// 必要なコンポーネントを定義
[RequireComponent(typeof(RectTransform))]

/// <summary>
/// フックのターゲットが見つかった時にUIを表示するためのクラス
/// </summary>
public class HookTarget : MonoBehaviour
{
    [SerializeField] Player player = default;                   //プレイヤー
    [SerializeField] RectTransform rectTransform = default;     //矩形の位置情報
    Vector3 targetPos;                                          //フックのターゲットのポジション

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        EventManager.Inst.Subscribe(SubjectType.OnFoundHook,Unit => OnFoundTargetHook());
        EventManager.Inst.Subscribe(SubjectType.OnNotFoundHook,Unit => OnCanNotFoundTargetHook());
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        FixTargetHook();
    }

    /// <summary>
    /// フックのターゲットが見つかった時に呼ばれる
    /// </summary>
    void OnFoundTargetHook()
    {
        gameObject.SetActive(true);
        player = player ?? PlayerAccessor.Inst.GetPlayer().GetComponent<Player>();
        targetPos = player.TargetHookPosition;
        FixTargetHook();
    }

    /// <summary>
    /// フックのターゲットが見つからなくなった時に呼ばれる
    /// </summary>
    void OnCanNotFoundTargetHook()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// フックのターゲットの位置に調整する
    /// </summary>
    void FixTargetHook()
    {
        rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPos);
    }
}
