using System;
using System.IO;
using UnityEngine;

namespace UtilsToolbox.Utils.IO.JsonIO
{
    public static class JsonReader
    {
        public static void Read<T>(string filePath, Func<string, T> deserializeFunc, 
            Action<T> onSuccess, Action onError = null)
            where T : class
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string text = reader.ReadToEnd();
                    T data = JsonSerializer.Deserialize(deserializeFunc, text);
                    if (data != null)
                    {
                        onSuccess?.Invoke(data);
                    }
                    else
                    {
                        onError?.Invoke();
                    }
                }
            }
            catch (IOException e)
            {
                UnityEngine.Debug.LogError($"Error reading from file '{filePath}': {e.Message}");
                onError?.Invoke();
            }
        }
        
        public static void Read<T>(string filePath, Action<T> onSuccess, Action onError = null)
            where T : class
        {
            Read(filePath, UnityEngine.JsonUtility.FromJson<T>, onSuccess, onError);
        }
    }
}