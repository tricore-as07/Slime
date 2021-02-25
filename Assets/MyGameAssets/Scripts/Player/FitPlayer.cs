using UnityEngine;

/// <summary>
/// プレイヤーに付いていく
/// </summary>
public class FitPlayer : MonoBehaviour
{
    [SerializeField] GameObject player = default;       //プレイヤーのオブジェクト

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    private void Start()
    {
        player = PlayerAccessor.Inst.GetPlayer();
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        // 自分のポジションをプレイヤーのポジションと同じにする
        transform.position = player.transform.position;
    }
}
