using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Botaemic.Core
{
    [ExecuteInEditMode]
    public class CameraShaderEffect : MonoBehaviour
    {
       [SerializeField] private Material _material = null;

        #region Unity Functions
        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, _material);
        }
        #endregion

        #region Logging
        private void Log(string text)
        {
            DebugUtility.Log(text);
        }

        private void LogWarning(string text)
        {
            DebugUtility.Log("WARNING! " + text);
        }
        #endregion
    }
}