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

        public static bool CheckIfComponentIsSet<T>(T source)
        {
            if (source == null)
            {
#if UNITY_EDITOR
                Debug.LogError(typeof(T) + " component NOT SET OR IS NULL");
#endif
                return false;
            }
            return true;
        }


        public static bool CheckIfComponentIsSet<T>(T component, GameObject source)
        {
            if (component == null)
            {
#if UNITY_EDITOR
                Debug.LogError(typeof(T) + " component NOT SET OR IS NULL on GameObject " + source.name);
#endif
                return false;
            }
            return true;
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

        public static void Log<T>(Component component, Component neededIn)
        {
#if UNITY_EDITOR
            if (component == null)
            {
                Debug.LogError(neededIn.name +" GameObject: " +typeof(T) + " component NOT SET OR IS NULL");
            }
#endif
        }

        public static void Log(string comment)
        {
#if UNITY_EDITOR
            Debug.Log(comment);
 #endif
        }

        public static void LogWarning(string comment)
        {
#if UNITY_EDITOR
            Debug.LogWarning(comment);
#endif
        }

        public static void LogError(string comment)
        {
#if UNITY_EDITOR
            Debug.LogError(comment);
#endif
        }

    }
}