﻿using VMUnityLib;
using UniRx;
using System.Collections.Generic;
using System;

/// <summary>
/// イベントの種類
/// </summary>
public enum SubjectType
{
    OnGameStart,            // ゲームが開始された時のイベント
    OnGameClear,            // ゲームがクリアされた時のイベント
    OnGameOver,             // ゲームオーバーになった時のイベント
    OnRetry                 // リトライされた時のイベント
}

/// <summary>
/// イベントを管理するマネージャークラス
/// </summary>
public class EventManager : Singleton<EventManager>
{
    Dictionary<SubjectType, Subject<Unit>> eventDictionary = new Dictionary<SubjectType, Subject<Unit>>();                      //イベントをSubjectTypeで呼び出せるようにするDictionary

    /// <summary>
    /// 指定したイベントにSubscribeする
    /// </summary>
    /// <param name="type">Subscribeするイベントの種類</param>
    /// <param name="action">イベントが呼ばれた時に実行されるデリゲート</param>
    public void Subscribe(SubjectType type,Action<Unit> action)
    {
        // Subscribeする種類のイベントが存在していなかったら
        if (!eventDictionary.ContainsKey(type))
        {
            CreateSubject(type);
        }
        // Subscribeする
        eventDictionary[type].Subscribe(action);
    }

    /// <summary>
    /// Subjectを作成する
    /// </summary>
    /// <param name="type">作成するSubjectの種類</param>
    void CreateSubject(SubjectType type)
    {
        // Subjectの作成
        var subject = new Subject<Unit>();
        eventDictionary.Add(type, subject);
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
