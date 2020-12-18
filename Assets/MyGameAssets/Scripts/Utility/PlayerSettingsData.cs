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
    [InfoBox("触手が伸びきるまでの時間です", InfoMessageType.None, 0)]
    [SerializeField] float extendTentacleTime = 0f;                             //触手が伸びきるまでにかかる時間
    public float ExtendTentacleTime => extendTentacleTime;                      //外部に公開するためのプロパティ
    [InfoBox("通常状態のプレイヤーの摩擦の大きさです", InfoMessageType.None, 0)]
    [SerializeField, Range(0f, 1f)] float normalPlayerFriction = 0f;            //通常状態のプレイヤーの摩擦の大きさ
    public float NormalPlayerFriction => normalPlayerFriction;                  //外部に公開するためのプロパティ
    [InfoBox("氷状態のプレイヤーの摩擦の大きさです", InfoMessageType.None, 0)]
    [SerializeField, Range(0f, 1f)] float frozenPlayerFriction = 0f;            //氷状態のプレイヤーの摩擦の大きさ
    public float FrozenPlayerFriction => frozenPlayerFriction;                  //外部に公開するためのプロパティ
    [InfoBox("タップの押された瞬間を\r\n判定する時の誤差許容範囲", InfoMessageType.None, 0)]
    [SerializeField] float tapErrorToleranceTime = 0f;                          //タップされた瞬間を判定する時の誤差の許容範囲
    public float TapErrorToleranceTime => tapErrorToleranceTime;                //外部に公開するためのプロパティ
    [InfoBox("触手の最大の太さ", InfoMessageType.None, 0)]
    [SerializeField] float tentacleMaxThickness = 1.0f;                         //タップされた瞬間を判定する時の誤差の許容範囲
    public float TentacleMaxThickness => tentacleMaxThickness;                  //外部に公開するためのプロパティ
}
