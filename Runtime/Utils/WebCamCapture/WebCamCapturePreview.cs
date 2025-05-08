using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UtilsToolbox.Utils.WebCamCapture
{
    public class WebCamCapturePreview : MonoBehaviour
    {
        [SerializeField] private WebCamCaptureHandler _webCamCaptureHandler;
        [SerializeField] private RawImage _webCamOutputImage;

        private Texture2D _webCamTexture;

#if UNITY_EDITOR
        private void Reset()
        {
            if (_webCamCaptureHandler == null)
            {
                _webCamCaptureHandler = FindObjectOfType<WebCamCaptureHandler>(true);
            }
        }
#endif

        [UsedImplicitly]
        public void OnWebCamInitialized()
        {
            if (_webCamOutputImage != null)
            {
                SetTexture();
            }
        }

        private void Update()
        {
            if (_webCamOutputImage != null && 
                _webCamCaptureHandler.IsInitialized && 
                _webCamCaptureHandler.DidUpdateThisFrame)
            {
                SetTexture();
            }
        }

        private void SetTexture()
        {
            _webCamTexture = _webCamCaptureHandler.GetTexture();
            _webCamOutputImage.texture = _webCamTexture;
            _webCamOutputImage.rectTransform.sizeDelta = new Vector2(_webCamTexture.width, _webCamTexture.height);
        }
    }
}