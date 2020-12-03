﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
/// <summary>
/// スキン選択ボタン
/// </summary>
public class SkinSelectButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] SkinId id;                                 //ボタンが押された時に変更されるスキン
    [SerializeField] UnlockType unlockType = UnlockType.None;   //スキンを開放する条件
    [SerializeField] int unlockNum = default;                   //開放する際の条件となる数

    /// <summary>
    /// 解除する条件の種類
    /// </summary>
    public enum UnlockType
    {
        None,                   //なし
        clearStageNum,          //ステージのクリア数
        diamondNum              //ダイヤモンドの取得数
    }

    /// <summary>
    /// Editor上で初期化する際の処理
    /// </summary>
    void Reset()
    {
        button = button ?? GetComponent<Button>();
    }

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        switch (unlockType)
        {
            case UnlockType.None: break;
            case UnlockType.clearStageNum:
                {
                    UpdateIsUnlockByClearStageNum();
                    EventManager.Inst.Subscribe(SubjectType.OnChangeClearStageNum,Unit => UpdateIsUnlockByClearStageNum());
                    break;
                }
            case UnlockType.diamondNum:
                {
                    UpdateIsUnlockByDiamondNum();
                    EventManager.Inst.Subscribe(SubjectType.OnChangeDiamondNum, Unit => UpdateIsUnlockByDiamondNum());
                    break;
                }
        }
    }

    /// <summary>
    /// アンロックするかどうかをクリアしたステージ数で更新する
    /// </summary>
    void UpdateIsUnlockByClearStageNum()
    {
        var clearStageNum = SaveDataManager.Inst.GetClearStageNum();
        button.interactable = clearStageNum >= unlockNum;
    }

    /// <summary>
    /// アンロックするかどうかをダイヤモンドの取得数で更新する
    /// </summary>
    void UpdateIsUnlockByDiamondNum()
    {
        var diamondNum = DiamondCounter.Inst.DiamondToralNum;
        button.interactable = diamondNum >= unlockNum;
    }

    /// <summary>
    /// ボタンがクリックされた時
    /// </summary>
    public void OnClick()
    {
        SkinManager.Inst.OnSelectSkin(id);
    }
}
