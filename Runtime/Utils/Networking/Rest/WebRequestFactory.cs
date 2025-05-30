using UnityEngine.Networking;

namespace UtilsToolbox.Utils.Networking.Rest
{
    internal static class WebRequestFactory
    {
        internal static UnityWebRequest CreateWebRequest(string url, WebRequestType type, int timeout,
            WebRequestHeader header = null, UploadHandlerRaw uploadHandler = null,
            DownloadHandlerBuffer downloadHandler = null)
        {
            UnityWebRequest request = type switch
            {
                WebRequestType.GET => UnityWebRequest.Get(url),
                WebRequestType.PUT => new UnityWebRequest(url, UnityWebRequest.kHttpVerbPUT)
                {
                    uploadHandler = uploadHandler,
                    downloadHandler = downloadHandler
                },
                WebRequestType.POST => new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
                {
                    uploadHandler = uploadHandler,
                    downloadHandler = downloadHandler
                },
                WebRequestType.DELETE => UnityWebRequest.Delete(url),
                _ => UnityWebRequest.Get(url)
            };

            request.timeout = timeout;

            if (uploadHandler != null && header != null)
            {
                request.SetRequestHeader(header.Name, header.Value);
            }

            return request;
        }
    }
}