using VMUnityLib;
using UniRx;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// イベントの種類
/// </summary>
public enum SubjectType
{
    OnHome,                 // ホームの戻る時のイベント
    OnGameStart,            // ゲームが開始された時のイベント
    OnGameClear,            // ゲームがクリアされた時のイベント
    OnGameOver,             // ゲームオーバーになった時のイベント
    OnRetry,                // リトライされた時のイベント
    OnNextStage,            // 次のステージに進む時のイベント
    OnOpenConfig,           // 設定が開かれた時のイベント
    OnCloseConfig,          // 設定が閉じられた時のイベント
    OnFoundHook,            // フックが見つかった時のイベント
    OnNotFoundHook,         // フックが見つからなくなった時もイベント
    OnOpenLevels,           // ステージ選択UIが開かれた時のイベント
    OnCloseLevels,          // ステージ選択UIが閉じられた時のイベント
    StartFadeOut,           // フェードアウトが始まった時のイベント
    EndFadeIn,              // フェードインが終わった時のイベント
    OnOpenSkins,            // スキンん選択UIが開かれた時のイベント
    OnCloseSkins,           // スキン選択UIが閉じられた時のイベント
    OnChangeSkin,           // スキンが変更された時のイベント
    OnChangeDiamondNum,     // 取得しているダイヤモンドの数が更新された時のイベント
    OnChangeClearStageNum,  // クリアしたステージ数が更新された時のイベント
    OnOpenRecommend,        // おすすめゲームを開いた時のイベント
    OnCloseRecommend,       // おすすめゲームを閉じた時のイベント
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
    /// <returns>イベントの解除に必要なIDisposable</returns>
    public IDisposable Subscribe(SubjectType type,Action<Unit> action,GameObject gameObject = null)
    {
        // Subscribeする種類のイベントが存在していなかったら
        if (!eventDictionary.ContainsKey(type))
        {
            CreateSubject(type);
        }
        // Subscribeする
        if(gameObject == null)
        {
            return eventDictionary[type].Subscribe(action);
        }
        else
        {
            return eventDictionary[type].Subscribe(action).AddTo(gameObject);
        }
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
