using UnityEngine;

/// <summary>
/// シーンの設定
/// </summary>
public class SceneSetting : MonoBehaviour
{
    [SerializeField] int stageNumber = 1;           //ステージの番号
    public int StageNumber => stageNumber;          //外部に公開するためのプロパティ

    void Start()
    {
        EventManager.Inst.Subscribe(SubjectType.OnGameClear,Unit => SaveDataManager.Inst.SaveClearStageNum(stageNumber));    
    }
}
