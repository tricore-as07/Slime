using UnityEngine;

/// <summary>
/// ダイヤモンドのオブジェクトに関する処理をする
/// </summary>
public class DiamondObject : MonoBehaviour
{
    bool isAcquisition = false;                                 //獲得したかどうか
    [SerializeField] GameObject getDiamondEffect = default;     //ダイヤを取得した時のエフェクト

    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        EventManager.Inst.Subscribe(SubjectType.OnGameStart,Unit => isAcquisition = gameObject.activeSelf, gameObject);
        EventManager.Inst.Subscribe(SubjectType.OnGameOver, Unit => gameObject.SetActive(isAcquisition), gameObject);
    }

    /// <summary>
    /// ２つのColliderが衝突したフレームに呼び出される（片方はisTriggerがtrueである時）
    /// </summary>
    /// <param name="other">この衝突に含まれるその他のCollider</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagName.Player)
        {
            gameObject.SetActive(false);
            if(getDiamondEffect != null)
            {
                var obj = Instantiate(getDiamondEffect, transform.parent);
                obj.transform.position = transform.position;
            }
        }
    }
}