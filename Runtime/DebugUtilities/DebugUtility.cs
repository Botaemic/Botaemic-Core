using UnityEngine;

namespace Botaemic.Core
{
    public static class DebugUtility
    {
        public static void HandleErrorIfNullGetComponent<T>(Component component, GameObject source)
        {
#if UNITY_EDITOR
            if (component == null)
            {
                Debug.LogError("Error: expected to find a component of type " + typeof(T) + " on GameObject " + source.name + ", but none were found.");
            }
#endif
        }

        public static void HandleErrorNullGetComponent<T>(GameObject source)
        {
#if UNITY_EDITOR
            Debug.LogError("Error: expected to find a component of type " + typeof(T) + " on GameObject " + source.name + ", but none were found.");
#endif
        }

        public static void Log<T>(Component component)
        {
#if UNITY_EDITOR
            if (component == null)
            {
                Debug.LogError(typeof(T) + " component NOT SET OR IS NULL");
            }
#endif
        }

        public static void Log(string comment)
        {
#if UNITY_EDITOR
            Debug.Log(comment);
 #endif
        }

    }
}