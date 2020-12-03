using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// スキンの管理をする
/// </summary>
public class SkinManager : SingletonMonoBehaviour<SkinManager>
{
    [SerializeField] PlayerSkinData playerSkinData;
    Dictionary<SkinId, GameObject> skinDictionary;
    SkinId nowSkinId;

    /// <summary>
    /// スクリプトのインスタンスがロードされたときに呼び出される
    /// </summary>
    void Awake()
    {
        nowSkinId = SaveDataManager.Inst.GetSkinID();
        skinDictionary = new Dictionary<SkinId, GameObject>();
        foreach (var skinData in playerSkinData.Skins)
        {
            skinDictionary.Add(skinData.id, skinData.skin);
        }
        EventManager.Inst.InvokeEvent(SubjectType.OnChangeSkin);
    }

    /// <summary>
    /// IDと対応したマテリアルを取得する
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>引数として渡したIDに対応したマテリアル</returns>
    public GameObject GetSkin(SkinId id)
    {
        return skinDictionary[id];
    }

    /// <summary>
    /// 今のマテリアルを取得する
    /// </summary>
    /// <returns>今のマテリアル</returns>
    public GameObject GetNowSkin()
    {
        return skinDictionary[nowSkinId];
    }

    /// <summary>
    /// スキンを選択したときに呼ばれる
    /// </summary>
    /// <param name="id">選択されたID</param>
    public void OnSelectSkin(SkinId id)
    {
        if(nowSkinId != id)
        {
            nowSkinId = id;
            SaveDataManager.Inst.SavePlayerSkinID(nowSkinId);
            EventManager.Inst.InvokeEvent(SubjectType.OnChangeSkin);
        }
    }
}
