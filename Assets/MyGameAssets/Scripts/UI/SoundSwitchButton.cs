using UnityEngine;

/// <summary>
/// サウンドのオンオフを切り替えるボタンの処理
/// </summary>
public class SoundSwitchButton : MonoBehaviour
{
    [SerializeField] GameObject switchingDestination;       //切り替え先

    /// <summary>
    /// 切り替えボタンを押された時の処理
    /// </summary>
    public void OnClickSwitchButton()
    {
        switchingDestination.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
