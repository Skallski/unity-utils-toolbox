using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    public class ReverseSpritesheetAnimationClipGenerator : EditorWindow
    {
        private Object[] sprites;
        private string savePath = "Assets";

        private void OnGUI()
        {
            GUILayout.Label("Reverse Spritesheet Animation Clip Generator", EditorStyles.boldLabel);

            if (GUILayout.Button("Use Selected Sprites"))
            {
                sprites = Selection.objects
                    .Where(o => o is Sprite)
                    .OrderByDescending(o => o.name)
                    .ToArray();
            }

            if (sprites == null || sprites.Length == 0)
            {
                EditorGUILayout.HelpBox("Select sprites and click above.", MessageType.Info);
                return;
            }

            GUILayout.Label($"Selected {sprites.Length} sprites (reversed):");
            foreach (Object s in sprites)
            {
                GUILayout.Label($" - {s.name}");
            }

            GUILayout.Space(10);
            EditorGUILayout.LabelField($"Save Path: {savePath}");

            if (GUILayout.Button("Browse..."))
            {
                string selectedPath = EditorUtility.OpenFolderPanel("Select Save Folder", "Assets", "");
                if (!string.IsNullOrEmpty(selectedPath))
                {
                    if (selectedPath.StartsWith(Application.dataPath))
                    {
                        savePath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Invalid Path",
                            "Please select a folder inside your project's Assets folder.",
                            "OK");
                    }
                }
            }

            GUI.enabled = sprites is { Length: > 0 };
            GUILayout.Space(10);
            if (GUILayout.Button("Generate Reversed AnimationClip"))
            {
                GenerateReversedClip();
            }
            GUI.enabled = true;
        }

        private void GenerateReversedClip()
        {
            if (sprites == null || sprites.Length == 0)
            {
                Debug.LogError("No sprites selected!");
                return;
            }

            // Validate and prepare save path
            if (savePath.StartsWith("Assets") == false)
            {
                EditorUtility.DisplayDialog("Invalid Save Path",
                    "Save path must be inside the project's Assets folder.",
                    "OK");
                
                return;
            }

            if (Directory.Exists(savePath) == false)
            {
                Directory.CreateDirectory(savePath);
                AssetDatabase.Refresh();
            }

            // Clip name based on first sprite
            string baseName = sprites[0].name;
            string clipName = baseName + "_reversed";
            string fullPath = Path.Combine(savePath, clipName + ".anim").Replace("\\", "/");

            // Create AnimationClip
            AnimationClip clip = new AnimationClip();
            EditorCurveBinding binding = new EditorCurveBinding
            {
                path = "",
                type = typeof(SpriteRenderer),
                propertyName = "m_Sprite"
            };

            ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[sprites.Length];
            float step = 1f / (sprites.Length - 1);
            for (int i = 0; i < sprites.Length; i++)
            {
                keyframes[i] = new ObjectReferenceKeyframe
                {
                    time = i * step,
                    value = (Sprite)sprites[i]
                };
            }

            AnimationUtility.SetObjectReferenceCurve(clip, binding, keyframes);

            // Save animation file properly
            AssetDatabase.CreateAsset(clip, fullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            EditorUtility.DisplayDialog("Animation Created",
                $"Animation clip created successfully at:\n\n{fullPath}",
                "OK");
        }
    }
}
