using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダイヤモンドの管理をする
/// </summary>
public class DiamondManager : MonoBehaviour
{
    // FIXME: orimoto ステージのエディタが完成したらステージ作成時に取得してくるように修正
    int stageSerialNum = 1;                                     //ステージの識別番号
    List<GameObject> diamonds = new List<GameObject>();         //ステージにあるダイヤモンドのリスト
    DiamondAcquisitionData diamondAcquisitionData;              //ダイヤモンドの獲得状況がセーブされているデータ

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        Initialize();
        EventManager.Inst.Subscribe(SubjectType.OnGameClear, Unit => OnGameClearProcess());
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Initialize()
    {
        // Diamondのタグのゲームオブジェクトをリストに格納
        diamonds.AddRange(GameObject.FindGameObjectsWithTag(TagName.Diamond));
        // DiamondのオブジェクトをX座標を元にソート
        diamonds.Sort(
            (obj1, obj2) =>
            (int)(obj1.transform.position.x - obj2.transform.position.x)
            );
        // ダイヤモンドの獲得データをセーブデータから取得してくる
        diamondAcquisitionData = SaveDataManager.Inst.GetDiamondAcquisitionData(stageSerialNum);
        // セーブされているデータとステージに存在しているダイヤモンドの数が同じなら
        if (diamonds.Count == diamondAcquisitionData.isDiamondAcquisitionList.Count)
        {
            // 既に取得している識別番号のダイヤモンドは非アクティブにする
            for (int i = 0; i < diamonds.Count; i++)
            {
                // 獲得しているダイヤモンド（trueになっているもの）は
                // 非表示（false）にしたいのでboolを反転して上書き
                diamonds[i].SetActive(!diamondAcquisitionData.isDiamondAcquisitionList[i]);
            }
        }
        // セーブされているデータとステージに存在しているダイヤモンドの数が違うなら（未プレイで仮データの状態）
        else
        {
            // リストを作り直す
            diamondAcquisitionData.isDiamondAcquisitionList = new List<bool>();
            // ステージに存在しているダイヤモンドの数と同じになるまでセーブデータ用のダイヤモンドの獲得データ数を増やす
            while (diamonds.Count > diamondAcquisitionData.isDiamondAcquisitionList.Count)
            {
                diamondAcquisitionData.isDiamondAcquisitionList.Add(false);
            }
        }
    }

    /// <summary>
    /// ダイヤモンドの識別番号を返す
    /// </summary>
    /// <param name="diamond">識別番号の知りたいダイヤモンドのゲームオブジェクト</param>
    /// <returns>識別番号</returns>
    public int GetDiamondIdentificationNumber(GameObject diamond)
    {
        return diamonds.IndexOf(diamond);
    }

    /// <summary>
    /// ゲームクリアした時の処理
    /// </summary>
    void OnGameClearProcess()
    {
        // ダイヤモンドの獲得状況を更新
        for (int i = 0; i < diamondAcquisitionData.isDiamondAcquisitionList.Count; i++)
        {
            // 同じ識別番号の箇所を上書き
            // ダイヤモンドが獲得されたらオブジェクトのアクティブがfalseになっているのでboolを反転して代入
            diamondAcquisitionData.isDiamondAcquisitionList[i] = !diamonds[i].activeSelf;
        }
        // ダイヤモンドの獲得状況をセーブする
        SaveDataManager.Inst.SaveDiamondAcquisitionData(stageSerialNum,diamondAcquisitionData);
    }
}
