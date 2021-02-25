using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 必要なコンポーネントを定義
[RequireComponent(typeof(Text))]

/// <summary>
/// デバッグ用の警告を表示するUI
/// </summary>
/// NOTE : orimoto レベルデザイン時にミスを防ぐためのものなのでレベルデザインが終わり次第削除予定です。
public class DebugWarningUI : MonoBehaviour
{
    [SerializeField] GameObject player;         // プレイヤーのオブジェクト
    [SerializeField] Text text;                 // 警告を表示する用のテキストクラス

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // フックのオブジェクトのリストを作成し、シーン内にあるものを取得する
        List<GameObject> hookObjectList = new List<GameObject>();
        hookObjectList.AddRange(GameObject.FindGameObjectsWithTag(TagName.HookPoint));
        // 必要なものがSerializeFieldに設定されていなければキャッシュする
        player = player ?? PlayerAccessor.Inst.GetPlayer();
        text = text ?? GetComponent<Text>();
        // 表示する文字列を空にしておく
        text.text = "";
        // フックのオブジェクトが１個以下なら
        if (hookObjectList.Count <= 1)
        {
            // 早期リターン
            return;
        }
        // オブジェクトが複数あるなら必要な間隔が空けられているか判定するために必要な値を求める
        var HookPointColliderRadius = hookObjectList[0].GetComponent<SphereCollider>()?.radius;                         // コライダーの半径を取得
        var HookPointTriggerRadius = hookObjectList[0].transform.localScale.y * HookPointColliderRadius;                // トリガーの半径を求める
        var mustOpenDist = (float)(HookPointTriggerRadius + HookPointTriggerRadius + player.transform.localScale.x);    // 開けないといけない間隔
        var mustOpenSqrDist = mustOpenDist * mustOpenDist;                                                              // 開けないといけない間隔の２乗
        // オブジェクトを総当たりで判定
        for (int i = 0; i < hookObjectList.Count - 1; i++)
        {
            for (int j = i + 1; j < hookObjectList.Count; j++)
            {
                var pos1 = hookObjectList[i].transform.position;
                var pos2 = hookObjectList[j].transform.position;
                // 必要な間隔が空いているかの判定
                if ((pos1 - pos2).sqrMagnitude < mustOpenSqrDist)
                {
                    // 空いていなければエラー表示
                    text.text = hookObjectList[i].name + "と" + hookObjectList[j].name + "の間隔が必要な分空けられていません";
                }
            }
        }
    }
}
