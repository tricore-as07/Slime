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
    [SerializeField] List<StageSaveData> stageSaveDataList = default;       //ステージ毎のセーブデータのリスト

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        // セーブデータのファイルが存在しているなら
        if (JsonDataSaver.FileExists<SaveData>(this))
        {
            // セーブデータのファイルが存在しているならそのファイルをロードする
            JsonDataSaver.Load<SaveData>(this);
            // ロードしたファイルのステージのリストがnullなら
            if (stageSaveDataList == null)
            {
                // リストを初期化する
                stageSaveDataList = new List<StageSaveData>();
            }
        }
        // セーブデータのファイルが存在しないなら
        else
        {
            InitializationSaveData();
            // 初期化したセーブデータを保存してファイルを作成しておく
            JsonDataSaver.Save<SaveData>(this);
        }
    }

    /// <summary>
    /// セーブデータを初期化する
    /// </summary>
    void InitializationSaveData()
    {
        stageSaveDataList = new List<StageSaveData>();
    }

    /// <summary>
    /// ステージのデータをセーブする
    /// </summary>
    /// <param name="stageNum">ステージ数</param>
    /// <param name="isAcquireList">ステージにあるダイヤを取ったかどうかをリストでまとめたもの</param>
    void SaveStageData(int stageNum, IReadOnlyCollection<bool> isAcquireList)
    {
        // 既にセーブされてるステージのリストがこれからセーブするステージ数分ないなら
        // 今のステージ数に達するまで追加する
        while(stageSaveDataList.Count < stageNum)
        {
            stageSaveDataList = new List<StageSaveData>();
        }
        // 新しいリストの作成
        StageSaveData newStageSaveData = new StageSaveData();
        newStageSaveData.isAcquireItemList = new List<bool>();
        // 新しいリストにダイヤを取ったかどうかのリストをまとめて追加
        newStageSaveData.isAcquireItemList.AddRange(isAcquireList);
        // 新しく作ったリストで上書きする
        stageSaveDataList[stageNum - 1] = newStageSaveData;
    }
}

/// <summary>
/// ステージのセーブデータ
/// </summary>
[Serializable]
public class StageSaveData
{
    public List<bool> isAcquireItemList = default;          //アイテム獲得したかどうかのリスト
}