using UnityEngine;

namespace UtilsToolbox.Utils.MultiSwitch
{
    public class MultiSwitchAnimationController : MultiSwitchWithParams<Animator, RuntimeAnimatorController>
    {
        protected override void SetStateInternalAction(Animator animator, RuntimeAnimatorController controller)
        {
            animator.runtimeAnimatorController = controller;
        }
    }
}