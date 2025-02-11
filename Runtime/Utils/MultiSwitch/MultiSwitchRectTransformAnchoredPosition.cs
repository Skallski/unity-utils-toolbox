using UnityEngine;

namespace UtilsToolbox.Utils.MultiSwitch
{
    public class MultiSwitchRectTransformAnchoredPosition : MultiSwitchWithParams<RectTransform, Vector2>
    {
        protected override void SetStateInternalAction(RectTransform rectTransform, Vector2 anchoredPosition)
        {
            rectTransform.anchoredPosition = anchoredPosition;
        }
    }
}