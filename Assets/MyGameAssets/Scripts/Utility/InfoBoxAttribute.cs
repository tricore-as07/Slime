using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// インスペクターに表示する際の種類
/// </summary>
public enum InfoMessageType
{
    None,       //通常
    Info,       //情報
    Warning,    //警告
    Error       //エラー
}

/// <summary>
/// インスペクターにInfoBoxを追加するためのカスタム属性
/// </summary>
[AttributeUsage(                //作成する属性クラスの振る舞いを決める
    AttributeTargets.Field,     //属性を適用できる範囲をフィールドに設定
    Inherited = true,           //ターゲットが継承された後も、そのターゲットに設定された属性が一緒に継承されるか
    AllowMultiple = true        //ターゲットに大して複数回適用が可能かどうか
    )]
public sealed class InfoBoxAttribute : PropertyAttribute
{
    public string message;          //インスペクターに表示する文字列
    public InfoMessageType type;    //インスペクターに表示する際の情報の種類

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="showMessage">インスペクターに表示する文字列</param>
    /// <param name="infoMessageType">インスペクターに表示する際の情報の種類</param>
    /// <param name="order">表示する順番</param>
    public InfoBoxAttribute(string showMessage, InfoMessageType infoMessageType = InfoMessageType.None, int order = 0)
    {
        // コンストラクタで渡ってきた引数をメンバ変数に格納する
        message = showMessage;
        type = infoMessageType;
        this.order = order;
    }
}

#if UNITY_EDITOR
/// <summary>
/// インスペクターにInfoBoxを描画する
/// </summary>
[CustomPropertyDrawer(typeof(InfoBoxAttribute))]
public sealed class InfoBoxDrawer : DecoratorDrawer
{
    InfoBoxAttribute InfoBoxAttribute => attribute as InfoBoxAttribute;     //カスタム属性をInfoBox用のカスタムクラスにキャスト

    /// <summary>
    /// デコレータの独自のGUIを作るためにオーバーライド
    /// </summary>
    /// <param name="position">デコレータのGUIのために使用するスクリーン上の矩形</param>
    public override void OnGUI(Rect position)
    {
        // インデントサイズを取得する
        var infoBoxPosition = EditorGUI.IndentedRect(position);
        infoBoxPosition.height = GetInfoBoxHeight();
        // HelpBoxを使用してInfoBoxをレンダリングする
        EditorGUI.HelpBox(infoBoxPosition, InfoBoxAttribute.message, GetMessageType(InfoBoxAttribute.type));
    }

    /// <summary>
    /// InfoBoxの高さを取得する
    /// </summary>
    /// <returns>InfoBoxの高さ</returns>
    public override float GetHeight()
    {
        return GetInfoBoxHeight();
    }

    /// <summary>
    /// メッセージの種類を取得する
    /// </summary>
    /// <param name="type">インスペクターに表示する際の種類</param>
    /// <returns>HelpBoxで使用するenumの形式に変換して返す</returns>
    public MessageType GetMessageType(InfoMessageType type)
    {
        // HelpBoxで使用するenumの形式に変換する
        switch (type)
        {
            case InfoMessageType.None:
                {
                    return MessageType.None;
                }
            case InfoMessageType.Info:
                {
                    return MessageType.Info;
                }
            case InfoMessageType.Warning:
                {
                    return MessageType.Warning;
                }
            case InfoMessageType.Error:
                {
                    return MessageType.Error;
                }
        }
        return 0;
    }

    /// <summary>
    /// InfoBoxの高さを取得する
    /// </summary>
    /// <returns>InfoBoxの高さ</returns>
    public float GetInfoBoxHeight()
    {
        // GUI要素のスタイル情報を作成する
        var style = new GUIStyle("InfoBox");
        // GUIに何をレンダリングするかの情報を作成する
        var content = new GUIContent(InfoBoxAttribute.message);
        // InfoMessageTypeがNoneなら21,それ以外なら53をメッセージのサイズにする
        var infoMessageSize = InfoBoxAttribute.type != InfoMessageType.None ? 53 : 21;
        // contentとinfoMessageSizeを使用してレンダリングした時の高さ
        var calcHeight = style.CalcHeight(content, Screen.width - infoMessageSize);
        // レンダリングした時の高さと40で大きい方を高さとして返す（最低の高さを40にする）
        return Mathf.Max(calcHeight, 40);
    }
}
#endif