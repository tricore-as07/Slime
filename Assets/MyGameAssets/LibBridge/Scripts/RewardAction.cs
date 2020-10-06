/******************************************************************************/
/*!    \brief  広告の視聴完了時処理 TODO:分離　ゲーム側処理が食い込んでる　福留作成
*******************************************************************************/
using UnityEngine;

public class RewardAction : MonoBehaviour
{
    //[SerializeField]
    //GameObject otherUiRoot;
    //[SerializeField]
    //CameraMove cameraMove;
    //[SerializeField]
    //Player     player;
    //[SerializeField]
    //Enemy      enemy;
    //[SerializeField]
    //GameObject aura;
    //[SerializeField]
    //WindowMove windowMove;
    //[SerializeField]
    //bool isAnimation = false;

    //int waitCnt;
    //bool useFlag = false;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        //// 経験値獲得
        //PlayerParameter playerParamater = new PlayerParameter();
        //playerParamater.OnWatchAdvertisement();

        //// ザリガンは画面に映らない場所へ避難
        //if(isAnimation)
        //{
        //    enemy.transform.position = new Vector3(1000.0f, 1000.0f, 100.0f);
        //}

        //otherUiRoot.SetActive(false);

        //waitCnt = 0;
        //useFlag = true;

    }

    ///// <summary>
    ///// 初期化
    ///// </summary>
    //public void Update()
    //{
    //    if (useFlag == false)
    //    {
    //        return;
    //    }
    //    if (isAnimation == false)
    //    {
    //        waitCnt++;
    //        if(waitCnt == 1)
    //        {
    //            windowMove.Initialize();
    //        }
    //        if(waitCnt == 100)
    //        {
    //            windowMove.End();
    //            useFlag = true;
    //            otherUiRoot.SetActive(true);
    //        }
    //    }
    //    else
    //    {
    //        waitCnt++;
    //        if (waitCnt == 1)
    //        {
    //            cameraMove.transform.position = new Vector3(-3.942f, 0.937f, 1.4071f);
    //            //cameraMove.transform.position = player.transform.position + new Vector3(1.0f, 3.25f, 0.0f);
    //            //cameraMove.transform.LookAt(player.transform);
    //            return;
    //        }
    //        if (waitCnt == 20)
    //        {
    //            aura.SetActive(true);
    //        }

    //        if (waitCnt == 65)
    //        {
    //            player.OnRewardChangeNextAnimation();
    //        }
    //        if (waitCnt < 100)
    //        {
    //            cameraMove.gameObject.transform.LookAt(player.transform);
    //        }
    //        if (waitCnt >= 100 && waitCnt < 300)
    //        {
    //            cameraMove.transform.position = player.transform.position + new Vector3(0.0f, 1.5f, 0.0f) + new Vector3(0.015f, -0.0075f, -0.0015f) * waitCnt;
    //            cameraMove.transform.LookAt(player.transform.position + new Vector3(0.0f, 1.5f, 0.0f));
    //        }
    //        if (waitCnt == 300)
    //        {
    //            windowMove.Initialize();
    //        }
    //        if (waitCnt == 400)
    //        {
    //            windowMove.End();
    //        }
    //        if (waitCnt == 410)
    //        {
    //            cameraMove.InitMoveData(cameraMove.transform.position + new Vector3(0, 0, 1.0f), 0.1f);
    //            useFlag = true;
    //            otherUiRoot.SetActive(true);
    //        }

    //    }
    //}

}