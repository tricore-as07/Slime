using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

public class LevelContntManager : SingletonMonoBehaviour<LevelContntManager>
{
    [SerializeField] List<StageElementUpdater> levels = default;

    // Start is called before the first frame update
    void Start()
    {
        SwitchLevels();
        EventManager.Inst.Subscribe(SubjectType.OnHome,Unit => SwitchLevels());
        EventManager.Inst.Subscribe(SubjectType.OnRetry,Unit => SwitchLevels());
        EventManager.Inst.Subscribe(SubjectType.OnNextStage,Unit => SwitchLevels());
    }

    void SwitchLevels()
    {
        int clearStageNum = SaveDataManager.Inst.GetClearStageNum();
        for (int i = 0; i < levels.Count; i++)
        {
            if(i <= clearStageNum)
            {
                levels[i].gameObject.SetActive(true);
            }
            else
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateContent(int updateStageNum)
    {
        levels[updateStageNum - 1].StageElementUpdate();
    }
}
