using UnityEngine;
using UniRx;
using System;

/// <summary>
/// プレイヤーに関する処理を行う
/// </summary>
public partial class Player : MonoBehaviour
{
    /// <summary>
    /// プレイヤーが入力を受け付けない時のステート
    /// </summary>
    public class PlayerDoNotAcceptInputState : PlayerStateMachine<Player>.PlayerState
    {
        /// <summary>
        /// 状態へ突入時の処理はこのEnterで行う
        /// </summary>
        protected internal override void Enter()
        {
            // ステートをStayにするイベントを呼ぶ処理をデリゲートにする
            Action<Unit> action = Unit => stateMachine.SendEvent((int)PlayerStateEventId.Stay);
            // 上記のデリゲートを下の三つのイベント時に実行するようにする
            EventManager.Inst.Subscribe(SubjectType.OnRetry, action);
            EventManager.Inst.Subscribe(SubjectType.OnHome, action);
            EventManager.Inst.Subscribe(SubjectType.OnNextStage, action);
        }
    }
}
