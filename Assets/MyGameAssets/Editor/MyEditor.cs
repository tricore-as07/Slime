using UnityEngine;
using UnityEditor;
using System.Reflection;

/// <summary>
/// 自分用のカスタムエディタ
/// </summary>
public class MyEditor : EditorWindow
{
    Editor playerEditor;                            //プレイヤーの設定を表示するのに使うEditorクラス
    Editor stageEditor;                             //ステージの設定を表示するのに使うEditorクラス
    ScriptableObject playerSettings;                //プレイヤーの設定用ScriptableObject
    ScriptableObject stageSettings;                 //ステージの設定用ScriptableObject
    Vector2 scrollPosition = new Vector2(0, 0);     //スクロールのポジションを保存しておくための２次元ベクトル

    /// <summary>
    /// ウィンドウを開く
    /// </summary>
    [MenuItem("Custom/MyWindow")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow<MyEditor>();
    }

    /// <summary>
    /// GUIイベント,レンダリング処理
    /// </summary>
    void OnGUI()
    {
        // 自動的にレイアウトされるスクロールビューを開始する
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        // スペースを開ける
        GUILayout.Space(20);
        // プレイヤーの設定項目をレンダリングする
        DrawSettings<Player>(ref playerSettings, ref playerEditor, TagName.Player, "playerSettingsData","プレイヤーの設定を更新する");
        // スペースを開ける
        GUILayout.Space(60);
        // ステージの設定項目をレンダリングする
        DrawSettings<StageSettingsOwner>(ref stageSettings, ref stageEditor, TagName.StageSettingsOwner, "stageSettingsData","ステージの設定を更新する");
        // スペースをあける
        GUILayout.Space(200);
        // 自動的にレイアウトされるスクロールビューを終了する
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// ScriptableObjectを使用した現在のシーンに割り当てられている設定項目を描画する
    /// </summary>
    /// <typeparam name="Type">ScriptableObjectを所持しているクラス</typeparam>
    /// <param name="drawObject">レンダリングするScriptableObjectをキャッシュする変数</param>
    /// <param name="editor">ScriptableObjectをレンダリングするために使うEditorクラス</param>
    /// <param name="tagName">ScriptableObjectを所持しているゲームオブジェクトのタグ</param>
    /// <param name="variableName">ScriptableObjectをキャッシュしている変数名</param>
    /// <param name="buttonName">ScriptableObjectを更新するボタンに表示する文字列</param>
    void DrawSettings<Type>(ref ScriptableObject drawObject, ref Editor editor, string tagName, string variableName, string buttonName)
        where Type : MonoBehaviour
    {
        // 変更がされたかのチェックを開始する
        EditorGUI.BeginChangeCheck();
        // ボタンが押されたら
        if (GUILayout.Button(buttonName))
        {
            // タグを元にゲームオブジェクトを検索する
            var gameObject = GameObject.FindGameObjectWithTag(tagName);
            // コンポーネントを取得する
            var component = gameObject.GetComponent<Type>();
            // ScriptableObjectがキャッシュされている変数を取得してくる
            var setting = component.GetType().GetField(variableName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(component);
            // 取得してきた変数をScriptableObjectにキャストする
            var settingsData = setting as ScriptableObject;
            // そもそも変数が取得できてなかったら
            if(setting == null)
            {
                // 初期化
                drawObject = null;
                editor = null;
            }
            // ScriptableObjectがnullじゃないなら名前を取得
            string myname = settingsData?.name;
            // 取得した名前でアセット一覧から検索
            var guids = UnityEditor.AssetDatabase.FindAssets(myname);
            // アセットIDが見つからなかった場合、そもそもデータが取得できてなくて文字列がnullの場合
            if (guids.Length == 0 | myname == null)
            {
                // 見つからなかった場合はエラーを投げる
                throw new System.IO.FileNotFoundException(string.Format("シーンに設定データが割り当てられている{0}オブジェクトがありません", tagName));
            }
            // アセットIDが見つかった場合
            else
            {
                // アセットIDが見つかった数だけループする
                for (int i = 0; i < guids.Length; i++)
                {
                    // アセットIDからアセットのパスを取得
                    var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    // アセットのパスからScriptableObject型でアセットを取得する
                    drawObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                    // 取得したアセットと現在のシーンに割り当てられているアセットの名前が一緒だったら検索をやめる
                    if (drawObject.name == myname)
                    {
                        break;
                    }
                }
            }
        }
        // スペースを開ける
        GUILayout.Space(20);
        // ユーザーによって設定された値を取得
        drawObject = (ScriptableObject)EditorGUILayout.ObjectField(variableName, drawObject, typeof(ScriptableObject), false);
        // 変更がされていたら
        if (EditorGUI.EndChangeCheck())
        {
            // 描画するエディタを作成
            editor = Editor.CreateEditor(drawObject);
        }
        // エディタが作成されていたら
        if (editor != null)
        {
            editor.OnInspectorGUI();
        }
    }
}
