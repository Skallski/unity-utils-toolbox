using System;
using UnityEngine;

namespace UtilsToolbox.Utils.TimeBased.Tweening
{
    public class TweenHandle
    {
        private MonoBehaviour _caller;
        private Coroutine _coroutine;
        private Action _onSkip;
        internal bool IsActive { get; private set; }

        internal TweenHandle(MonoBehaviour caller, Coroutine coroutine, Action onSkip)
        {
            _caller = caller ?? throw new ArgumentNullException("Coroutine caller cannot be null!");
            _coroutine = coroutine ?? throw new ArgumentNullException("Coroutine cannot be null!");
            _onSkip = onSkip ?? throw new ArgumentNullException("Skip action cannot be null!");

            IsActive = true;
        }

        private void InterruptInternal(Action onInterrupt = null)
        {
            if (IsActive == false)
            {
                return;
            }

            _caller.StopCoroutine(_coroutine);
            onInterrupt?.Invoke();
            IsActive = false;
                
            _caller = null;
            _coroutine = null;
            _onSkip = null;
        }

        public void Interrupt()
        {
            InterruptInternal();
        }

        public void Skip()
        {
            InterruptInternal(_onSkip);
        }
    }
}