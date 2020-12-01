using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキン選択ボタン
/// </summary>
public class SkinSelectButton : MonoBehaviour
{
    [SerializeField] MaterialId id;         //ボタンが押された時に変更されるマテリアル

    /// <summary>
    /// ボタンがクリックされた時
    /// </summary>
    public void OnClick()
    {
        SkinManager.Inst.OnSelectSkin(id);
    }
}
