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
            // ロードしたファイルのステージのリストがnullなら
            if (saveData == null)
            {
                saveData = new SaveData();

            }
        }
        // セーブデータのファイルが存在しないなら
        else
        {
            saveData = new SaveData();
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
}
