using UnityEngine;

namespace I2.Loc
{
	public static class ScriptLocalization
	{

		public static string SkinTermsText_Diamond 		{ get{ return LocalizationManager.GetTranslation ("SkinTermsText_Diamond"); } }
		public static string SkinTermsText_Stage 		{ get{ return LocalizationManager.GetTranslation ("SkinTermsText_Stage"); } }
	}

    public static class ScriptTerms
	{

		public const string SkinTermsText_Diamond = "SkinTermsText_Diamond";
		public const string SkinTermsText_Stage = "SkinTermsText_Stage";
	}
}