﻿using UnityEngine;

/// <summary>
/// プレイヤーの触手
/// </summary>
public class PlayerTentacle : MonoBehaviour
{
    GameObject player;                              //プレイヤーのゲームオブジェクト
    Vector3 hook;                                   //フックのポジション
    Vector3 mediumPos;                              //プレイヤーのポジションとフックのポジションの中間
    Vector3 dire;                                   //フックの方向
    float dist;                                     //プレイヤーとフックの距離
    float extendTime;                               //触手を伸ばすのにかかる時間
    float shrinkTime;                               //触手を縮ませるのにかかる時間
    float elapsedTime;                              //触手を伸ばすのにかかった経過時間
    bool endExtendTentacle;                         //触手を伸ばし終えたかどうか
    bool isShrinkingTentacle;                       //触手を収縮させるかどうか
    float tentacleMaxThickness;                     //触手の最大の太さ
    float tentacleMinThickness;                     //触手の最小の太さ
    [SerializeField] MeshRenderer mesh = default;   //メッシュコンポーネント

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
    /// <param name="material">プレイヤーの見た目のマテリアル</param>
    /// <param name="tentacleMaxThickness">触手の最大の太さ</param>
    public void ExtendTentacle(GameObject playerObject,Vector3 hookPosition,Material material,PlayerSettingsData playerSettingsData)
    {
        endExtendTentacle = false;
        isShrinkingTentacle = false;
        elapsedTime = 0f;
        extendTime = playerSettingsData.ExtendTentacleTime;
        shrinkTime = playerSettingsData.ShrinkTentacleTime;
        player = playerObject;
        hook = hookPosition;
        gameObject.SetActive(true);
        mesh.material = material;
        this.tentacleMaxThickness = playerSettingsData.TentacleMaxThickness;
        this.tentacleMinThickness = playerSettingsData.TentacleMinThickness;
        FixTentacle();
    }

    /// <summary>
    /// 触手を縮める
    /// </summary>
    public void ShrinkTentacle()
    {
        isShrinkingTentacle = true;
        elapsedTime = 0f;
        dire = (hook - player.transform.position).normalized;
    }

    /// <summary>
    /// 触手の調整をする
    /// </summary>
    void FixTentacle()
    {
        //触手を収縮されるかどうか
        if(!isShrinkingTentacle)
        {
            ExtendingTentacle();
        }
        else
        {
            ShrinkingTentacle();
        }
    }

    /// <summary>
    /// 触手を伸ばす
    /// </summary>
    void ExtendingTentacle()
    {
        elapsedTime += Time.deltaTime;
        //経過時間でプレイヤーからフックまでのどのくらいの割合まで触手を伸ばすかを決定する
        float extendPercentage = elapsedTime / extendTime;
        float tentacleThickness;
        dist = Vector3.Distance(player.transform.position, hook);
        //伸ばす割合が１以上なら
        if (extendPercentage >= 1)
        {
            // 触手を伸ばし終えてるフラグが立っていなかったら
            if (!endExtendTentacle)
            {
                //フラグを立てて触手を伸ばし終えたイベントを呼ぶ
                endExtendTentacle = true;
                EventManager.Inst.InvokeEvent(SubjectType.OnNotFoundHook);
            }
            // フックとプレイヤーの中間のポジションを求める
            mediumPos = (player.transform.position + hook) * 0.5f;
            tentacleThickness = tentacleMinThickness;
        }
        else
        {
            // 触手を伸ばす距離を求める
            dist *= extendPercentage;
            // 触手の長さから触手のオブジェクトの中心位置を求める
            mediumPos = player.transform.position + (hook - player.transform.position) * extendPercentage * 0.5f;
            // 触手の太さを伸ばす割合に応じて計算する
            tentacleThickness = tentacleMinThickness + ((tentacleMaxThickness - tentacleMinThickness) * extendPercentage);
        }
        // 各パラメータを決定
        transform.position = mediumPos;
        transform.localScale = new Vector3(tentacleThickness, tentacleThickness, dist);
        transform.LookAt(hook);
    }

    /// <summary>
    /// 触手を縮ませる
    /// </summary>
    void ShrinkingTentacle()
    {
        elapsedTime += Time.deltaTime;
        float shrinkPercentage = elapsedTime / shrinkTime;
        float tentacleThickness;
        float shinkingDist;
        if (shrinkPercentage >= 1)
        {
            player = null;
            hook = Vector3.zero;
            gameObject.SetActive(false);
        }
        else
        {
            shinkingDist = dist * (1 - shrinkPercentage);
            mediumPos = player.transform.position + (dire * shinkingDist * 0.5f);
            tentacleThickness = tentacleMaxThickness - ((tentacleMaxThickness - tentacleMinThickness) * shrinkPercentage);
            transform.position = mediumPos;
            transform.localScale = new Vector3(tentacleThickness, tentacleThickness, shinkingDist);
        }
    }
}
