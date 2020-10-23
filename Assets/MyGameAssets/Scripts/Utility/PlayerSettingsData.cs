using UnityEngine;

/// <summary>
/// プレイヤーの設定項目
/// </summary>
[CreateAssetMenu(menuName = "MyGameSettings/PlayerSettings", fileName = "PlayerSettings")]
public class PlayerSettingsData : ScriptableObject
{
    [Header("プレイヤーの設定項目")]
    [InfoBox("プレイヤーのジャンプ力\r\n※この数値は加速度なので、２倍にしたら\r\n最高到達点が２倍になるものではありません", InfoMessageType.None, 0)]
    [SerializeField] float jumpPower = 0f;                                      //ジャンプする時の力
    public float JumpPower => jumpPower;                                        //外部に公開するためのプロパティ
    [InfoBox("氷の状態でのジャンプ力を表す係数\r\n※JumpPowerにこの値をかけた数値\r\nがジャンプ力になります", InfoMessageType.None, 0)]
    [SerializeField, Range(0f, 1f)] float jumpPowerByIceConditionFactor = 0f;   //氷の状態の時のジャンプ力の係数
    public float JumpPowerByIceConditionFactor => jumpPowerByIceConditionFactor;//外部に公開するためのプロパティ
    [InfoBox("氷の状態で炎に当たった時に\r\n氷が溶けるまでの時間（無敵時間）", InfoMessageType.None, 0)]
    [SerializeField] float meltIceTime = 0f;                                    //溶ける時間
    public float MeltIceTime => meltIceTime;                                    //外部に公開するためのプロパティ
    [InfoBox("触手が伸びる最大の長さです", InfoMessageType.None, 0)]
    [SerializeField] float tentacleMaxLength = 0f;                              //触手の最大の長さ
    public float TentacleMaxLength => tentacleMaxLength;                        //外部に公開するためのプロパティ
}

