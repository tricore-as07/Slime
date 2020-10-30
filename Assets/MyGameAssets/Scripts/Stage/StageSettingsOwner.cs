using UnityEngine;
using VMUnityLib;

/// <summary>
/// ステージの設定の所有者
/// </summary>
public class StageSettingsOwner : SingletonMonoBehaviour<StageSettingsOwner>
{
    [SerializeField] StageSettingsData stageSettingsData = default;         //ゲームで使用するステージの設定
    public StageSettingsData StageSettingsData => stageSettingsData;        //外部に公開するプロパティ
}
