using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UtilsToolbox.Utils.HttpClient.Factory;

namespace UtilsToolbox.Utils.HttpClient
{
    public static partial class HttpClient
    {
        /// <summary>
        /// Creates and sends GET request
        /// </summary>
        public static async Task<T> Get<T>(string url) 
            where T : class
        {
            UnityWebRequest getRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.GET);
            
            string response = await SendRequestAsync(getRequest);
            return string.IsNullOrEmpty(response) ? null : JsonUtility.FromJson<T>(response);
        }
        
        /// <summary>
        /// Crates and sends PUT request
        /// </summary>
        public static async Task Put<T>(string url, T data) 
            where T : class
        {
            UnityWebRequest putRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.PUT, data);
            await SendRequestAsync(putRequest);
        }

        /// <summary>
        /// Creates and sends POST request
        /// </summary>
        public static async Task Post<T>(string url, T data) 
            where T : class
        {
            UnityWebRequest postRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.POST, data);
            await SendRequestAsync(postRequest);
        }

        /// <summary>
        /// Creates and sends DELETE request
        /// </summary>
        public static async Task Delete(string url)
        {
            UnityWebRequest deleteRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.DELETE);
            await SendRequestAsync(deleteRequest);
        }

        /// <summary>
        /// Sends web request and handles its result
        /// </summary>
        private static async Task<string> SendRequestAsync(UnityWebRequest request)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            request.SendWebRequest().completed += _ => tcs.TrySetResult(true);
            await tcs.Task;

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"<color=green>{request.method} [{request.responseCode}]:</color> {request.downloadHandler.text}");
                return request.downloadHandler.text;
            }
            
            string responseText = request.downloadHandler?.text;
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"<color=green>{request.method} [{request.responseCode}]:</color> {responseText}");
            }
            else
            {
                Debug.LogError($"<color=red>{request.method} [{request.responseCode}]:</color> {request.error}");
            }

            return responseText;
        }
    }
}