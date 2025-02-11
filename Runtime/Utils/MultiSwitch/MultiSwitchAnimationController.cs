using UnityEditor.Animations;
using UnityEngine;

namespace UtilsToolbox.Utils.MultiSwitch
{
    public class MultiSwitchAnimationController : MultiSwitchWithParams<Animator, AnimatorController>
    {
        protected override void SetStateInternalAction(Animator animator, AnimatorController value)
        {
            animator.runtimeAnimatorController = value;
        }
    }
}