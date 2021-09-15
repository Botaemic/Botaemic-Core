﻿using UnityEngine;

namespace Botaemic.Utils
{
    public abstract class Singleton<T> : Singleton where T : MonoBehaviour
    {
        #region  Fields
        private static T _instance;

        private static readonly object Lock = new object();

        [SerializeField] private bool _persistent = false;
        #endregion

        #region  Properties
        public static T Instance
        {
            get
            {
                if (Quitting)
                {
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                    // ReSharper disable once AssignNullToNotNullAttribute
                    return null;
                }
                //lock (Lock)
                //{
                //if (_instance != null)
                //    return _instance;
                var instances = FindObjectsOfType<T>() as T[];
                var count = instances.Length;
                if (count > 0)
                {
                    if (count == 1)
                        return _instance = instances[0];
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                    for (var i = 1; i < count; i++)
                    {
                        Destroy(instances[i].gameObject);
                    }
                    return _instance = instances[0];
                }

                Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}")
                           .AddComponent<T>();
                //}
            }
        }
        #endregion

        #region  Methods
        private void Awake()
        {
            _instance = Instance;
            if (_persistent)
            {
                DontDestroyOnLoad(gameObject);
            }
            OnAwake();
        }

        private void OnEnable()
        {
            _instance = Instance;
        }

        protected virtual void OnAwake() { }
        #endregion
    }

    public abstract class Singleton : MonoBehaviour
    {
        #region  Properties
        public static bool Quitting { get; private set; }
        #endregion

        #region  Methods
        private void OnApplicationQuit()
        {
            Quitting = true;
        }
        #endregion
    }
}