using UnityEngine;
using TMPro;
using UniRx;

/// <summary>
/// ステージ数を表示するUI
/// </summary>
public class StageNumUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = default;        //ステージ数を表示するテキスト
    SceneSetting sceneSetting;                              //シーンのステージ数

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        EventManager.Inst.Subscribe(SubjectType.OnHome,Unit => UpdateStageNum());
    }

    /// <summary>
    /// ステージ数を更新する
    /// </summary>
    void UpdateStageNum()
    {
        sceneSetting = GameObject.FindGameObjectWithTag(TagName.SceneSettings).GetComponent<SceneSetting>();
        text.text = sceneSetting.StageNumber.ToString();
    }
}
