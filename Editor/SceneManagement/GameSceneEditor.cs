using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Botaemic.Core.Editor
{
    [CustomEditor(typeof(GameScene), true)]
    public class GameSceneEditor : ExtendedEditor
    {
		private SerializedProperty _name;
		private SerializedProperty _description;
		private SerializedProperty _sceneName;
		private SerializedProperty _sprite;
		private string[] sceneList;

		private const string noScenesWarning = "There are no scenes set for this level yet! Add a new scene with the dropdown below";
		private GUIStyle headerLabelStyle;


		private void OnEnable()
		{
			_name = serializedObject.FindProperty("_name");
			_description = serializedObject.FindProperty("_description");
			_sceneName = serializedObject.FindProperty("_sceneName");
			_sprite = serializedObject.FindProperty("_sprite");

			PopulateScenePicker();
			InitializeGuiStyles();
		}

		public override void OnInspectorGUI()
		{
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.LabelField("Scene information", headerLabelStyle);
			EditorGUILayout.Space();
			DrawProperty(_name);
			DrawScenePicker();
			DrawProperty(_description);
			DrawProperty(_sprite);
			serializedObject.ApplyModifiedProperties();
		}

		private void DrawScenePicker()
		{
			string sceneName = _sceneName.stringValue;
			EditorGUI.BeginChangeCheck();
			int selectedScene = sceneList.ToList().IndexOf(sceneName);

			if (selectedScene < 0)
			{
				EditorGUILayout.HelpBox(noScenesWarning, MessageType.Warning);
			}

			selectedScene = EditorGUILayout.Popup("Scene", selectedScene, sceneList);
			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(target, "Changed selected scene");
				_sceneName.stringValue = sceneList[selectedScene];
				MarkAllDirty();
			}
		}

		private void InitializeGuiStyles()
		{
			headerLabelStyle = new GUIStyle(EditorStyles.largeLabel)
			{
				fontStyle = FontStyle.Bold,
				fontSize = 18,
				fixedHeight = 70.0f
			};
		}

		private void PopulateScenePicker()
		{
			var sceneCount = SceneManager.sceneCountInBuildSettings;
			sceneList = new string[sceneCount];
			for (int i = 0; i < sceneCount; i++)
			{
				sceneList[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
			}
		}

		private void MarkAllDirty()
		{
			EditorUtility.SetDirty(target);
			EditorSceneManager.MarkAllScenesDirty();
		}
	}
}