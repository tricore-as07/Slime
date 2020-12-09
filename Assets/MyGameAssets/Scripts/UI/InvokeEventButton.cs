using UnityEngine;

/// <summary>
/// ボタンでイベントを呼ぶためのクラス
/// </summary>
public class InvokeEventButton : MonoBehaviour
{
    [SerializeField] SubjectType invokeEventType = default;     // ボタンを押した時に呼ぶイベント

    /// <summary>
    /// 指定されたイベントを呼ぶ
    /// </summary>
    public void InvokeEvent()
    {
        EventManager.Inst.InvokeEvent(invokeEventType);
    }
}
