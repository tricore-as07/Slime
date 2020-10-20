using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VMUnityLib;

[Serializable]
public class SaveData : SingletonMonoBehaviour<SaveData>
{
    [SerializeField]
    List<StageData> stages = default;

    void Start()
    {
        if (JsonDataSaver.FileExists<SaveData>(this))
        {
            JsonDataSaver.Load<SaveData>(this);
            if (stages == null)
            {
                stages = new List<StageData>();
            }
        }
        else
        {
            JsonDataSaver.Save<SaveData>(this);
        }
    }

    /// <summary>
    /// セーブをする
    /// </summary>
    /// <param name="stageNum">ステージ数</param>
    /// <param name="isAcquireList">ステージにあるダイヤを取ったかどうかをリストでまとめたもの</param>
    void Save(int stageNum, IReadOnlyCollection<bool> isAcquireList)
    {
        // 既にセーブされてるステージのリストがこれからセーブするステージ数分ないなら
        // 今のステージ数に達するまで追加する
        while(stages.Count < stageNum)
        {
            stages = new List<StageData>();
        }
        // 新しいリストの作成
        StageData newData = new StageData();
        newData.isAcquireList = new List<bool>();
        // 新しいリストにダイヤを取ったかどうかのリストをまとめて追加
        newData.isAcquireList.AddRange(isAcquireList);
        // 新しく作ったリストで上書きする
        stages[stageNum - 1] = newData;
    }
}

[Serializable]
public class StageData
{
    public List<bool> isAcquireList = default;
}