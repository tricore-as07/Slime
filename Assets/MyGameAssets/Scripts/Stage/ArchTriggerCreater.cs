using UnityEngine;

/// <summary>
/// アーチのオブジェクトのトリガーを作成するクラス
/// </summary>
/// NOTE: orimoto デバック、レベルデザイン時にメッシュの違うアーチオブジェクトを作成しても地面として動作するように作成してあります。
/// TODO: orimoto リリース前に削除予定
public class ArchTriggerCreater : MonoBehaviour
{
    /// <summary>
    /// Updateが最初に呼び出される前のフレームで呼び出される
    /// </summary>
    void Start()
    {
        // 自分のオブジェクトの複製
        GameObject duplicateObject = Instantiate(this.gameObject,transform.position,transform.rotation,transform.parent);
        // タグの変更
        duplicateObject.tag = TagName.GroundTrigger;
        // 自分のクラスを削除（無限に複製し続けてしまうため）
        var arch = duplicateObject.GetComponent<ArchTriggerCreater>();
        Destroy(arch);
        // MeshColliderを取得してトリガーオブジェクトにする
        var meshCollider = duplicateObject.GetComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
        // Rigidbodyを追加して設定の変更
        var rigid = duplicateObject.AddComponent<Rigidbody>();
        rigid.angularDrag = 0;
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
