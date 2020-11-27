using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// それぞれのステージの要素（ダイヤモンド）を更新する
/// </summary>
public class StageElementUpdater : MonoBehaviour
{
    [SerializeField] List<DiamondStatus> itemList;              //アイテムのリスト
    [SerializeField] StageSelectButton stageSelectButton;       //ステージ選択ボタン

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        StageElementUpdate();
    }

    /// <summary>
    /// ステージの要素を更新する
    /// </summary>
    public void StageElementUpdate()
    {
        // ダイヤモンドの獲得情報をセーブデータから取得する
        DiamondAcquisitionData diamondAcquisitionData = SaveDataManager.Inst.GetDiamondAcquisitionData(stageSelectButton.StageNum);
        for (int i = 0; i < diamondAcquisitionData.isDiamondAcquisitionList.Count; i++)
        {
            // ダイヤモンドの数だけ取得情報を表示するUIをアクティブにする
            itemList[i].gameObject.SetActive(true);
            // ダイヤモンドが取得されているかどうかを更新する
            itemList[i].UpdateAcquisitionStatus(diamondAcquisitionData.isDiamondAcquisitionList[i]);
        }
    }
}
