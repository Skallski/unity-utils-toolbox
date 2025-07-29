using TMPro;
using UnityEngine;

namespace UtilsToolbox.Utils.UI.Text
{
    [ExecuteAlways]
    [RequireComponent(typeof(TMP_Text))]
    public class BendText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textMesh;
        [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float _curveScale = 10f;
        
#if UNITY_EDITOR
        private void Reset()
        {
            if (_textMesh == null)
            {
                _textMesh = GetComponent<TMP_Text>();
            }
        }
#endif

        private void Awake()
        {
            if (_textMesh == null)
            {
                _textMesh = GetComponent<TMP_Text>();
            }
            
            _textMesh.ForceMeshUpdate();
        }

        private void Update()
        {
            ApplyCurve();
        }

        private void ApplyCurve()
        {
            _textMesh.ForceMeshUpdate();
            TMP_TextInfo textInfo = _textMesh.textInfo;

            if (textInfo.characterCount == 0)
            {
                return;
            }

            float firstCharX = textInfo.characterInfo[0].origin;
            float lastCharX = textInfo.characterInfo[textInfo.characterCount - 1].xAdvance;
            float textWidth = lastCharX - firstCharX;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                if (textInfo.characterInfo[i].isVisible == false)
                {
                    continue;
                }

                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;
                Vector3 offset = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) * 0.5f;

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] -= offset;
                }

                float charMidX = (textInfo.characterInfo[i].origin + textInfo.characterInfo[i].xAdvance) * 0.5f;
                float normalizedX = (charMidX - firstCharX) / textWidth;
                float curveY = _curve.Evaluate(normalizedX) * _curveScale;

                for (int j = 0; j < 4; j++)
                {
                    vertices[vertexIndex + j] += offset + new Vector3(0, curveY, 0);
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                _textMesh.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }
        }
    }
}