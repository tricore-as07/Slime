using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの設定項目
/// </summary>
[CreateAssetMenu(menuName = "MyGameSettings/SceneList", fileName = "SceneList")]
public class SceneList : ScriptableObject
{
    [SceneNameAttribute, SerializeField] List<string> scenes = default;
    public List<string> Scenes => scenes;
}
