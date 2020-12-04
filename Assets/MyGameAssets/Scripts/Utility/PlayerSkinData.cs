using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// プレイヤーのスキンのデータ
/// </summary>
[CreateAssetMenu(menuName = "MyGameSettings/PlayerSkinData", fileName = "PlayerSkin")]
public class PlayerSkinData : ScriptableObject
{
    [SerializeField] List<SkinPair> skins = default;            //スキンのリスト
    public IReadOnlyList<SkinPair> Skins => skins;              //スキンのリストを外部に公開するためのプロパティ
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
    [SerializeField] public SkinId id = default;            //スキンのID
    [SerializeField] public GameObject skin = default;      //IDに対応したプレイヤーのスキン
}