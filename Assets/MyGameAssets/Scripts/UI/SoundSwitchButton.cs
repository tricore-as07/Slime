using UnityEngine;

/// <summary>
/// サウンドのオンオフを切り替えるボタンの処理
/// </summary>
public class SoundSwitchButton : MonoBehaviour
{
    [SerializeField] GameObject switchingDestination = default;       //切り替え先
    [SerializeField] bool isPlaySoundDestination = default;           //ボタンを押した時にサウンドを鳴らすようにするかどうか

    /// <summary>
    /// 切り替えボタンを押された時の処理
    /// </summary>
    public void OnClickSwitchButton()
    {
        // 切り替え先をアクティブにする
        switchingDestination.SetActive(true);
        // 自分を非アクティブにする
        this.gameObject.SetActive(false);
        // サウンドを鳴らすかどうかをセーブする
        SaveDataManager.Inst.SaveIsPlaySound(isPlaySoundDestination);
        // サウンドを鳴らすかどうかの変更を反映
        SoundManager.Inst.ChangeIsPlaySound(isPlaySoundDestination);
    }
}
