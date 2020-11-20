using UnityEngine;

/// <summary>
/// シーンの初期化処理をする
/// </summary>
public class SceneInitializer : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        EventManager.Inst.InvokeEvent(SubjectType.OnHome);
    }
}
