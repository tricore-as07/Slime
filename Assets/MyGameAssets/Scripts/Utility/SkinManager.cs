using System.Collections.Generic;
using UnityEngine;
using VMUnityLib;

/// <summary>
/// スキンの管理をする
/// </summary>
public class SkinManager : SingletonMonoBehaviour<SkinManager>
{
    [SerializeField] PlayerSkinData playerSkinData = default;   //プレイヤーのスキンデータ
    Dictionary<SkinId, GameObject> skinDictionary;              //スキンデータをDictionaryに変換するためのもの
    SkinId nowSkinId;                                           //今のスキンのID

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
    /// IDと対応したスキンを取得する
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>引数として渡したIDに対応したスキン</returns>
    public GameObject GetSkin(SkinId id)
    {
        GameObject skinObject;
        if (skinDictionary.TryGetValue(id, out skinObject)) 
        {
            return skinObject;
        }
        else
        {
            Debug.LogError("IDの対応したスキンが登録されていません。/nPlayerSkinDataにIDとプレハブを登録してください。");
            return null;
        }
    }

    /// <summary>
    /// 今のスキンを取得する
    /// </summary>
    /// <returns>今のスキン</returns>
    public GameObject GetNowSkin()
    {
        GameObject skinObject;
        if (skinDictionary.TryGetValue(nowSkinId, out skinObject))
        {
            return skinObject;
        }
        else
        {
            Debug.LogError("IDの対応したスキンが登録されていません。/nPlayerSkinDataにIDとプレハブを登録してください。");
            return null;
        }
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
