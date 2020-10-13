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
    Dictionary<SubjectType, Subject<Unit>> eventDictionary = new Dictionary<SubjectType, Subject<Unit>>();                      //イベントをSubjectTypeで呼び出せるようにするDictionary
    Dictionary<SubjectType, List<Action<Unit>>> tempActionDictionary = new Dictionary<SubjectType, List<Action<Unit>>>();       //SubscribeするSubjectが無かったデリゲートを一時的に保存するDictionary

    /// <summary>
    /// 指定したイベントにSubscribeする
    /// </summary>
    /// <param name="type">Subscribeするイベントの種類</param>
    /// <param name="action">イベントが呼ばれた時に実行されるデリゲート</param>
    public void Subscribe(SubjectType type,Action<Unit> action)
    {
        // Subscribeする種類のイベントが存在していたら
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type].Subscribe(action);
        }
        // Subscribeする種類のイベントが存在していなかったら
        else
        {
            // 一時的に保存するDictionaryにSubscribeするイベントの種類のリストが存在していたら
            if (tempActionDictionary.ContainsKey(type))
            {
                tempActionDictionary[type].Add(action);
            }
            // 一時的に保存するDictionaryにSubscribeするイベントの種類のリストが存在していなかったら
            else
            {
                // 新しくリストを作成し、デリゲートを追加する
                tempActionDictionary.Add(type, new List<Action<Unit>>());
                tempActionDictionary[type].Add(action);
            }
        }
    }

    /// <summary>
    /// Subjectを作成する
    /// </summary>
    /// <param name="type">作成するSubjectの種類</param>
    public void CreateSubject(SubjectType type)
    {
        // 既に指定した種類のSubjectが存在していたら早期リターン
        if(eventDictionary.ContainsKey(type))
        {
            return;
        }
        // Subjectの作成
        var subject = new Subject<Unit>();
        eventDictionary.Add(type, subject);
        // Subscribe出来なくて一時的に保存するされているデリゲートが存在するなら
        if (tempActionDictionary.ContainsKey(type))
        {
            // Subscribe出来ていなかったデリゲートを全てSubscribeする
            foreach (var action in tempActionDictionary[type])
            {
                eventDictionary[type].Subscribe(action);
            }
            tempActionDictionary[type].Clear();
        }
    }

    /// <summary>
    /// 指定された種類のイベントを実行する
    /// </summary>
    /// <param name="type">実行するイベントの種類</param>
    public void InvokeEvent(SubjectType type)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type].OnNext(Unit.Default);
        }
    }
}
