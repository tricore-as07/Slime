using UnityEngine;
using VMUnityLib;
using System;

public class NextSceneDecider : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger = default;       //シーンを切り替える際に使用するクラス
    [SerializeField] SceneList sceneList = default;             //シーンのリスト
    IDisposable disposable;

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    private void OnEnable()
    {
        disposable = EventManager.Inst.Subscribe(SubjectType.OnGameClear, Unit => DecideNextScene());
        string nowSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int nextSceneIndex = sceneList.Scenes.IndexOf(nowSceneName) + 1;
        if (nextSceneIndex < sceneList.Scenes.Count)
        {
            sceneChanger.SceneName = sceneList.Scenes[nextSceneIndex];
        }
        else
        {
            sceneChanger.SceneName = sceneList.Scenes[0];
        }
    }

    void DecideNextScene()
    {

    }

    private void OnDisable()
    {
        disposable.Dispose();
    }
}
