using System.Collections.Generic;
/// <summary>
/// タグ名を定数で管理するクラス
/// </summary>
public static class TagName
{
    public const string Untagged = "Untagged";
    public const string Respawn = "Respawn";
    public const string Finish = "Finish";
    public const string EditorOnly = "EditorOnly";
    public const string MainCamera = "MainCamera";
    public const string Player = "Player";
    public const string GameController = "GameController";
    
    /// <summary>
    /// タグ名の配列を取得
    /// </summary>
    public static string[] GetTagNames()
    {
        List<string> tagNames = new List<string>();
        tagNames.Add(TagName.Untagged);
        tagNames.Add(TagName.Respawn);
        tagNames.Add(TagName.Finish);
        tagNames.Add(TagName.EditorOnly);
        tagNames.Add(TagName.MainCamera);
        tagNames.Add(TagName.Player);
        tagNames.Add(TagName.GameController);
        return tagNames.ToArray();
    }
}
