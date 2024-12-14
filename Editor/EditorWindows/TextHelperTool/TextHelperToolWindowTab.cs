namespace UtilsToolbox.EditorWindows.TextHelperTool
{
    internal abstract class TextHelperToolWindowTab
    {
        internal string TabName { get; private set; }

        internal TextHelperToolWindowTab(string tabName)
        {
            TabName = tabName;
        }

        internal abstract void Reset();
        
        internal abstract void OnGUI();
    }
}