/******************************************************************************/
/*!    \brief  バトル中のプール名 FIXME:ライブラリが依存している.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolName
{
    public const string Effect = "EffectPool";
    public const string Shot = "ShotPool";
    public const string Unit = "UnitPool";
    public const string Ground = "GroundPool";

    /// <summary>
    /// プール名の配列を取得
    /// </summary>
    public static string[] GetPoolNames()
    {
        List<string> poolNames = new List<string>();
        poolNames.Add(PoolName.Effect);
        poolNames.Add(PoolName.Shot);
        poolNames.Add(PoolName.Unit);
        poolNames.Add(PoolName.Ground);
        return poolNames.ToArray();
    }
}
