using System;
using System.Collections;
using UnityEngine;

namespace UtilsToolbox.Utils.TimeBased.CoroutineHelper
{
    public static class Delayer
    {
        /// <summary>
        /// Delays callback invocation by single frame
        /// </summary>
        /// <param name="callback"> action that will be performed after the delay </param>
        /// <returns></returns>
        public static IEnumerator DelaySingleFrame(Action callback)
        {
            yield return null;
            callback?.Invoke();
        }

        /// <summary>
        /// Delays callback invocation by amount of time
        /// </summary>
        /// <param name="callback"> action that will be performed after the delay </param>
        /// <param name="time"> amount of time that delays the callback </param>
        /// <returns></returns>
        public static IEnumerator Delay(Action callback, float time)
        {
            return Delay(callback, new WaitForSeconds(time));
        }
        
        /// <summary>
        /// Delays callback invocation by a coroutine
        /// </summary>
        /// <param name="callback"> action that will be performed after the delay </param>
        /// <param name="coroutine"> coroutine to wait for </param>
        /// <returns></returns>
        public static IEnumerator Delay(Action callback, IEnumerator coroutine)
        {
            yield return coroutine;
            callback?.Invoke();
        }

        /// <summary>
        /// Delays callback invocation by a custom yield instruction
        /// </summary>
        /// <param name="callback"> action that will be performed after the delay </param>
        /// <param name="customYieldInstruction"> custom yield instruction to wait for </param>
        /// <returns></returns>
        public static IEnumerator Delay(Action callback, CustomYieldInstruction customYieldInstruction)
        {
            yield return customYieldInstruction;
            callback?.Invoke();
        }
        
        /// <summary>
        /// Delays callback invocation by a yield instruction
        /// </summary>
        /// <param name="callback"> action that will be performed after the delay </param>
        /// <param name="yieldInstruction"> basic yield instruction to wait for </param>
        /// <returns></returns>
        public static IEnumerator Delay(Action callback, YieldInstruction yieldInstruction)
        {
            yield return yieldInstruction;
            callback?.Invoke();
        }
    }
}