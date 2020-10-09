using VMUnityLib;
using UniRx;
using System.Collections.Generic;
using System;

/// <summary>
/// イベントの種類
/// </summary>
public enum SubjectType
{
    OnGameStart,
    OnGameClear
}

/// <summary>
/// イベントを管理するマネージャークラス
/// </summary>
public class EventManager : Singleton<EventManager>
{
    Dictionary<SubjectType, IObservable<Unit>> eventDictionary = new Dictionary<SubjectType, IObservable<Unit>>();      //イベントをSubjectTypeで呼び出せるようにするDictionary

    /// <summary>
    /// サブジェクトの追加
    /// </summary>
    /// <param name="type">イベントの種類</param>
    /// <param name="observable">追加するサブジェクト</param>
    public void AddSubject(SubjectType type, Subject<Unit> observable)
    {
        eventDictionary.Add(type,observable);
    }

    /// <summary>
    /// イベントの種類から該当するイベントを通知用のクラスを取得する
    /// </summary>
    /// <param name="type">必要な通知用クラスの種類</param>
    /// <returns>通知用クラス</returns>
    public IObservable<Unit> GetObservable(SubjectType type)
    {
        return eventDictionary[type];
    }
}
