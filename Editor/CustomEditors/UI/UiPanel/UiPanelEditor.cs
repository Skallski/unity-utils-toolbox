using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UtilsToolbox.Editor.CustomEditors.UI.UiPanel
{
    [CustomEditor(typeof(UtilsToolbox.Utils.UI.UiPanel.UiPanel), true)]
    public class UiPanelEditor : UnityEditor.Editor
    {
        private UtilsToolbox.Utils.UI.UiPanel.UiPanel _panel;

        private SerializedProperty _content;
        private SerializedProperty _background;
        private SerializedProperty _opened;
        private SerializedProperty _closed;

        private bool _eventsUnfolded;
        private Color _oldGuiBackgroundColor;
        
        private readonly HashSet<string> _propertyNamesToExclude = new HashSet<string>() { "m_Script" };

        private void OnEnable()
        {
            _panel = target as UtilsToolbox.Utils.UI.UiPanel.UiPanel;

            SetupProperties();
        }

        protected virtual void SetupProperties()
        {
            SetupProperty(ref _content, "_content");
            SetupProperty(ref _background, "_background");
            SetupProperty(ref _opened, "_opened");
            SetupProperty(ref _closed, "_closed");
        }

        protected void SetupProperty(ref SerializedProperty serializedProperty, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || _propertyNamesToExclude.Contains(propertyName))
            {
                return;
            }

            SerializedProperty foundProperty = serializedObject.FindProperty(propertyName);
            if (foundProperty != null)
            {
                serializedProperty = foundProperty;
                _propertyNamesToExclude.Add(propertyName);
            }
        }

        public override void OnInspectorGUI()
        {
            if (_panel == null)
            {
                return;
            }
            
            serializedObject.Update();
            
            DrawInspectorInternal();
            DrawInheritorsFields();

            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawInspectorInternal()
        {
            EditorGUILayout.BeginVertical();
            
            DrawBase();
            EditorGUILayout.Space();
            
            DrawEvents();
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), new Color(0f, 0f, 0f, 0.3f));
            EditorGUILayout.Space();
        }

        private void DrawBase()
        {
            _oldGuiBackgroundColor = GUI.backgroundColor;
            
            // validate content field
            if (_content.objectReferenceValue == null)
            {
                GUI.backgroundColor = Color.red;
                EditorGUILayout.PropertyField(_content);
                GUI.backgroundColor = _oldGuiBackgroundColor;
                
                EditorGUILayout.LabelField(
                    new GUIContent("Null reference!", EditorGUIUtility.IconContent("Error@2x").image),
                    new GUIStyle(EditorStyles.helpBox)
                    {
                        fixedHeight = 30,
                        fontSize = 10
                    });
                
                EditorGUILayout.Space();
            }
            else
            {
                EditorGUILayout.PropertyField(_content);
            }
            
            EditorGUILayout.PropertyField(_background); // show background field
        }

        private void DrawEvents()
        {
            _eventsUnfolded = EditorGUILayout.BeginFoldoutHeaderGroup(_eventsUnfolded, "Panel Events");
            if (_eventsUnfolded)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(_opened);
                EditorGUILayout.PropertyField(_closed);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawInheritorsFields()
        {
            // workaround to display fields of the inheritors
            SerializedProperty iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                if (_propertyNamesToExclude.Contains(iterator.name))
                {
                    continue;
                }

                EditorGUILayout.PropertyField(iterator, true);
            }
        }
    }
}