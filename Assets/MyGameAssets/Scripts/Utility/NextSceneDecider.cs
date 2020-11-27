using UnityEngine;
using VMUnityLib;
using System;

/// <summary>
/// 次のシーンを決定する
/// </summary>
public class NextSceneDecider : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger = default;       //シーンを切り替える際に使用するクラス
    [SerializeField] SceneList sceneList = default;             //シーンのリスト

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    private void OnEnable()
    {
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
}
