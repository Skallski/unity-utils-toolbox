namespace UtilsToolbox.Utils.UI.UiPanel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAnimatedClose
    {
        /// <summary>
        /// Called before closing animation is being triggered
        /// </summary>
        void OnClosedBeforeAnimation();

        /// <summary>
        /// Called after closing animation is finished
        /// </summary>
        void OnClosedAfterAnimation();

        /// <summary>
        /// Handles animated closing
        /// Make sure to invoke callback when the animation is finished!
        /// </summary>
        void HandleAnimatedClosing(System.Action onAnimationFinished)
        {
            // default implementation
            onAnimationFinished?.Invoke();
        }
    }
}