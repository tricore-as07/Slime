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
	public const string TapToStart = "TapToStart";
	public const string GoalArea = "GoalArea";
	public const string Ground = "Ground";
	public const string HookPoint = "HookPoint";
	public const string GroundTrigger = "GroundTrigger";
	public const string WindGimmick = "WindGimmick";
	public const string Diamond = "Diamond";
	public const string DiamondManager = "DiamondManager";
	
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
		tagNames.Add(TagName.TapToStart);
		tagNames.Add(TagName.GoalArea);
		tagNames.Add(TagName.Ground);
		tagNames.Add(TagName.HookPoint);
		tagNames.Add(TagName.GroundTrigger);
		tagNames.Add(TagName.WindGimmick);
		tagNames.Add(TagName.Diamond);
		tagNames.Add(TagName.DiamondManager);
		return tagNames.ToArray();
	}
}
