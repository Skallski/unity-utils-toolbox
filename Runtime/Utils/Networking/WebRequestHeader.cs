namespace UtilsToolbox.Utils.Networking
{
    public class WebRequestHeader
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