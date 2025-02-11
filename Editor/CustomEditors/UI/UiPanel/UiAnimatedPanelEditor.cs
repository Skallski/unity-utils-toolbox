using UnityEditor;
using UnityEngine;
using UtilsToolbox.Utils.UI.UiPanel;

namespace UtilsToolbox.Editor.CustomEditors.UI.UiPanel
{
    [CustomEditor(typeof(UiPanelAnimated), true)]
    public class UiAnimatedPanelEditor : UiPanelEditor
    {
        private SerializedProperty _animatedOpen;
        private SerializedProperty _animatedClose;
        
        private bool _animationsUnfolded;
        
        protected override void SetupProperties()
        {
            base.SetupProperties();
            
            SetupProperty(ref _animatedOpen, "_animatedOpen");
            SetupProperty(ref _animatedClose, "_animatedClose");
        }

        protected override void DrawInspectorInternal()
        {
            base.DrawInspectorInternal();

            _animationsUnfolded = EditorGUILayout.BeginFoldoutHeaderGroup(_animationsUnfolded, "Animations");
            if (_animationsUnfolded)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(_animatedOpen);
                EditorGUILayout.PropertyField(_animatedClose);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), new Color(0f, 0f, 0f, 0.3f));
            EditorGUILayout.Space();
        }
    }
}