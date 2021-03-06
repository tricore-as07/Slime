﻿using UnityEngine;

/// <summary>
/// 棘のオブジェクトの処理をする
/// </summary>
public class SpineObject : MonoBehaviour
{
    Player player;           //プレイヤーのクラス

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される
    /// </summary>
    /// <param name="collision">この衝突に含まれるその他のCollision</param>
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトがプレイヤーなら
        if (collision.gameObject.tag == TagName.Player)
        {
            // プレイヤーのクラスを持っていなければキャッシュする 
            player = player ?? collision.gameObject.GetComponent<Player>();
            // プレイヤーが氷の状態じゃなければゲームオーバーにする
            if(!player.IsFrozen)
            {
                // ゲームオーバー処理を実行する
                player.OnGameOverBySpine();
                EventManager.Inst.InvokeEvent(SubjectType.OnGameOver);
            }
        }
    }
}
