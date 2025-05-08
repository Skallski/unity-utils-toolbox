using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace UtilsToolbox.Utils.WebCamCapture
{
    public class WebCamCaptureHandler : MonoBehaviour
    {
        [SerializeField] private string _cameraDeviceName;
        [SerializeField] private Vector2Int _imageResolution;
        [SerializeField] private int _fps;
        [SerializeField] private bool _flipY;

        [Space]
        [SerializeField] private bool _initializeOnStartup;
        [SerializeField] private UnityEvent _onInitialized;

        private WebCamTexture _webCamTexture;
        private Texture2D _texture2D;
        private Color32[] _pixelBuffer;
        
        public bool IsInitialized { get; private set; }
        
        public bool DidUpdateThisFrame => _webCamTexture is { isPlaying: true, didUpdateThisFrame: true};

#if UNITY_EDITOR
        [ContextMenu(nameof(ShowAvailableCameraDevices))]
        private void ShowAvailableCameraDevices()
        {
            string[] deviceNames = WebCamTexture.devices.Select(cam => cam.name).ToArray();
            if (deviceNames.Length > 0)
            {
                for (int i = 0, c = deviceNames.Length; i < c; i++)
                {
                    Debug.Log($"{i + 1}. {deviceNames[i]}");
                }
            }
            else
            {
                Debug.LogWarning("No WebCams devices connected!");
            }
        }
#endif
        
        private void Start()
        {
            if (_initializeOnStartup)
            {
                InitializeWebCam();
            }
        }

        private void Update()
        {
            if (IsInitialized && DidUpdateThisFrame)
            {
                _webCamTexture.GetPixels32(_pixelBuffer);

                if (_flipY)
                {
                    FlipTextureY(_pixelBuffer, _webCamTexture.width, _webCamTexture.height);
                }

                _texture2D.SetPixels32(_pixelBuffer);
                _texture2D.Apply();
            }
        }

        public void InitializeWebCam(string cameraDeviceName = null, Vector2Int? imageResolution = null, int? fps = null)
        {
            if (IsInitialized)
            {
                return;
            }

            if (cameraDeviceName != null && imageResolution.HasValue && fps.HasValue)
            {
                _cameraDeviceName = cameraDeviceName;
                _imageResolution = imageResolution.Value;
                _fps = fps.Value;
            }
            
            StartCoroutine(Initialize_Coroutine());
            IEnumerator Initialize_Coroutine()
            {
                if (string.IsNullOrEmpty(_cameraDeviceName))
                {
                    Debug.LogError($"Requested WebCam name: '{_cameraDeviceName}' is null or empty!");
                    yield break;
                }
                
                string[] devices = WebCamTexture.devices.Select(device => device.name).ToArray();
                
                if (devices.Length == 0)
                {
                    Debug.LogError("No WebCam devices connected!");
                    yield break;
                }
                
                if (devices.Contains(_cameraDeviceName) == false)
                {
                    Debug.LogError($"Requested WebCam: '{_cameraDeviceName}' not found!");
                    yield break;
                }
                
                _webCamTexture = new WebCamTexture(_cameraDeviceName, _imageResolution.x, _imageResolution.y, _fps);
                _webCamTexture.Play();

                yield return new WaitForSeconds(0.5f);
                if (_webCamTexture.isPlaying == false || _webCamTexture.width <= 16 || _webCamTexture.height <= 16)
                {
                    Debug.LogError($"WebCam '{_cameraDeviceName}' initialization failed!");
                    yield break;
                }
                
                _texture2D = new Texture2D(_webCamTexture.width, _webCamTexture.height);
                _pixelBuffer = new Color32[_webCamTexture.width * _webCamTexture.height];

                Debug.Log($"Web cam: {_cameraDeviceName} has been initialized");
                IsInitialized = true;
                _onInitialized?.Invoke();
            }
        }
        
        private static void FlipTextureY(IList<Color32> pixels, int width, int height)
        {
            int halfWidth = (int)(width * 0.5f);

            for (int y = 0; y < height; y++)
            {
                int rowStart = y * width;
                int rowEnd = rowStart + width - 1;

                for (int x = 0; x < halfWidth; x++)
                {
                    (pixels[rowStart + x], pixels[rowEnd - x]) = (pixels[rowEnd - x], pixels[rowStart + x]);
                }
            }
        }
        
        public Texture2D GetTexture() => _texture2D;

        public byte[] GetTextureAsByteArray() => _texture2D.GetRawTextureData();
    }
}