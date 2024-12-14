using UnityEngine;

namespace UtilsToolbox.Constructions.Generic
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError($"Singleton's instance cannot be set! missing object of type: {typeof(T)}");
                    }
                }

                return _instance;
            }
        }

        private T Target => this as T;

        protected virtual void Awake()
        {
            if (_instance != null && _instance != Target)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = Target;
                SetInstanceInternal();
            }
        }

        protected virtual void SetInstanceInternal() {}
    }
}