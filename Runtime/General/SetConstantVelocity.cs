using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Botaemic.Core
{
    public class SetConstantVelocity : MonoBehaviour
    {
        [SerializeField] private Vector3 _velocity;

        private Rigidbody _rb;

        #region Unity Functions
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null) { LogWarning("No rigidbody detected"); }
        }

        private void Start()
        {
            _rb.velocity = _velocity;
        }
        #endregion

        #region Logging
        private void Log(string text)
        {
            DebugUtility.Log(text);
        }

        private void LogWarning(string text)
        {
            DebugUtility.Log("WARNING! - " + transform.name + " => " + text);
        }
        #endregion
    }
}