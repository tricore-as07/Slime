using UnityEngine;
using UnityEngine.UI;
using I2.Loc;
using TMPro;
using UniRx;

[RequireComponent(typeof(Button))]
/// <summary>
/// スキン選択ボタン
/// </summary>
public class SkinSelectButton : MonoBehaviour
{
    [SerializeField] Button button = default;                   //自分のボタンコンポーネント
    [SerializeField] SkinId id = default;                       //ボタンが押された時に変更されるスキン
    [SerializeField] UnlockType unlockType = UnlockType.None;   //スキンを開放する条件
    [SerializeField] int unlockNum = default;                   //開放する際の条件となる数
    [SerializeField] GameObject lockImage = default;            //ロック状態の時に表示するイメージ画像
    [SerializeField] GameObject newImage = default;             //使用されていない時に表示するイメージ画像
    [SerializeField] GameObject selectImage = default;          //選択されているスキンに表示するイメージ画像
    [SerializeField] TextMeshProUGUI unlockTermsText = default; //アンロック条件を表示するテキスト
    bool isUnlock;                                              //アンロックされているかどうか

    /// <summary>
    /// 解除する条件の種類
    /// </summary>
    public enum UnlockType
    {
        None,                   //解除条件なし
        clearStageNum,          //ステージのクリア数
        diamondNum              //ダイヤモンドの取得数
    }

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        switch (unlockType)
        {
            case UnlockType.None:
                {
                    isUnlock = true;
                    break; 
                }
            case UnlockType.clearStageNum:
                {
                    UpdateIsUnlockByClearStageNum();
                    EventManager.Inst.Subscribe(SubjectType.OnChangeClearStageNum,Unit => UpdateIsUnlockByClearStageNum());
                    break;
                }
            case UnlockType.diamondNum:
                {
                    UpdateIsUnlockByDiamondNum();
                    EventManager.Inst.Subscribe(SubjectType.OnChangeDiamondNum, Unit => UpdateIsUnlockByDiamondNum());
                    break;
                }
        }
        //現在のスキンとこのUIのスキンが一致している時は選択時UIをアクティブに
        selectImage.SetActive(SkinManager.Inst.NowSkinID == id);
        EventManager.Inst.Subscribe(SubjectType.OnChangeSkin,Unit => selectImage.SetActive(SkinManager.Inst.NowSkinID == id),gameObject);
    }

    /// <summary>
    /// アンロックするかどうかをクリアしたステージ数で更新する
    /// </summary>
    void UpdateIsUnlockByClearStageNum()
    {
        var clearStageNum = SaveDataManager.Inst.GetClearStageNum();
        // アンロックされているかどうか
        isUnlock = clearStageNum >= unlockNum;
        // ロックされている時のイメージ画像のアクティブを更新
        lockImage.SetActive(!isUnlock);
        // まだスキンを使っていなかったら
        if(!SaveDataManager.Inst.GetIsUsedSkin(id))
        {
            newImage.SetActive(isUnlock);
        }
    }

    /// <summary>
    /// アンロックするかどうかをダイヤモンドの取得数で更新する
    /// </summary>
    void UpdateIsUnlockByDiamondNum()
    {
        var diamondNum = DiamondCounter.Inst.DiamondToralNum;
        // アンロックされているかどうか
        isUnlock = diamondNum >= unlockNum;
        // ロックされている時のイメージ画像のアクティブを更新
        lockImage.SetActive(!isUnlock);
        // まだスキンを使っていなかったら
        if (!SaveDataManager.Inst.GetIsUsedSkin(id))
        {
            newImage.SetActive(isUnlock);
        }
    }

    /// <summary>
    /// ボタンがクリックされた時
    /// </summary>
    public void OnClick()
    {
        // アンロックされている場合
        if (isUnlock)
        {
            SkinManager.Inst.OnSelectSkin(id);
            unlockTermsText.text = "";
            newImage.SetActive(false);
            SaveDataManager.Inst.SaveUsedSkin(id);
        }
        else
        {
            ChangeUnlockTermsText();
        }
    }

    /// <summary>
    /// アンロック条件のテキストを変更する
    /// </summary>
    void ChangeUnlockTermsText()
    {
        if(unlockType == UnlockType.clearStageNum)
        {
            unlockTermsText.text = string.Format(ScriptLocalization.SkinTermsText_Stage, unlockNum);
        }
        else if(unlockType == UnlockType.diamondNum)
        {
            unlockTermsText.text = string.Format(ScriptLocalization.SkinTermsText_Diamond, unlockNum);
        }
    }
}
