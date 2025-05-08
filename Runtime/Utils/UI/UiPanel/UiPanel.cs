namespace UtilsToolbox.Utils.UI.UiPanel
{
    /// <summary>
    /// Generic ui panel class
    /// </summary>
    /// <typeparam name="TPanelType"> enum that specifies pane type </typeparam>
    public abstract class UiPanel<TPanelType> : UiPanelBase
        where TPanelType : System.Enum
    {
        public abstract TPanelType PanelType { get; }
        
        protected sealed override void OpenSelf()
        {
            if (this is IAnimatedOpen animatedOpen)
            {
                ForceOpen();
                
                animatedOpen.OnOpenedBeforeAnimation();
                animatedOpen.HandleAnimatedOpening(() =>
                {
                    animatedOpen.OnOpenedAfterAnimation();
                    OnOpened();
                });
            }
            else
            {
                base.OpenSelf();
            }
        }
        
        protected sealed override void CloseSelf()
        {
            if (this is IAnimatedClose animatedClose)
            {
                animatedClose.OnClosedBeforeAnimation();
                animatedClose.HandleAnimatedClosing(() =>
                {
                    animatedClose.OnClosedAfterAnimation();
                    OnClosed();
                    ForceClose();
                });
            }
            else
            {
                base.CloseSelf();
            }
        }
    }
}