using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// スキンの管理をする
/// </summary>
public class SkinManager : SingletonMonoBehaviour<SkinManager>
{
    [SerializeField] PlayerMaterialData playerMaterialData;
    Dictionary<MaterialId, Material> materialDictionary;
    MaterialId nowMaterialId;

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        nowMaterialId = SaveDataManager.Inst.GetMaterialID();
        materialDictionary = new Dictionary<MaterialId, Material>();
        foreach (var materialData in playerMaterialData.Materials)
        {
            materialDictionary.Add(materialData.id, materialData.material);
        }
        EventManager.Inst.InvokeEvent(SubjectType.OnChangeSkin);
    }

    /// <summary>
    /// IDと対応したマテリアルを取得する
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>引数として渡したIDに対応したマテリアル</returns>
    public Material GetMaterial(MaterialId id)
    {
        return materialDictionary[id];
    }

    /// <summary>
    /// 今のマテリアルを取得する
    /// </summary>
    /// <returns>今のマテリアル</returns>
    public Material GetNowMaterial()
    {
        return materialDictionary[nowMaterialId];
    }

    /// <summary>
    /// スキンを選択したときに呼ばれる
    /// </summary>
    /// <param name="id">選択されたID</param>
    public void OnSelectSkin(MaterialId id)
    {
        if(nowMaterialId != id)
        {
            nowMaterialId = id;
            SaveDataManager.Inst.SavePlayerMaterialID(nowMaterialId);
            EventManager.Inst.InvokeEvent(SubjectType.OnChangeSkin);
        }
    }
}
