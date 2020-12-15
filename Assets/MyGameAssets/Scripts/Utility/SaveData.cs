using System.Collections.Generic;
using System;

/// <summary>
/// セーブデータに関する処理をするクラス
/// </summary>
[Serializable]
public class SaveData
{
    public StageSaveData stageSaveData = default;                               //ステージのセーブデータ
    public int clearStageNum = 0;                                               //クリアしたステージ数
    public SkinId playerSkinId = SkinId.None;                                   //プレイヤーのスキンのID
    public bool isPlaySound = true;                                             //サウンドを鳴らすかどうか
    public UsedSkinData usedSkinData = default;                                 //使用したことのあるスキンのデータ

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public SaveData()
    {
        stageSaveData = new StageSaveData();
        usedSkinData = new UsedSkinData();
    }
}

/// <summary>
/// 使用したスキンのデータをリストで管理する
/// </summary>
[Serializable]
public class UsedSkinData
{
    public List<SkinId> skinIdList = default;                                   //スキンIDのリスト

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public UsedSkinData()
    {
        skinIdList = new List<SkinId>();
    }
}

/// <summary>
/// ステージ毎のセーブデータをリストで管理する
/// </summary>
[Serializable]
public class StageSaveData
{
    public List<DiamondAcquisitionData> diamondAcquisitionDataList = default;   //ステージのセーブデータのリスト

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public StageSaveData()
    {
        // リストの初期化
        diamondAcquisitionDataList = new List<DiamondAcquisitionData>();
        // 何もしないでセーブした時のためにデータを一つ追加しておく
        diamondAcquisitionDataList.Add(new DiamondAcquisitionData());
    }

    /// <summary>
    /// 同じデータを持ったクラスを作るための引数付きコンストラクタ
    /// </summary>
    /// <param name="stageSaveData">コピー元のStageSaveDataクラス</param>
    public StageSaveData(StageSaveData stageSaveData)
    {
        // 引数のインスタンスを元にステージのセーブデータのリストをコピーする
        this.diamondAcquisitionDataList = new List<DiamondAcquisitionData>(stageSaveData.diamondAcquisitionDataList);
    }
}

/// <summary>
/// ダイアモンドを獲得したかどうかのリストをラッピンングするデータクラス
/// </summary>
[Serializable]
public class DiamondAcquisitionData
{
    public List<bool> isDiamondAcquisitionList = default;                       //アイテム獲得したかどうかのリスト

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public DiamondAcquisitionData()
    {
        // リストの初期化
        isDiamondAcquisitionList = new List<bool>();
        // 何もしないでセーブした時のためにデータを一つ追加しておく
        isDiamondAcquisitionList.Add(false);
    }

    /// <summary>
    /// 同じデータを持ったクラスを作るための引数付きコンストラクタ
    /// </summary>
    /// <param name="diamondAcquisitionData">コピー元のDiamondAcquisitionDataクラス</param>
    public DiamondAcquisitionData(DiamondAcquisitionData diamondAcquisitionData)
    {
        // 引数のインスタンスを元にアイテム獲得したかどうかのリストをコピーする
        this.isDiamondAcquisitionList = new List<bool>(diamondAcquisitionData.isDiamondAcquisitionList);
    }
}