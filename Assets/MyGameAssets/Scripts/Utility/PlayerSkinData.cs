using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// プレイヤーのスキンのデータ
/// </summary>
[CreateAssetMenu(menuName = "MyGameSettings/PlayerSkinData", fileName = "PlayerSkin")]
public class PlayerSkinData : ScriptableObject
{
    [SerializeField] List<SkinPair> skins;
    public List<SkinPair> Skins => skins;
}

/// <summary>
/// スキンのID
/// </summary>
public enum SkinId
{
    None,
    Red,
    Blue
}

/// <summary>
/// スキンのIDとオブジェクトを紐づけるためのクラス
/// </summary>
[Serializable]
public class SkinPair
{
    [SerializeField] public SkinId id;
    [SerializeField] public GameObject skin;
}