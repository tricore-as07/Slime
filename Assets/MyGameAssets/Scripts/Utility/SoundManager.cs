using UnityEngine;
using VMUnityLib;

/// <summary>
/// サウンドを管理する
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] GameObject soundOnUiObject = default;      //サウンドオンの時のUIオブジェクト
    [SerializeField] GameObject soundOffUiObject = default;     //サウンドオフの時のUIオブジェクト
    public bool isPlaySound { get; private set; }               //音を鳴らすかどうか

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        isPlaySound = SaveDataManager.Inst.GetIsPlaySound();
        soundOnUiObject.SetActive(isPlaySound);
        soundOffUiObject.SetActive(!isPlaySound);
    }

    /// <summary>
    /// 音を鳴らすかどうかを変更する
    /// </summary>
    /// <param name="isPlaySound">音を鳴らすかどうか</param>
    public void ChangeIsPlaySound(bool isPlaySound)
    {
        this.isPlaySound = isPlaySound;
    }
}
