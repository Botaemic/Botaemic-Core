using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Botaemic.Editor
{
    public class MenuEditor : Editor
    {
        #region Private variables
        private static string _newValue = "";
        private static bool _showWindow = false;
        private static int _windowWidth = 300;
        private static int _windowHeight = 150;

        private static SerializedObject _object;
        private static SerializedProperty _type;
        #endregion

        #region Unity methodes
        private void OnEnable()
        {
            _object = new SerializedObject(target);
            _type = _object.FindProperty("_type");
        }

        public override void OnInspectorGUI()
        {

            _object.Update();
            EditorGUILayout.PropertyField(_type);
            if (!_showWindow)
            {
                if (GUILayout.Button("Create a new MenuType"))
                {
                    NewValuePopup popup = new NewValuePopup();
                    popup.maxSize = new Vector2(_windowWidth, _windowHeight);
                    popup.minSize = popup.maxSize;
                    popup.Show();
                }
            }
            _object.ApplyModifiedProperties();
        }
        #endregion

        #region Public Methodes
        public static void CreateNewMenuType(string name)
        {
            MenuType newType = ScriptableObject.CreateInstance<MenuType>();
            AssetDatabase.CreateAsset(newType, @"Assets/" + name + ".asset");
            AssetDatabase.SaveAssets();
            _type.objectReferenceValue = newType;
            _object.ApplyModifiedProperties();
        }
        #endregion

        #region Private Methodes

        #endregion

        #region Logging
        /// <summary>
        /// Logs a message to the console
        /// </summary>
        private void Log(string text)
        {
#if UNITY_EDITOR
            Debug.Log("[MenuEditor]: " + text);
#endif
        }

        /// <summary>
        /// Logs a warning to the console
        /// </summary>
        private void LogWarning(string text)
        {
#if UNITY_EDITOR
            Debug.LogWarning("[MenuEditor]: " + text);
#endif
        }

        /// <summary>
        /// Logs a error to the console
        /// </summary>
        private void LogError(string text)
        {
#if UNITY_EDITOR
            Debug.LogError("[MenuEditor]: " + text);
#endif
        }

        #endregion

        public class NewValuePopup : EditorWindow
        {
            public void OnGUI()
            {
                EditorGUILayout.LabelField("New Type", EditorStyles.wordWrappedLabel);

                _newValue = GUILayout.TextField(_newValue).Trim();

                if (!IsValidString(_newValue))
                {
                    EditorGUILayout.HelpBox("Detected invalid characters in the name!", MessageType.Warning);
                }

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Confirm"))
                {
                    if (IsValidString(_newValue))
                    {
                        CreateNewMenuType(_newValue);
                        _showWindow = false;
                        this.Close();
                    }
                }

                if (GUILayout.Button("Cancel"))
                {
                    _showWindow = false;
                    this.Close();
                }

                GUILayout.EndHorizontal();
            }

            private bool IsValidString(string msg)
            {
                return !string.IsNullOrEmpty(msg)
                       && msg.IndexOfAny(Path.GetInvalidFileNameChars()) < 0
                       && !File.Exists(msg);
            }
            private void Awake()
            {
                _showWindow = true;
                _newValue = "";
            }

            private void OnDestroy()
            {
                _showWindow = false;
                _newValue = "";
            }
        }
    }
}