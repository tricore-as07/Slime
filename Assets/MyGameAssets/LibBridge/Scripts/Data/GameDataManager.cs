/******************************************************************************/
/*!    \brief  ゲームパラメータのマネージャ.
*******************************************************************************/

using UnityEngine;
using VMUnityLib;
using System.Collections;
using System.Collections.Generic;

public class GameDataManager : Singleton<GameDataManager>
{
    public UserData          UserData         { get; set; }
    public IdentifiedDataManager<EffectData> EffectDataManager { get; set; }
    public IdentifiedDataManager<VoiceData> VoiceDataManager { get; set; }

    public GameDataManager()
    {

    }

    /// <summary>
    /// 不変データを読み込む.
    /// </summary>
    public void LoadStaticData()
    {
        EffectDataManager = new IdentifiedDataManager<EffectData>("Data/EffectData");
        VoiceDataManager = new IdentifiedDataManager<VoiceData>("Data/VoiceData");
        EffectDataManager.LoadData();
        VoiceDataManager.LoadData();
    }

    /// <summary>
    /// デフォルトデータを読み込む.
    /// </summary>
    public void LoadDefaultData()
    {
        /*
        // TODO:通信実装.
        UserData = GetDummyData();

        // TODO:正しいものに.
        List<string> skillSlotIdList = new List<string>();
        UnitData friendUnit = new UnitData(GetKituneArcheTypeDummy(5), 0, 12, 0, UnitKind.PLAYER, skillSlotIdList);

        UserPublicData friendPublicData = new UserPublicData(
            "friend",
            "ミスターフレンド",
            friendUnit,
            100
            );
        BattleInitializer.Inst.SetSortieInfo(UserData.GetPartyData(0), friendPublicData);
        */
    }

    /// <summary>
    /// ダミーデータ作成.
    /// </summary>
    /// <returns></returns>
    UserData GetDummyData()
    {
        UserData ret = new UserData(new UserPublicData("test", "test user"));
        return ret;
    }

}
