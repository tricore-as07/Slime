using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

/// <summary>
/// サウンドの再生をするクラス
/// </summary>
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] List<EventAndAudioPair> audioList = default;

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        //各オーディオとイベントを設定していく
        foreach(var audio in audioList)
        {
            EventManager.Inst.Subscribe(audio.eventType, Unit => audio.audioSource.Play(),gameObject);
        }
    }
}

/// <summary>
/// イベントとオーディオを紐づけるためのクラス
/// </summary>
[Serializable]
public class EventAndAudioPair
{
    public SubjectType eventType = default;         //再生するタイミングのイベントの種類
    public AudioSource audioSource = default;       //再生するオーディオ
}
