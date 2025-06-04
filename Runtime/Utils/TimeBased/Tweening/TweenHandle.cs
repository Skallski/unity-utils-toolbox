using UnityEngine;

namespace UtilsToolbox.Utils.TimeBased.Tweening
{
    public class TweenHandle
    {
        private readonly MonoBehaviour _caller;
        private Coroutine _coroutine;
        internal bool IsActive { get; private set; }

        internal TweenHandle(MonoBehaviour caller, Coroutine coroutine)
        {
            _caller = caller;
            _coroutine = coroutine;
            IsActive = true;
        }
        
        public void Interrupt()
        {
            if (IsActive && _coroutine != null && _caller != null)
            {
                _caller.StopCoroutine(_coroutine);
                IsActive = false;
                _coroutine = null;
            }
        }
    }
}