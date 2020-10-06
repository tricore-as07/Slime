/******************************************************************************/
/*!    \brief  ゲーム側からのみ参照される、ゲームの固有パラメータ.
*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VMUnityLib;

public static class GameInfo
{
    // 難易度
    public enum Difficulty
    {
        EASY,
        NOMAL,
        HARD
    };

    // キューシートの名前.
    public static readonly string CUESEET_NAME = "BtmanRythm";
}
