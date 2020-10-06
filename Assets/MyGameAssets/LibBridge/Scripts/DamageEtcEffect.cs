/******************************************************************************/
/*!    \brief   ダメージに乗せるゲーム固有パラメータ.
*******************************************************************************/

using UnityEngine;
using VMUnityLib;

[System.Serializable]
public struct DamageEtcEffect
{
    public const float DamageVelocityPow = 150000.0f;
    public const float DamageTorquePow = 200.0f;

    [System.NonSerialized]
    public Vector3  staticVelocity;            // 固定ベロシティ(プログラム用).

    public Vector3  staticRotatedVelocity;     // 固定ベロシティ。回転の影響を受ける.
    public float    reflectRamge;              // 反発の範囲。0～360度。回転の影響を受ける.
    public float    reflectionCoefficient;     // 反発係数。元々その物質が持っているベロシティをどの程度コピーするか。反発方向は自分の向き（回転の影響を受ける）. 
    public float    multiForceFactor;          // 元々持っているベロシティの加速係数.
}
