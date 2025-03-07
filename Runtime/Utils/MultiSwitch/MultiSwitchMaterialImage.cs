using UnityEngine;
using UnityEngine.UI;

namespace UtilsToolbox.Utils.MultiSwitch
{
    public class MultiSwitchMaterialImage : MultiSwitchWithParams<Image, Material>
    {
        protected override void SetStateInternalAction(Image image, Material material)
        {
            image.material = material;
        }
    }
}