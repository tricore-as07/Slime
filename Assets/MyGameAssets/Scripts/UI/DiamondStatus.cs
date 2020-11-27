using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ダイヤモンドの取得状況を表す
/// </summary>
public class DiamondStatus : MonoBehaviour
{
    [SerializeField] Image image;           //取得状況を表すイメージ
    [SerializeField] Color getColor;        //取得している時の色
    [SerializeField] Color notGetColor;     //取得していない時の色

    /// <summary>
    /// 取得状況を更新する
    /// </summary>
    /// <param name="status">取得状況</param>
    public void UpdateAcquisitionStatus(bool status)
    {
        // 取得しているなら
        if(status)
        {
            image.color = getColor;
        }
        // 取得していないなら
        else
        {
            image.color = notGetColor;
        }
    }
}
