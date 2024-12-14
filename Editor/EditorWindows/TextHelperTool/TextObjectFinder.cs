using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UtilsToolbox.EditorWindows.TextHelperTool
{
    internal class TextObjectFinder : TextHelperToolWindowTab
    {
        private TMP_FontAsset _fontAsset;
        private Vector2 _scrollPos;
        
        private string _searchText;
        private List<TextMeshProUGUI> _searchResults;
        private int _selectedButtonIndex;

        private enum Mode
        {
            FindByFontAsset,
            FindByPhrase,
        }

        private Mode _mode = Mode.FindByFontAsset;

        public TextObjectFinder(string tabName) : base(tabName)
        {
            ResetInternal();
        }

        private void ResetInternal()
        {
            _fontAsset = null;
            _scrollPos = Vector2.zero;

            _searchText = string.Empty;
            _searchResults = new List<TextMeshProUGUI>();
            _selectedButtonIndex = -1;
        }

        internal override void Reset()
        {
            ResetInternal();
            _mode = Mode.FindByFontAsset;
        }

        internal override void OnGUI()
        {
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            _mode = (Mode) EditorGUILayout.EnumPopup(new GUIContent("Search Mode"), _mode);
            if (EditorGUI.EndChangeCheck())
            {
                ResetInternal();
            }
            
            EditorGUILayout.Space(10);

            switch (_mode)
            {
                default:
                case Mode.FindByFontAsset:
                {
                    HandleFindingByFontAsset();
                    break;
                }
                case Mode.FindByPhrase:
                {
                    HandleFindingByPhrase();
                    break;
                }
            }
        }

        private void HandleFindingByFontAsset()
        {
            EditorGUI.BeginChangeCheck();
            _fontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", _fontAsset,
                typeof(TMP_FontAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                _selectedButtonIndex = -1;
            }
            
            List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
            
            TextHelperToolUtils.TraverseThroughTextMeshProUGUIs(tmp =>
            {
                if (tmp.font == _fontAsset)
                {
                    texts.Add(tmp);
                }
            });

            EditorGUILayout.Space(10);
            
            EditorGUILayout.LabelField(new GUIContent(_fontAsset == null 
                    ? $"Found texts with missing font: {texts.Count}" 
                    : $"Found texts with '{_fontAsset.name}' font: {texts.Count}", 
                    EditorGUIUtility.IconContent("Info@2x").image), new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleCenter
            });

            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), new Color(0f, 0f, 0f, 0.3f));

            if (texts.Count == 0)
            {
                return;
            }

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            DisplayResultsAsButtons(texts, ref _selectedButtonIndex);
            EditorGUILayout.EndScrollView();
        }

        private void HandleFindingByPhrase()
        {
            EditorGUI.BeginChangeCheck();
            _searchText = EditorGUILayout.TextArea(_searchText, GUILayout.Height(60));
            if (EditorGUI.EndChangeCheck())
            {
                _searchResults.Clear();
                _selectedButtonIndex = -1;
        
                if (_searchText.Length < 3)
                {
                    return;
                }
            
                TextHelperToolUtils.TraverseThroughTextMeshProUGUIs(tmp =>
                {
                    if (tmp.text.Contains(_searchText))
                    {
                        _searchResults.Add(tmp);
                    }
                });
            }

            EditorGUILayout.Space(10);
            
            EditorGUILayout.LabelField(new GUIContent($"Found matching texts: {_searchResults.Count}", 
                EditorGUIUtility.IconContent("Info@2x").image), new GUIStyle(EditorStyles.label) 
            {
                alignment = TextAnchor.MiddleCenter
            });
            
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), new Color(0f, 0f, 0f, 0.3f));
            
            if (_searchResults.Count == 0)
            {
                return;
            }

            EditorGUILayout.Space();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            DisplayResultsAsButtons(_searchResults, ref _selectedButtonIndex);
            EditorGUILayout.EndScrollView();
        }

        private static void DisplayResultsAsButtons(IReadOnlyList<TextMeshProUGUI> texts, ref int selectedButtonIndex)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                TextMeshProUGUI tmp = texts[i];

                if (selectedButtonIndex == i)
                {
                    GUI.backgroundColor = Color.yellow;
                }

                GameObject tmpObject = tmp.gameObject;
                if (GUILayout.Button($"{tmpObject.name}: {tmp.text}"))
                {
                    EditorGUIUtility.PingObject(tmpObject);
                    Selection.activeGameObject = tmpObject;

                    selectedButtonIndex = i;
                }

                GUI.backgroundColor = Color.white;
            }
        }
    }
}