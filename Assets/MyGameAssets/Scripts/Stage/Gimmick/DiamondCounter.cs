using VMUnityLib;

/// <summary>
/// ダイヤモンドの合計数を数えるカウンタークラス
/// </summary>
public class DiamondCounter : Singleton<DiamondCounter>
{
    int diamondTotalNum = 0;                        //ダイヤモンドの合計取得数
    public int DiamondToralNum => diamondTotalNum;  //外部に公開するためのプロパティ

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DiamondCounter()
    {
        UpdateDiamondTotalNum();
    }

    /// <summary>
    /// ダイヤモンドの合計数を更新する
    /// </summary>
    public void UpdateDiamondTotalNum()
    {
        var beforeDiamondTotalNum = diamondTotalNum;
        // 一度ダイヤモンドの数をリセット
        diamondTotalNum = 0;
        // セーブデータから今のダイヤモンドの取得情報をもってくる
        var dataList = SaveDataManager.Inst.GetDiamondAcquisitionDataList();
        // 合計数の計算
        for (int i = 0; i < dataList.Count; i++)
        {
            for (int j = 0; j < dataList[i].isDiamondAcquisitionList.Count; j++)
            {
                // trueなら1をfalseなら0を加算する
                diamondTotalNum += dataList[i].isDiamondAcquisitionList[j] ? 1 : 0;
            }
        }
        // 計算する前の合計数と計算後の合計数が一緒じゃなければ
        if(beforeDiamondTotalNum != diamondTotalNum)
        {
            // ダイヤモンドの合計数が変わったイベントを呼ぶ
            EventManager.Inst.InvokeEvent(SubjectType.OnChangeDiamondNum);
        }
    }
}
