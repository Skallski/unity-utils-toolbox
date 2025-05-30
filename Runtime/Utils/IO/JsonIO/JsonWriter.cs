using System;
using System.IO;

namespace UtilsToolbox.Utils.IO.JsonIO
{
    public static class JsonWriter
    {
        public static void Write<T>(string filePath, T data, Func<T, string> serializeFunc, 
            Action onSuccess = null, Action onError = null)
            where T : class
        {
            string content = JsonSerializer.Serialize(serializeFunc, data);

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(content);
                    writer.Flush();
                    onSuccess?.Invoke();
                }
            }
            catch (IOException e)
            {
                UnityEngine.Debug.LogError($"Error writing to file '{filePath}':  {e.Message}");
                onError?.Invoke();
            }
        }
        
        public static void Write<T>(string filePath, T data, Action onSuccess = null, Action onError = null)
            where T : class
        {
            Write(filePath, data, UnityEngine.JsonUtility.ToJson, onSuccess, onError);
        }
    }
}