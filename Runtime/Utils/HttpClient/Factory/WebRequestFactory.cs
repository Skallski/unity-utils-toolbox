using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace UtilsToolbox.Utils.HttpClient.Factory
{
    public static class WebRequestFactory
    {
        public static UnityWebRequest CreateWebRequest(string url, WebRequestType requestType, 
            object data = null, WebRequestHeader? requestHeader = null, int timeout = 10)
        {
            UnityWebRequest request = new UnityWebRequest(url, requestType.ToString());

            if (data != null)
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SafelyAttachRequestHeader(requestHeader);
            request.timeout = timeout;

            return request;
        }

        private static void SafelyAttachRequestHeader(this UnityWebRequest request, WebRequestHeader? requestHeader)
        {
            WebRequestHeader header = requestHeader ?? new WebRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader(header.Name, header.Value);
        }
    }
}