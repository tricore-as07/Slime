using UnityEngine;

/// <summary>
/// アーチのオブジェクトのトリガーを作成するクラス
/// </summary>
public class ArchTriggerCreater : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // 自分のオブジェクトの複製
        GameObject gameObject = Instantiate(this.gameObject,transform.position,transform.rotation,transform.parent);
        // タグの変更
        gameObject.tag = TagName.GroundTrigger;
        // 自分のクラスを削除（無限に複製し続けてしまうため）
        var arch = gameObject.GetComponent<ArchTriggerCreater>();
        Destroy(arch);
        // MeshColliderを取得してトリガーオブジェクトにする
        var meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
        // Rigidbodyを追加して設定の変更
        var rigid = gameObject.AddComponent<Rigidbody>();
        rigid.angularDrag = 0;
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
