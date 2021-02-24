using UnityEngine;
using VMUnityLib;

/// <summary>
/// プレイヤーを所持するだけのシングルトンクラス
/// </summary>
public class PlayerOwner : Singleton<PlayerOwner>
{
    GameObject player = null;       //プレイヤー

    /// <summary>
    /// プレイヤーを取得する
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayer()
    {
        if(player == null)
        {
            player = PlayerOwner.Inst.GetPlayer();
        }
        return player;
    }
}
