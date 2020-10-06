/******************************************************************************/
/*!    \brief  ボイスデータ.
*******************************************************************************/

using UnityEngine;
using VMUnityLib;

[CreateAssetMenu(menuName = "Data/VoiceData")]
public sealed class VoiceData : BaseData
{
    public string    TermName { get { return termName; } set { termName = value; } }
    public float     Time  { get { return time; } set { time = value; } }
    
    [SerializeField] string    termName;
    [SerializeField] float     time;
}
