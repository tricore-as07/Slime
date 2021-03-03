﻿using UnityEngine;
using VMUnityLib;
using UnityEngine.UI;

/// <summary>
/// ステージを選択するボタンのスクリプト
/// </summary>
public class StageSelectButton : MonoBehaviour
{
    int stageNum = default;                                     //ステージ数
    public int StageNum => stageNum;                            //外部に公開するためのプロパティ
    [SerializeField] SceneSettingData sceneList = default;      //シーンのリスト
    [SerializeField] SceneChanger sceneChanger = default;       //シーンを切り替える際に使用するクラス
    [SerializeField] Image image = default;                     //ボタンのイメージ

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        stageNum = transform.GetSiblingIndex() + 1;
    }

    /// <summary>
    /// オブジェクトがアクティブになった時に呼ばれる
    /// </summary>
    private void OnEnable()
    {
        // 押されたボタンのシーンの名前を取得
        string nextSceneName = sceneList.Scenes[stageNum - 1];
        // 今のシーンの名前を取得
        string nowSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        // 今のシーンと遷移する予定のシーンが同じなら
        if (nowSceneName == nextSceneName)
        {
            image.color = sceneList.SelectColor;
        }
        else
        {
            image.color = sceneList.NonSelectColor;
        }
    }

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
