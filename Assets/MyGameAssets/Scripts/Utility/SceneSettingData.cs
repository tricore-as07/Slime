using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの設定項目
/// </summary>
[CreateAssetMenu(menuName = "MyGameSettings/SceneList", fileName = "SceneList")]
public class SceneSettingData : ScriptableObject
{
    [SceneNameAttribute, SerializeField] List<string> scenes = default;
    public List<string> Scenes => scenes;
    [SerializeField] Color selectColor = Color.white;
    public Color SelectColor => selectColor;
    [SerializeField] Color nonSelectColor = Color.white;
    public Color NonSelectColor => nonSelectColor;
    [SerializeField] Color lockButtonColor = Color.white;
    public Color LockButtonColor => lockButtonColor;
    [SerializeField] Color lockTextColor = Color.white;
    public Color LockTextColor => lockTextColor;
}
