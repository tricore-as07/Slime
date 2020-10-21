using System.Collections.Generic;
using UnityEngine;
using System;
using VMUnityLib;

/// <summary>
/// セーブデータに関する処理をするクラス
/// </summary>
[Serializable]
public class SaveData : SingletonMonoBehaviour<SaveData>
{
    [SerializeField] Root root;       //ステージ毎のセーブデータのリスト

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        // セーブデータのファイルが存在しているなら
        if (JsonDataSaver.FileExists<Root>(root))
        {
            // セーブデータのファイルが存在しているならそのファイルをロードする
            root = JsonDataSaver.Load<Root>(root);
            // ロードしたファイルのステージのリストがnullなら
            if (root == null)
            {
                InitializationSaveData();
            }
        }
        // セーブデータのファイルが存在しないなら
        else
        {
            InitializationSaveData();
            // 初期化したセーブデータを保存してファイルを作成しておく
            JsonDataSaver.Save<Root>(root);
        }
    }

    /// <summary>
    /// セーブデータを初期化する
    /// </summary>
    void InitializationSaveData()
    {
        root = new Root();
        root.stageData = new StageSaveDataList();
        root.stageData.stageSaveDataList = new List<StageSaveData>();
        root.stageData.stageSaveDataList.Add(new StageSaveData());
        root.stageData.stageSaveDataList[0].isAcquireItemList = new List<bool>();
        root.stageData.stageSaveDataList[0].isAcquireItemList.Add(false);
    }

    /// <summary>
    /// ステージのデータをセーブする
    /// </summary>
    /// <param name="stageNum">ステージ数</param>
    /// <param name="isAcquireList">ステージにあるダイヤを取ったかどうかをリストでまとめたもの</param>
    public void SaveStageData(int stageNum, IReadOnlyCollection<bool> isAcquireList)
    {
        // 既にセーブされてるステージのリストがこれからセーブするステージ数分ないなら
        // 今のステージ数に達するまで追加する
        while(root.stageData.stageSaveDataList.Count < stageNum)
        {
            root.stageData.stageSaveDataList = new List<StageSaveData>();
        }
        // 新しいリストの作成
        StageSaveData newStageSaveData = new StageSaveData();
        newStageSaveData.isAcquireItemList = new List<bool>();
        // 新しいリストにダイヤを取ったかどうかのリストをまとめて追加
        newStageSaveData.isAcquireItemList.AddRange(isAcquireList);
        // 新しく作ったリストで上書きする
        root.stageData.stageSaveDataList[stageNum - 1] = newStageSaveData;
    }

    /// <summary>
    /// ステージのセーブデータを返す
    /// </summary>
    /// <param name="stageNum"></param>
    /// <returns></returns>
    public StageSaveData LoadStageData(int stageNum)
    {
        if(root.stageData.stageSaveDataList.Count >= stageNum)
        {
            StageSaveData stageSaveData = root.stageData.stageSaveDataList[stageNum - 1].Clone() as StageSaveData;
            return stageSaveData;
        }
        return new StageSaveData();
    }
}

[Serializable]
public class Root
{
    public StageSaveDataList stageData;       //ステージ毎のセーブデータのリスト
}

/// <summary>
/// ステージ毎のセーブデータをリストで管理する
/// </summary>
[Serializable]
public class StageSaveDataList
{
    public List<StageSaveData> stageSaveDataList = default;
}

/// <summary>
/// ステージのセーブデータ
/// </summary>
[Serializable]
public class StageSaveData : ICloneable
{
    public List<bool> isAcquireItemList = default;          //アイテム獲得したかどうかのリスト

    public object Clone()
    {
        return MemberwiseClone();
    }
}