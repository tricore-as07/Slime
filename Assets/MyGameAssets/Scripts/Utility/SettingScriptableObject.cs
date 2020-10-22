using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Settings", fileName = "Settings")]
public class SettingScriptableObject : ScriptableObject
{
    [InfoBox("プレイヤーの設定項目", InfoMessageType.Info, 0)]
    [SerializeField] PlayerSetting playerSetting;
    [InfoBox("ステージの設定項目", InfoMessageType.Info, 0)]
    [SerializeField] StageSetting stageSetting;
}

[System.Serializable]
public class PlayerSetting
{
    [SerializeField] float jumpPower = 0f;                                      //ジャンプする時の力
    public float JumpPower => jumpPower;
    [SerializeField, Range(0f, 1f)] float jumpPowerByIceConditionFactor = 0f;   //氷の状態の時のジャンプ力の係数
    public float JumpPowerByIceConditionFactor => jumpPowerByIceConditionFactor;
    [SerializeField] float meltIceTime = default;                               //溶ける時間
    public float MeltIceTime => meltIceTime;
}

[System.Serializable]
public class StageSetting
{
    [SerializeField] float groundSideMarginSpace = 0f;
    public float GroundSideMarginSpace => groundSideMarginSpace;
    [SerializeField] float motorForceOfHook = 0f;
    public float MotorForceOfHook => motorForceOfHook;
    [SerializeField] float motorTargetAngle = 0f;
    public float MotorTatgetAngle => motorTargetAngle;
    [SerializeField] float deadZoneVelocity = 0f;
    public float DeadZoneVelocity => deadZoneVelocity;
}