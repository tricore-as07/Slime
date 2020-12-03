using System.Collections.Generic;
using VMUnityLib;

/// <summary>
/// セーブデータの管理をする（セーブ、ロード等）
/// </summary>
public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    SaveData saveData;          //セーブデータ

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        saveData = new SaveData();
        // セーブデータのファイルが存在しているなら
        if (JsonDataSaver.FileExists<SaveData>(saveData))
        {
            // セーブデータのファイルが存在しているならそのファイルをロードする
            saveData = JsonDataSaver.Load<SaveData>(saveData);
        }
        // セーブデータのファイルが存在しないなら
        else
        {
            // 初期化したセーブデータを保存してファイルを作成しておく
            JsonDataSaver.Save<SaveData>(saveData);
        }
    }

    /// <summary>
    /// ダイヤモンドの獲得データをセーブする
    /// </summary>
    /// <param name="stageNum">セーブするステージ数</param>
    /// <param name="diamondAcquisitionData">セーブするダイヤモンドの獲得データ</param>
    public void SaveDiamondAcquisitionData(int stageNum, DiamondAcquisitionData diamondAcquisitionData)
    {
        // 既にセーブされてるステージのリストがこれからセーブするステージ数分ないなら
        // 今のステージ数に達するまで追加する
        while (saveData.stageSaveData.diamondAcquisitionDataList.Count < stageNum)
        {
            saveData.stageSaveData.diamondAcquisitionDataList.Add(new DiamondAcquisitionData());
        }
        // 新しいリストの作成
        DiamondAcquisitionData newDiamondAcquisitionData = new DiamondAcquisitionData();
        // 新しいリストにダイヤを取ったかどうかのリストをまとめて追加
        newDiamondAcquisitionData.isDiamondAcquisitionList = new List<bool>(diamondAcquisitionData.isDiamondAcquisitionList);
        // 新しく作ったリストで上書きする
        saveData.stageSaveData.diamondAcquisitionDataList[stageNum - 1] = newDiamondAcquisitionData;
        JsonDataSaver.Save<SaveData>(saveData);
    }

    /// <summary>
    /// ダイヤモンドの獲得データを取得する
    /// </summary>
    /// <param name="stageNum">取得したいステージ数</param>
    /// <returns>該当ステージのダイヤモンドの獲得データ</returns>
    public DiamondAcquisitionData GetDiamondAcquisitionData(int stageNum)
    {
        // 取得したいステージ数がダイヤモンドの獲得データのリストの数以下なら（取得するデータが存在する場合）
        if (saveData.stageSaveData.diamondAcquisitionDataList.Count >= stageNum)
        {
            // 該当ステージのデータをコピーして返す
            DiamondAcquisitionData diamondAcquisitionData = new DiamondAcquisitionData(saveData.stageSaveData.diamondAcquisitionDataList[stageNum - 1]);
            return diamondAcquisitionData;
        }
        // 該当ステージが存在しない場合は新しくDiamondAcquisitionDataを作って返す
        return new DiamondAcquisitionData();
    }

    /// <summary>
    /// クリアしたステージ数をセーブする
    /// </summary>
    /// <param name="clearStageNum">クリアしたステージ数</param>
    public void SaveClearStageNum(int clearStageNum)
    {
        // クリアしたステージ数がセーブされているステージ数より大きければ上書き保存する
        if(saveData.clearStageNum < clearStageNum)
        {
            // クリアしたステージ数を上書き
            saveData.clearStageNum = clearStageNum;
            // 保存
            JsonDataSaver.Save<SaveData>(saveData);
            // クリアしたステージ数が変更されたイベントを呼ぶ
            EventManager.Inst.InvokeEvent(SubjectType.OnChangeClearStageNum);
        }
    }

    /// <summary>
    /// クリアしたステージ数を取得する
    /// </summary>
    /// <returns>クリアしたステージ数</returns>
    public int GetClearStageNum()
    {
        return saveData.clearStageNum;
    }

    /// <summary>
    /// プレイヤーのマテリアルデータをセーブする
    /// </summary>
    /// <param name="id">上書きするマテリアルID</param>
    public void SavePlayerMaterialID(MaterialId id)
    {
        saveData.playerMaterialId = id;
        JsonDataSaver.Save<SaveData>(saveData);
    }

    /// <summary>
    /// マテリアルIDを取得する
    /// </summary>
    /// <returns>マテリアルID</returns>
    public MaterialId GetMaterialID()
    {
        return saveData.playerMaterialId;
    }

    /// <summary>
    /// ダイヤモンドの獲得データのリストを取得する
    /// </summary>
    /// <returns>ダイヤモンドの獲得データのリスト</returns>
    public List<DiamondAcquisitionData> GetDiamondAcquisitionDataList()
    {
        return saveData.stageSaveData.diamondAcquisitionDataList;
    }
}
