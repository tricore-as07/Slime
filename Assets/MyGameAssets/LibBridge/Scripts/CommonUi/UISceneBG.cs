/******************************************************************************/
/*!    \brief  シーン背景画像.
*******************************************************************************/

using UnityEngine;
using System.Collections;

public sealed class UISceneBG : MonoBehaviour 
{
    // シーン背景画像の種別.
    [System.Serializable]
    public enum SceneBgKind
    {
        NONE,
        HOME,
        COMMON
    }
    
    /// <summary>
    /// 背景画像を切り替える.
    /// </summary>
    public void ChangeBG(SceneBgKind kind)
    {
        // TODO:とりあえず仮で即時切替しているが、実際は滑らかに切り替える必要がある？.
        // TODO:背景一枚テクスチャ、アニメーション付きUI、TiledSpriteの三種がある。ひとつのオブジェクトのテクスチャを切り替える方式と、.
        // 複合しているオブジェクトをTweenで操作する方式が混在するのでいずれ注意が必要.
        if(kind == SceneBgKind.NONE)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
