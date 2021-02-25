using UnityEngine;
using VMUnityLib;

/// <summary>
/// プレイヤーを所持するだけのシングルトンクラス
/// </summary>
public class PlayerAccessor : Singleton<PlayerAccessor>
{
    GameObject player = null;       //プレイヤー

    /// <summary>
    /// プレイヤーを取得する
    /// </summary>
    /// <returns>プレイヤーオブジェクトのインスタンス</returns>
    public GameObject GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(TagName.Player);
        }
        return player;
    }
}
