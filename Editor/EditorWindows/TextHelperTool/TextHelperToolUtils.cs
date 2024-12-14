using System;
using TMPro;
using UnityEngine;

namespace UtilsToolbox.EditorWindows.TextHelperTool
{
    internal static class TextHelperToolUtils
    {
        internal static void TraverseThroughTextMeshProUGUIs(Action<TextMeshProUGUI> onEachElementAction)
        {
            TextMeshProUGUI[] textMeshProUGUIs = GameObject.FindObjectsOfType<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmp in textMeshProUGUIs)
            {
                onEachElementAction?.Invoke(tmp);
            }
        }
    }
}