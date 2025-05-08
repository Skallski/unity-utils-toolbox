namespace UtilsToolbox.Utils.UI.UiPanel
{
    public class UiPanelOpeningParameters
    {
        public UiPanelOpeningParameters()
        {
            
        }
        
        public bool TryGet<T>(out T result) where T : UiPanelOpeningParameters
        {
            if (this is T casted)
            {
                result = casted;
                return true;
            }

            result = null;
            return false;
        }
    }
}