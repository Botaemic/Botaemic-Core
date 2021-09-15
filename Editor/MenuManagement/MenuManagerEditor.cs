using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Botaemic.Editor
{
    [CustomEditor(typeof(MenuController))]
    public class MenuManagerEditor : Editor
    {
        #region Private variables
        private static GUIContent _deleteButtonContent = new GUIContent("-", "delete");
        private static GUIContent _addButtonContent = new GUIContent("+", "add element");
        private static GUILayoutOption _miniButtonWidth = GUILayout.Width(20f);
        #endregion

        #region Unity methodes
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ShowProperty(serializedObject.FindProperty("_openingMenu"));
            ShowProperty(serializedObject.FindProperty("_menuOpenCloseEvent"));
            
            //Show(serializedObject.FindProperty("_menus"));
            //DropAreaGui(serializedObject.FindProperty("_menus"));

            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        #region Private Methodes
        private static void ShowBoolean(SerializedProperty toggle)
        {
            toggle.boolValue = EditorGUILayout.Toggle(toggle.displayName+":", toggle.boolValue);
        }

        protected void ShowProperty(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);
        }

        private void DropAreaGui(SerializedProperty list)
        {
            var evt = Event.current;

            var dropArea = GUILayoutUtility.GetRect(0.0f, 100.0f, GUILayout.ExpandWidth(true));

            GUI.Box(dropArea, "Drag and Drop to add 1 or more menu's");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!dropArea.Contains(evt.mousePosition)) { break; }

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                    if (evt.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();
                        foreach (var item in DragAndDrop.objectReferences)
                        {
                            var go = item as GameObject;
                            if (!go) { continue; }
                            var menu = go.GetComponent<Menu>();
                            if (!menu) { continue; }

                            AddMenu(list, menu);
                        }
                    }
                    Event.current.Use();
                    break;
            }
        }

        private static void AddMenu(SerializedProperty list, Menu menu)
        {
            list.InsertArrayElementAtIndex(list.arraySize);
            list.GetArrayElementAtIndex(list.arraySize - 1).objectReferenceValue = menu;
        }

        private static void Show(SerializedProperty list)
        {
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            SerializedProperty size = list.FindPropertyRelative("Array.size");
            EditorGUILayout.PropertyField(size);
            if (size.hasMultipleDifferentValues)
            {
                EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
            }
            else
            {
                ShowMenus(list);
            }

            EditorGUI.indentLevel -= 1;
        }

        private static void ShowMenus(SerializedProperty list)
        {
            if (list.arraySize == 0 && GUILayout.Button(_addButtonContent, EditorStyles.miniButton))
            {
                list.arraySize += 1;
            }

            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
                ShowButtons(list, i);
                EditorGUILayout.EndHorizontal();
            }
        }

        private static void ShowButtons(SerializedProperty list, int index)
        {
            if (GUILayout.Button(_addButtonContent, EditorStyles.miniButtonMid, _miniButtonWidth))
            {
                list.InsertArrayElementAtIndex(index);
            }

            if (GUILayout.Button(_deleteButtonContent, EditorStyles.miniButtonRight, _miniButtonWidth))
            {
                int oldSize = list.arraySize;
                list.DeleteArrayElementAtIndex(index);
                if (list.arraySize == oldSize)
                {
                    list.DeleteArrayElementAtIndex(index);
                }
            }
        }
        #endregion

    }
}