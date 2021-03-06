﻿using UnityEngine;

/// <summary>
/// 炎のギミックの処理をする
/// </summary>
public class FlameGimmick : MonoBehaviour
{
    Player player;           //プレイヤーのクラス

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    private void OnTriggerEnter(Collider other)
    {
        // 衝突したオブジェクトがプレイヤーなら
        if(other.tag == TagName.Player)
        {
            // プレイヤーのクラスを持っていなければキャッシュする 
            player = player ?? other.GetComponent<Player>();
            // プレイヤーが氷の状態じゃなければゲームオーバーにする
            if (!player.IsFrozen)
            {
                player.OnGameOverByFlame();
                // ゲームオーバー処理を実行する
                EventManager.Inst.InvokeEvent(SubjectType.OnGameOver);
            }
            else
            {
                // 氷の状態で炎のギミックに入ったときの処理をする
                player.OnFlameGimmickEnterByIceCondition();
            }
        }
    }
}
