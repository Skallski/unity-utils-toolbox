using UnityEditor;
using UnityEngine;

namespace UtilsToolbox.EditorWindows.TextHelperTool
{
    internal class TextHelperToolWindow : EditorWindow
    {
        private TextHelperToolWindowTab[] _fontToolTabs;

        private int _currentTabIndex = 0;
        private string[] _tabNames;

        // [MenuItem("Tools/Font Tool")]
        // public static void ShowWindow()
        // {
        //     TextHelperToolWindow fontToolWindow = GetWindow<TextHelperToolWindow>("Font Tool");
        //     
        // }

        private void OnEnable()
        {
            minSize = new Vector2(460, 460);
            
            Setup();
        }

        private void Setup()
        {
            _fontToolTabs = new TextHelperToolWindowTab[]
            {
                new FontAssetReplacer("Font Asset Replacer"),
                new TextObjectFinder("TextMeshPro Finder")
            };

            _tabNames = new string[_fontToolTabs.Length];
            for (int i = 0; i < _tabNames.Length; i++)
            {
                _tabNames[i] = _fontToolTabs[i].TabName;
            }

            _currentTabIndex = 0;
        }

        private void OnGUI()
        {
            EditorGUI.BeginChangeCheck();
            _currentTabIndex = GUILayout.Toolbar(_currentTabIndex, _tabNames);
            if (EditorGUI.EndChangeCheck())
            {
                _fontToolTabs[_currentTabIndex].Reset();
            }
            
            _fontToolTabs[_currentTabIndex].OnGUI();
        }
    }
}