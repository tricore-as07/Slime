using UnityEngine;

/// <summary>
/// プレイヤーの触手
/// </summary>
public class PlayerTentacle : MonoBehaviour
{
    GameObject player;          //プレイヤーのゲームオブジェクト
    Vector3 hook;               //フックのポジション
    Vector3 mediumPos;          //プレイヤーのポジションとフックのポジションの中間
    float dist;                 //プレイヤーとフックの距離
    float extendTime;           //触手を伸ばすのにかかる時間
    float extendElapsedTime;    //触手を伸ばすのにかかった経過時間

    /// <summary>
    /// 毎フレーム呼び出される
    /// </summary>
    void Update()
    {
        if(player != null)
        {
            FixTentacle();
        }
    }

    /// <summary>
    /// 触手を伸ばす処理
    /// </summary>
    /// <param name="playerObject">プレイヤーのゲームオブジェクト</param>
    /// <param name="hookPosition">フックのポジション</param>
    /// <param name="argExtendTime">フックを伸ばすのにかかる時間</param>
    public void ExtendTentacle(GameObject playerObject,Vector3 hookPosition,float argExtendTime)
    {
        extendElapsedTime = 0f;
        extendTime = argExtendTime;
        player = playerObject;
        hook = hookPosition;
        gameObject.SetActive(true);
        FixTentacle();
    }

    /// <summary>
    /// 触手を縮める
    /// </summary>
    public void ShrinkTentacle()
    {
        player = null;
        hook = Vector3.zero;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 触手の調整をする
    /// </summary>
    void FixTentacle()
    {
        extendElapsedTime += Time.deltaTime;
        //経過時間でプレイヤーからフックまでのどのくらいの割合まで触手を伸ばすかを決定する
        float extendPercentage = extendElapsedTime / extendTime;
        dist = Vector3.Distance(player.transform.position, hook);
        //伸ばす割合が１以上なら
        if (extendPercentage >= 1)
        {
            // フックとプレイヤーの中間のポジションを求める
            mediumPos = (player.transform.position + hook) / 2f;
        }
        else
        {
            // 触手を伸ばす距離を求める
            dist *= extendPercentage;
            // 触手の長さから触手のオブジェクトの中心位置を求める
            mediumPos = player.transform.position + (hook - player.transform.position) * extendPercentage / 2;
        }
        // 各パラメータを決定
        transform.position = mediumPos;
        transform.localScale = new Vector3(0.1f, 0.1f, dist);
        transform.LookAt(hook);
    }
}
