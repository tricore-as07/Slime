using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField] int stageNum = default;
    public int StageNum => stageNum;
    [SerializeField] SceneList sceneList = default;             //シーンのリスト
    [SerializeField] SceneChanger sceneChanger = default;       //シーンを切り替える際に使用するクラス

    public void OnClick()
    {
        string nextSceneName = sceneList.Scenes[stageNum - 1];
        string nowSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if(nowSceneName != nextSceneName)
        {
            sceneChanger.SceneName = nextSceneName;
            sceneChanger.ChangeScene();
            EventManager.Inst.InvokeEvent(SubjectType.OnCloseLevels);
        }
    }
}
