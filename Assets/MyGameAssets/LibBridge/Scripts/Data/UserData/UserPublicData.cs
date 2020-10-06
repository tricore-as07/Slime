/******************************************************************************/
/*!    \brief  ユーザーの公開情報.
*******************************************************************************/

using UnityEngine;
using System.Collections;

public sealed class UserPublicData
{
    public string   Uiid        { get; set; }   // UIID.
    public string   Name        { get; set; }   // 名前.
    // 最終ログイン日.
    // レベル.
    // ひとこと.

    public UserPublicData(
            string uiid,
            string name
        )
    {
        Uiid = uiid;
        Name = name;
    }
}
