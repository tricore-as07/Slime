using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ボタンでイベントを呼ぶためのクラス
/// </summary>
public class InvokeEventButton : MonoBehaviour
{
    [SerializeField] SubjectType invokeEventType = default;             // ボタンを押した時に呼ぶイベント
    [SerializeField] List<SubjectType> invokeEventTypeList = default;   // ボタンを押した時に呼ぶイベントが複数ある時に設定する

    /// <summary>
    /// 指定されたイベントを呼ぶ
    /// </summary>
    public void InvokeEvent()
    {
        EventManager.Inst.InvokeEvent(invokeEventType);
        if(invokeEventTypeList.Count > 0)
        {
            foreach(var invokeType in invokeEventTypeList)
            {
                EventManager.Inst.InvokeEvent(invokeType);
            }
        }
    }
}
