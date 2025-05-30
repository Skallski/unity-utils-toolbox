namespace UtilsToolbox.Utils.IO.JsonIO
{
    public static class JsonSerializer
    {
        public static string Serialize<T>(System.Func<T, string> serializer, T data) 
            where T : class
        {
            try
            {
                return serializer(data);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"JSON serialization error: {e.Message}");
                return null;
            }
        }

        public static T Deserialize<T>(System.Func<string, T> deserializer, string json) 
            where T : class
        {
            try
            {
                return deserializer(json);
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"JSON deserialization error: {e.Message}");
                return null;
            }
        }
    }
}