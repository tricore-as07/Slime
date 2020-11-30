using UnityEngine;
using VMUnityLib;

/// <summary>
/// ステージを選択するボタンのスクリプト
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    [SerializeField] int stageNum = default;                    //ステージ数
    public int StageNum => stageNum;                            //外部に公開するためのプロパティ
    [SerializeField] SceneList sceneList = default;             //シーンのリスト
    [SerializeField] SceneChanger sceneChanger = default;       //シーンを切り替える際に使用するクラス

    　/// <summary>
     /// ボタンをクリックされた時の処理
     /// </summary>
    public void OnClick()
    {
        // 押されたボタンのシーンの名前を取得
        string nextSceneName = sceneList.Scenes[stageNum - 1];
        // 今のシーンの名前を取得
        string nowSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // 今のシーンと遷移する予定のシーンが同じじゃなければ
        if(nowSceneName != nextSceneName)
        {
            // 遷移するシーンを押されたボタンのシーンの名前に変更
            sceneChanger.SceneName = nextSceneName;
            // シーン遷移処理
            sceneChanger.ChangeScene();
            // レベル選択のUIを閉じるイベントを呼ぶ
            EventManager.Inst.InvokeEvent(SubjectType.OnCloseLevels);
        }
    }
}
