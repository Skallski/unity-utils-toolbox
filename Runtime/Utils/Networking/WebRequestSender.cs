using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace UtilsToolbox.Utils.Networking
{
    public static class WebRequestSender
    {
        /// <summary>
        /// Creates and sends GET request
        /// </summary>
        /// <param name="caller">  </param>
        /// <param name="url"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public static void Get(MonoBehaviour caller, string url, Action<string> onSuccess, Action onError = null)
        {
            UnityWebRequest request = WebRequestFactory.CreateWebRequest(url, WebRequestType.GET, 10);
            caller.StartCoroutine(SendWebRequest_Coroutine(request, onSuccess, onError));
        }

        /// <summary>
        /// Crates and sends PUT request
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="requestBodyRaw"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public static void Put(MonoBehaviour caller, string url, WebRequestHeader header, byte[] requestBodyRaw, 
            Action onSuccess = null, Action onError = null)
        {
            UnityWebRequest request = WebRequestFactory.CreateWebRequest(url, WebRequestType.PUT, 10, header,
                new UploadHandlerRaw(requestBodyRaw), new DownloadHandlerBuffer());

            caller.StartCoroutine(SendWebRequest_Coroutine(request, _ => onSuccess?.Invoke(), onError));
        }
        
        /// <summary>
        /// Creates and sends POST request
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="url"></param>
        /// <param name="header"></param>
        /// <param name="requestBodyRaw"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public static void Post(MonoBehaviour caller, string url, WebRequestHeader header, byte[] requestBodyRaw, 
            Action onSuccess = null, Action onError = null)
        {
            UnityWebRequest request = WebRequestFactory.CreateWebRequest(url, WebRequestType.POST, 10, header,
                new UploadHandlerRaw(requestBodyRaw), new DownloadHandlerBuffer());

            caller.StartCoroutine(SendWebRequest_Coroutine(request, _ => onSuccess?.Invoke(), onError));
        }
        
        /// <summary>
        /// Creates and sends DELETE request
        /// </summary>
        /// <param name="caller"></param>
        /// <param name="url"></param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        public static void Delete(MonoBehaviour caller, string url, Action onSuccess = null, Action onError = null)
        {
            UnityWebRequest request = WebRequestFactory.CreateWebRequest(url, WebRequestType.DELETE, 10);
            caller.StartCoroutine(SendWebRequest_Coroutine(request, _ => onSuccess?.Invoke(), onError));
        }

        /// <summary>
        /// Sends web request and handles its result
        /// </summary>
        /// <param name="request"> request to proceed </param>
        /// <param name="onSuccess"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
        private static IEnumerator SendWebRequest_Coroutine(UnityWebRequest request, 
            Action<string> onSuccess, Action onError)
        {
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                Debug.Log($"<color=green>{request.method} request successful:</color> {response}");
                onSuccess?.Invoke(response);
            }
            else
            {
                Debug.LogError($"{request.method} request error: {request.error}");
                onError?.Invoke();
            }
        }
    }
}