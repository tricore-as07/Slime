using UnityEngine;
using UnityEditor;
using System.Reflection;

public class MyEditor : EditorWindow
{
    Editor playerEditor;
    Editor stageEditor;
    ScriptableObject playerSettings;
    ScriptableObject stageSettings;
    Vector2 scrollPosition = new Vector2(0, 0);

    [MenuItem("Custom/MyWindow")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<MyEditor>();
    }

    void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        GUILayout.Space(20);
        DrawSettings<Player>(ref playerSettings, ref playerEditor, TagName.Player, "playerSettingsData","プレイヤーの設定を更新する");
        GUILayout.Space(60);

        DrawSettings<StageSettingsOwner>(ref stageSettings, ref stageEditor, TagName.StageSettingsOwner, "stageSettingsData","ステージの設定を更新する");
        GUILayout.Space(200);

        EditorGUILayout.EndScrollView();
    }

    void DrawSettings<Type>(ref ScriptableObject drawObject, ref Editor editor, string tagName, string variableName, string buttonName)
        where Type : MonoBehaviour
    {
        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button(buttonName))
        {
            var gameObject = GameObject.FindGameObjectWithTag(tagName);
            var component = gameObject.GetComponent<Type>();
            var setting = component.GetType().GetField(variableName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(component);
            var settingsData = setting as ScriptableObject;
            if(setting == null)
            {
                drawObject = null;
                editor = null;
            }
            string myname = settingsData?.name;

            var guids = UnityEditor.AssetDatabase.FindAssets(myname);
            if (guids.Length == 0 | myname == null)
            {
                //throw new System.IO.FileNotFoundException("シーンに設定データが割り当てられているオブジェクトがありません");
                throw new System.IO.FileNotFoundException(string.Format("シーンに設定データが割り当てられている{0}オブジェクトがありません", tagName));
            }
            else
            {
                for (int i = 0; i < guids.Length; i++)
                {
                    var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    drawObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                    if (drawObject.name == myname)
                    {
                        break;
                    }
                }
            }
        }

        GUILayout.Space(20);

        drawObject = (ScriptableObject)EditorGUILayout.ObjectField(variableName, drawObject, typeof(ScriptableObject), false);
        if (EditorGUI.EndChangeCheck())
        {
            editor = Editor.CreateEditor(drawObject);
        }

        if (editor == null)
            return;
        editor.OnInspectorGUI();
    }
}
