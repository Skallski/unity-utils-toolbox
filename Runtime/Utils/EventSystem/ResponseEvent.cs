using System;

namespace UtilsToolbox.Utils.EventSystem
{
    /// <summary>
    /// Response event
    /// </summary>
    /// <typeparam name="TOutput"> output parameter </typeparam>
    public class ResponseEvent<TOutput>
    {
        private event Func<TOutput>? _func;

        /// <summary>
        /// Adds listener to response event by passing callback to add
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void AddListener(Func<TOutput> callback) => _func += callback;

        /// <summary>
        /// Adds listener to response event by passing callback to remove
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void RemoveListener(Func<TOutput> callback) => _func -= callback;

        /// <summary>
        /// Invokes response event
        /// </summary>
        /// <returns> response </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TOutput Invoke()
        {
            if (_func == null)
            {
                throw new InvalidOperationException("No listeners are attached.");
            }

            return _func.Invoke();
        }
    }
    
    /// <summary>
    /// Response event with single input parameter
    /// </summary>
    /// <typeparam name="TInput"> input parameter </typeparam>
    /// <typeparam name="TOutput"> output parameter </typeparam>
    public class ResponseEvent<TInput, TOutput>
    {
        private event Func<TInput, TOutput>? _func;

        /// <summary>
        /// Adds listener to response event by passing callback to add
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void AddListener(Func<TInput, TOutput> callback) => _func += callback;

        /// <summary>
        /// Adds listener to response event by passing callback to remove
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void RemoveListener(Func<TInput, TOutput> callback) => _func -= callback;

        /// <summary>
        /// Invokes response event with a parameter
        /// </summary>
        /// <param name="input"> parameter with which the event will be invoked  </param>
        /// <returns> response </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public TOutput Invoke(TInput input)
        {
            if (_func == null)
            {
                throw new InvalidOperationException("No listeners are attached.");
            }

            return _func.Invoke(input);
        }
    }
}