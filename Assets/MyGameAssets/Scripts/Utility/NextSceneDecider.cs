using UnityEngine;
using VMUnityLib;

/// <summary>
/// 次のシーンを決定する
/// </summary>
public class NextSceneDecider : MonoBehaviour
{
    [SerializeField] SceneChanger sceneChanger = default;       //シーンを切り替える際に使用するクラス
    [SerializeField] SceneList sceneList = default;             //シーンのリスト

    /// <summary>
    /// オブジェクトがアクティブになった時に呼ばれる
    /// </summary>
    private void OnEnable()
    {
        // 今のシーンの名前を取得
        string nowSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // 次のシーンの要素数を取得
        int nextSceneIndex = sceneList.Scenes.IndexOf(nowSceneName) + 1;
        // 次のシーンの要素数がシーンの総数より小さければ
        if (nextSceneIndex < sceneList.Scenes.Count)
        {
            // 次のシーンに遷移させる
            sceneChanger.SceneName = sceneList.Scenes[nextSceneIndex];
        }
        // 次のシーンの要素数がシーンの総数をオーバーしているなら
        else
        {
            // 最初のシーンに遷移させる
            sceneChanger.SceneName = sceneList.Scenes[0];
        }
    }
}
