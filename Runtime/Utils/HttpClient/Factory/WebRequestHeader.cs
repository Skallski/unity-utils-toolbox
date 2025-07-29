namespace UtilsToolbox.Utils.HttpClient.Factory
{
    public struct WebRequestHeader
    {
        public readonly string Name;
        public readonly string Value;

        public WebRequestHeader(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}