using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "MyGameSettings/PlayerMaterialData", fileName = "PlayerMaterial")]
public class PlayerMaterialData : ScriptableObject
{
    [SerializeField] List<MaterialPair> materials;
    public List<MaterialPair> Materials => materials;

}

public enum MaterialId
{
    None,
    Red,
    Blue
}

[Serializable]
public class MaterialPair
{
    [SerializeField] public MaterialId id;
    [SerializeField] public Material material;
}