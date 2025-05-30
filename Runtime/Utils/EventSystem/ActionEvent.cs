using System;

namespace UtilsToolbox.Utils.EventSystem
{
    /// <summary>
    /// Action event without parameters
    /// <example>
    /// public static readonly EventEx sampleEvent = new EventEx();
    /// </example>>
    /// </summary>
    public class ActionEvent
    {
        private event Action _action = delegate { };
        
        /// <summary>
        /// Adds listener to action event by passing callback to add
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void AddListener(Action callback) => _action += callback;
        
        /// <summary>
        /// Removes listener from action event by passing callback to remove
        /// </summary>
        /// <param name="callback"> callback that will be removed from event  </param>
        public void RemoveListener(Action callback) => _action -= callback;
        
        /// <summary>
        /// Invokes action event
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Invoke()
        {
            if (_action == null)
            {
                throw new InvalidOperationException("No listeners are attached.");
            }
            
            _action.Invoke();
        }
    }

    /// <summary>
    /// Action event with single parameter
    /// </summary>
    /// <typeparam name="T"> input parameter </typeparam>
    public class ActionEvent<T>
    {
        private event Action<T> _action = delegate { };
        
        /// <summary>
        /// Adds listener to action event by passing callback to add
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void AddListener(Action<T> callback) => _action += callback;
        
        /// <summary>
        /// Removes listener from action event by passing callback to remove
        /// </summary>
        /// <param name="callback"> callback that will be removed from event  </param>
        public void RemoveListener(Action<T> callback) => _action -= callback;
        
        /// <summary>
        /// Invokes action event with a parameter
        /// </summary>
        /// <param name="param"> parameter with which the event will be invoked </param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Invoke(T param)
        {
            if (_action == null)
            {
                throw new InvalidOperationException("No listeners are attached.");
            }
            
            _action.Invoke(param);
        }
    }
    
    /// <summary>
    /// Action event with two parameters
    /// </summary>
    /// <typeparam name="T1"> first input parameter </typeparam>
    /// <typeparam name="T2"> second input parameter </typeparam>
    public class ActionEvent<T1, T2>
    {
        private event Action<T1, T2> _action = delegate { };
        
        /// <summary>
        /// Adds listener to action event by passing callback to add
        /// </summary>
        /// <param name="callback"> callback that will be added to the event </param>
        public void AddListener(Action<T1, T2> callback) => _action += callback;
        
        /// <summary>
        /// Removes listener from action event by passing callback to remove
        /// </summary>
        /// <param name="callback"> callback that will be removed from event </param>
        public void RemoveListener(Action<T1, T2> callback) => _action -= callback;
        
        /// <summary>
        /// Invokes action event with two parameters
        /// </summary>
        /// <param name="paramOne"> first parameter, with which the event will be invoked </param>
        /// <param name="paramTwo"> second parameter, with which the event will be invoked </param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Invoke(T1 paramOne, T2 paramTwo)
        {
            if (_action == null)
            {
                throw new InvalidOperationException("No listeners are attached.");
            }
            
            _action.Invoke(paramOne, paramTwo);
        }
    }
}