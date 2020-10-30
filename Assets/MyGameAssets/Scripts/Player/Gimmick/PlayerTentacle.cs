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
    public void ExtendTentacle(GameObject playerObject,Vector3 hookPosition)
    {
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
        // フックとプレイヤーの中間のポジションを求める
        mediumPos = (player.transform.position + hook) / 2f;
        // フックとプレイヤーの距離を求める
        dist = Vector3.Distance(player.transform.position, hook);
        // 各パラメータを決定
        transform.position = mediumPos;
        transform.localScale = new Vector3(0.1f, 0.1f, dist);
        transform.LookAt(hook);
    }
}
