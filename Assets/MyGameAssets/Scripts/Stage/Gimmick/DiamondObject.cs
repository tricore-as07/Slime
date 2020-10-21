using UnityEngine;

/// <summary>
/// ダイヤモンドのオブジェクトに関する処理をする
/// </summary>
public class DiamondObject : MonoBehaviour
{
    //[SerializeField] DiamondManager diamondManager;                 //ダイヤモンドの管理をするクラス
    //[SerializeField] int DiamondSerialNum = 0;                      //自分のダイヤモンドの識別番号

    ///// <summary>
    ///// Updateが最初に呼び出される前のフレームで呼び出される
    ///// </summary>
    //void Start()
    //{
    //    // ダイヤモンドの管理をするクラスがなければ取得してキャッシュ
    //    diamondManager = diamondManager ?? GameObject.FindGameObjectWithTag(TagName.DiamondManager).GetComponent<DiamondManager>();
    //    // ダイヤモンドの識別番号を管理クラスから取得してくる
    //    DiamondSerialNum = diamondManager.GetDiamondIdentificationNumber(gameObject);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagName.Player)
        {
            gameObject.SetActive(false);
        }
    }
}