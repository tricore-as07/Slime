using UnityEngine;

/// <summary>
/// ステージの設定項目
/// </summary>
[CreateAssetMenu(menuName = "MyGameSettings/StageSettings", fileName = "StageSettings")]
public class StageSettingsData : ScriptableObject
{
    [Header("ステージの設定項目")]
    [InfoBox("地面の角に当たった時にジャンプが\r\n出来てしまうのを防ぐためのものです", InfoMessageType.None, 0)]
    [InfoBox("0に設定するとゲームが強制終了します", InfoMessageType.Warning,1)]
    [SerializeField] float groundSideMarginSpace = 0f;                          //地面の左右の横の余白
    public float GroundSideMarginSpace => groundSideMarginSpace;                //外部に公開するためのプロパティ
    [InfoBox("フックに引っ掛けた時にプレイヤーを加速させる力です", InfoMessageType.None, 0)]
    [SerializeField] float motorForceOfHook = 0f;                               //モーターの加速させる力
    public float MotorForceOfHook => motorForceOfHook;                          //外部に公開するためのプロパティ
    [InfoBox("フックに引っかかっているものを\r\nどの角度まで加速させるかを表します\r\n※フックの真下方向を0度とする", InfoMessageType.None, 0)]
    [SerializeField] float motorTargetAngle = 0f;                               //モーターの加速の目標角度
    public float MotorTatgetAngle => motorTargetAngle;                          //外部に公開するためのプロパティ
    [InfoBox("加速を止める時の速度です\r\n※上記の目標角度に達した後に\r\n逆方向に加速するタイミングが変わります", InfoMessageType.None, 0)]
    [SerializeField] float deadZoneVelocity = 0f;                               //加速をやめる速度（ある程度の高さまで上がったら逆に加速するために設定する）
    public float DeadZoneVelocity => deadZoneVelocity;                          //外部に公開するためのプロパティ
    [InfoBox("風のギミックの中にプレイヤーが入った時に\r\n加速させる力です", InfoMessageType.None, 0)]
    [SerializeField] float windPower = 0f;                                      //風の強さ
    public float WindPower => windPower;                                        //外部に公開するためのプロパティ
    [InfoBox("風のギミックの中にプレイヤーがいる状態の\r\n最高速度です", InfoMessageType.None, 0)]
    [SerializeField] float limitSpeedInWindGimmick = 0f;                        //風に入っている時に最高速度
    public float LimitSpeedOnWindGimmick => limitSpeedInWindGimmick;            //外部に公開するためのプロパティ
}