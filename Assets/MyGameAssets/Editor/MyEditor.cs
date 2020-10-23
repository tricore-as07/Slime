using UnityEngine;
using UnityEditor;
using System.Reflection;

public class MyEditor : EditorWindow
{
    private Editor editor;
    private ScriptableObject playerSettings;
    private ScriptableObject stageSettings;

    [MenuItem("Custom/MyWindow")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<MyEditor>();
    }

    void OnGUI()
    {
        DrawPlayerSettings();
    }

    void DrawPlayerSettings()
    {
        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("リロード"))
        {
            var playerObj = GameObject.FindGameObjectWithTag(TagName.Player);
            var player = playerObj.GetComponent<Player>();
            var setting = player.GetType().GetField("playerSettingsData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player);
            var playerSettingsData = setting as ScriptableObject;
            string myname = playerSettingsData.name;

            var guids = UnityEditor.AssetDatabase.FindAssets(myname);
            if (guids.Length == 0)
            {
                throw new System.IO.FileNotFoundException("MyScriptableObject does not found");
            }

            for (int i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                playerSettings = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (playerSettings.name == myname)
                {
                    break;
                }
            }
        }
        playerSettings = (ScriptableObject)EditorGUILayout.ObjectField("PlayerSettingsData", playerSettings, typeof(ScriptableObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            editor = Editor.CreateEditor(playerSettings);
        }

        if (editor == null)
            return;
        editor.OnInspectorGUI();
    }
}
