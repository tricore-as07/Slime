using UnityEngine;

/// <summary>
/// プレイヤーに付いていく
/// </summary>
public class FitPlayer : MonoBehaviour
{
    [SerializeField] GameObject player = default;       //プレイヤーのオブジェクト

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        if(player == null)
        {
            player = PlayerAccessor.Inst.GetPlayer();
        }
        // 自分のポジションをプレイヤーのポジションと同じにする
        transform.position = player.transform.position;
    }
}
