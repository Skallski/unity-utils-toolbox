using System;
using UnityEngine;
using UtilsToolbox.Utils.IO.JsonIO;

namespace UtilsToolbox.Utils.Networking.Rest.Json
{
    public static class JsonOverHttpHandler
    {
        public static void Get<T>(MonoBehaviour caller, string url, Func<string, T> deserializer,
            Action<T> onSuccess, Action onError = null) 
            where T : class
        {
            WebRequestSender.Get(caller, url, response => 
                {
                    T result = JsonSerializer.Deserialize(deserializer, response);
                    if (result != null)
                    {
                        onSuccess?.Invoke(result);
                    }
                    else
                    {
                        onError?.Invoke();
                    }
                },
                onError
            );
        }
        
        public static void Get<T>(MonoBehaviour caller, string url, 
            Action<T> onSuccess, Action onError = null) 
            where T : class
        {
            Get(caller, url, JsonUtility.FromJson<T>, onSuccess, onError);
        }

        public static void Put<T>(MonoBehaviour caller, string url, T data, Func<T, string> serializer,
            Action onSuccess = null, Action onError = null) 
            where T : class
        {
            string jsonData = JsonSerializer.Serialize(serializer, data);
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
            
            WebRequestSender.Put(caller, url, new WebRequestHeader("Content-Type", "application/json"), bodyRaw,
                onSuccess, onError);
        }
        
        public static void Put<T>(MonoBehaviour caller, string url, T data, 
            Action onSuccess = null, Action onError = null) 
            where T : class
        {
            Put(caller, url, data, JsonUtility.ToJson, onSuccess, onError);
        }

        public static void Post<T>(MonoBehaviour caller, string url, T data, Func<T, string> serializer,
            Action onSuccess = null, Action onError = null)
            where T : class
        {
            string jsonData = JsonSerializer.Serialize(serializer, data);
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
            
            WebRequestSender.Post(caller, url, new WebRequestHeader("Content-Type", "application/json"), bodyRaw,
                onSuccess, onError);
        }
        
        public static void Post<T>(MonoBehaviour caller, string url, T data, 
            Action onSuccess = null, Action onError = null)
            where T : class
        {
            Post(caller, url, data, JsonUtility.ToJson, onSuccess, onError);
        }
    }
}