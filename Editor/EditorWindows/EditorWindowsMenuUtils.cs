using System;
using UnityEditor;
using UnityEngine;
using UtilsToolbox.EditorWindows.TextHelperTool;

namespace UtilsToolbox.Editor.EditorWindows
{
    public static class EditorWindowsMenuUtils
    {
        private const string MENU_ROOT = "Window/Utils Toolbox/";

        [MenuItem(MENU_ROOT + "Animator Controller States Display")]
        public static void OpenAnimatorControllerStatesDisplay()
        {
            OpenWindow<AnimatorControllerStatesDisplay>();
        }
        
        [MenuItem(MENU_ROOT + "Components Finder")]
        public static void OpenComponentFinder()
        {
            OpenWindow<ComponentsFinder>();
        }
        
        [MenuItem(MENU_ROOT + "Game Object Renamer")]
        public static void OpenGameObjectRenamer()
        {
            OpenWindow<GameObjectRenamer>();
        }
        
        [MenuItem(MENU_ROOT + "Sprite Color Analyzer")]
        public static void OpenSpriteColorAnalyzer()
        {
            OpenWindow<SpriteColorAnalyzer>();
        }
        
        [MenuItem(MENU_ROOT + "Text Helper Tool")]
        public static void OpenTmpFinder()
        {
            OpenWindow<TextHelperToolWindow>();
        }

        private static void OpenWindow<TWindow>() where TWindow : EditorWindow
        {
            Type type = typeof(TWindow);
            EditorWindow window = EditorWindow.GetWindow(type);
            window.titleContent = new GUIContent(type.Name);
            window.Show();
        }
    }
}