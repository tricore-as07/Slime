using System.Collections.Generic;
using UnityEngine;

public class StageElementUpdater : MonoBehaviour
{
    [SerializeField] List<DiamondStatus> itemList;
    [SerializeField] StageSelectButton stageSelectButton;

    void Start()
    {
        StageElementUpdate();
    }

    public void StageElementUpdate()
    {
        DiamondAcquisitionData diamondAcquisitionData = SaveDataManager.Inst.GetDiamondAcquisitionData(stageSelectButton.StageNum);
        for (int i = 0; i < diamondAcquisitionData.isDiamondAcquisitionList.Count; i++)
        {
            itemList[i].gameObject.SetActive(true);
            itemList[i].UpdateAcquisitionStatus(diamondAcquisitionData.isDiamondAcquisitionList[i]);
        }
    }
}
