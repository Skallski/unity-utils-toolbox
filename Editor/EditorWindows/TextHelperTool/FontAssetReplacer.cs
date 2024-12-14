using TMPro;
using UnityEditor;
using UnityEngine;

namespace UtilsToolbox.EditorWindows.TextHelperTool
{
    internal class FontAssetReplacer : TextHelperToolWindowTab
    {
        private TMP_FontAsset _oldFontAsset;
        private TMP_FontAsset _newFontAsset;
        private string _statusMessage = "";
        
        public FontAssetReplacer(string tabName) : base(tabName) { }

        internal override void Reset()
        {
            _oldFontAsset = null;
            _newFontAsset = null;
            _statusMessage = string.Empty;
        }

        internal override void OnGUI()
        {
            EditorGUILayout.Space();
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            _oldFontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Old Font Asset", _oldFontAsset,
                typeof(TMP_FontAsset), false, GUILayout.Width(500f));
            
            if (_oldFontAsset != null)
            {
                EditorGUILayout.LabelField(new GUIContent($"Texts with this font: {GetTextsWithFont(_oldFontAsset)}",
                    EditorGUIUtility.IconContent("Info@2x").image));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            _newFontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font Asset", _newFontAsset,
                typeof(TMP_FontAsset), false, GUILayout.Width(500f));
            
            if (_newFontAsset != null)
            {
                EditorGUILayout.LabelField(new GUIContent($"Texts with this font: {GetTextsWithFont(_newFontAsset)}",
                    EditorGUIUtility.IconContent("Info@2x").image));
            }
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                _statusMessage = string.Empty;
            }

            if (_oldFontAsset != null && _newFontAsset != null)
            {
                EditorGUILayout.Space(10);
                
                if (GUILayout.Button("Replace Fonts"))
                {
                    ReplaceFonts();
                }
            }

            if (string.IsNullOrEmpty(_statusMessage) == false)
            {
                EditorGUILayout.HelpBox(_statusMessage, MessageType.Info);
            }
        }
        
        private static int GetTextsWithFont(TMP_FontAsset fontAsset)
        {
            int counter = 0;
            
            TextHelperToolUtils.TraverseThroughTextMeshProUGUIs(tmp =>
            {
                if (tmp.font == fontAsset)
                {
                    counter++;
                }
            });

            return counter;
        }

        private void ReplaceFonts()
        {
            int affectedObjectsCounter = 0;
            
            TextHelperToolUtils.TraverseThroughTextMeshProUGUIs(tmp =>
            {
                if (tmp.font == _oldFontAsset)
                {
                    Undo.RecordObject(tmp, "Replace Font Asset");
                    tmp.font = _newFontAsset;
                    EditorUtility.SetDirty(tmp);

                    affectedObjectsCounter++;
                }
            });

            _statusMessage = $"Font replacement completed. Affected objects: {affectedObjectsCounter}";
        }
    }
}