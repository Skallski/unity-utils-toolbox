namespace UtilsToolbox.Utils.UI.UiPanel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAnimatedOpen
    {
        /// <summary>
        /// Called before opening animation is being triggered
        /// </summary>
        void OnOpenedBeforeAnimation();

        /// <summary>
        /// Called after opening animation is finished
        /// </summary>
        void OnOpenedAfterAnimation();

        /// <summary>
        /// Handles animated opening
        /// Make sure to invoke callback when the animation is finished!
        /// </summary>
        void HandleAnimatedOpening(System.Action onAnimationFinished)
        {
            // default implementation
            onAnimationFinished?.Invoke();
        }
    }
}