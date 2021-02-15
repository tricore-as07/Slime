using UnityEngine;

/// <summary>
/// プレイヤーに追従する
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    GameObject player;          //プレイヤーのオブジェクト

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        //プレイヤーをキャッシュ
        player = GameObject.FindGameObjectWithTag(TagName.Player);
    }

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        //プレイヤーのポジションだけ追従させる
        transform.position = player.transform.position;
    }
}
