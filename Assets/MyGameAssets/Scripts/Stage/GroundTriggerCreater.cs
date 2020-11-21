using UnityEngine;

/// <summary>
/// 地面のトリガーを作成する
/// </summary>
public class GroundTriggerCreater : MonoBehaviour
{
    [SerializeField] GameObject triggerObject = default;        //生成するトリガーオブジェクト

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    private void Start()
    {
        // トリガーになるオブジェクトを生成する
        var trigger = Instantiate(triggerObject,transform);
        // トリガーになるオブジェクトの横の余白をオブジェクトの逆数にすることで大きさの違うものでも左右の余白の大きさは変わらないように
        float marginReciprocalNumber = StageSettingsOwner.Inst.StageSettingsData.GroundSideMarginSpace / transform.localScale.x;
        // トリガーになるオブジェクトの大きさを左右にスペースを開けるように調整する
        trigger.transform.localScale = new Vector3(1f - marginReciprocalNumber, trigger.transform.localScale.y, trigger.transform.localScale.z);
        // トリガーになるオブジェクトのローカルポジションを自分の縦幅の半分に設定する
        trigger.transform.localPosition = new Vector3(0f, 0.5f, 0f);
    }
}
