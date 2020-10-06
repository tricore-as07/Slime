/******************************************************************************/
/*!    \brief  エフェクトデータ.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VMUnityLib;

[CreateAssetMenu(menuName = "Data/EffectData")]
public sealed class EffectData : BaseData
{
    // プレハブ.
    public GameObject   Prefab  { get { return prefab;      } set { prefab = value; } }

    [SerializeField]
    GameObject prefab;
}
