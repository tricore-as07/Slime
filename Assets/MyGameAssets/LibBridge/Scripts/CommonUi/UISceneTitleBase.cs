/******************************************************************************/
/*!    \brief  シーン名を表示する.
*******************************************************************************/

using UnityEngine;
using System.Collections;

public abstract class UISceneTitleBase : MonoBehaviour
{
    /// <summary>
    /// シーンタイトルラベルの切り替え.
    /// </summary>
    /// <param name="localizeId"></param>
    abstract public void ChangeSceneTitleLabel(string localizeId);
}
