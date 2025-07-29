using System;
using System.Collections;
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
        public static void Get<T>(MonoBehaviour caller, string url, Action<T> onSuccess, Action onError = null) 
            where T : class
        {
            UnityWebRequest getRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.GET);
            
            caller.StartCoroutine(SendWebRequest_Coroutine(getRequest, 
                response =>
                {
                    T result = JsonUtility.FromJson<T>(response);
                    if (result != null)
                    {
                        onSuccess?.Invoke(result);
                    }
                    else
                    {
                        onError?.Invoke();
                    }
                }, 
                onError)
            );
        }

        /// <summary>
        /// Crates and sends PUT request
        /// </summary>
        public static void Put<T>(MonoBehaviour caller, string url, T data, Action onSuccess = null, Action onError = null) 
            where T : class
        {
            UnityWebRequest putRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.PUT, data);
            caller.StartCoroutine(SendWebRequest_Coroutine(putRequest, _ => onSuccess?.Invoke(), onError));
        }

        /// <summary>
        /// Creates and sends POST request
        /// </summary>
        public static void Post<T>(MonoBehaviour caller, string url, T data, Action onSuccess = null, Action onError = null)
            where T : class
        {
            UnityWebRequest postRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.POST, data);
            caller.StartCoroutine(SendWebRequest_Coroutine(postRequest, _ => onSuccess?.Invoke(), onError));
        }

        /// <summary>
        /// Creates and sends DELETE request
        /// </summary>
        public static void Delete(MonoBehaviour caller, string url, Action onSuccess = null, Action onError = null)
        {
            UnityWebRequest deleteRequest = WebRequestFactory.CreateWebRequest(url, WebRequestType.DELETE);
            caller.StartCoroutine(SendWebRequest_Coroutine(deleteRequest, _ => onSuccess?.Invoke(), onError));
        }

        /// <summary>
        /// Sends web request and handles its result
        /// </summary>
        private static IEnumerator SendWebRequest_Coroutine(UnityWebRequest request, 
            Action<string> onSuccess, Action onError)
        {
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler?.text;
                Debug.Log($"<color=green>{request.method} [{request.responseCode}]:</color> {response}");
                onSuccess?.Invoke(response);
            }
            else
            {
                Debug.LogError($"<color=red>{request.method} [{request.responseCode}]:</color> {request.error}");
                onError?.Invoke();
            }
        }
    }
}