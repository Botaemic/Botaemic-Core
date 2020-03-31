using UnityEngine;

namespace Botaemic.Core
{
    public class AddConstantVelocity : MonoBehaviour
    {
        [SerializeField] private Vector3 _velocity = Vector3.zero;

        private Rigidbody _rb;

        #region Unity Functions
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb == null) { LogWarning("No rigidbody detected"); }
        }

        private void FixedUpdate()
        {
            _rb.velocity += _velocity;
            Log(_rb.velocity.ToString());
        }
        #endregion

        #region Logging
        private void Log(string text)
        {
            DebugUtility.Log(text);
        }

        private void LogWarning(string text)
        {
            DebugUtility.Log("WARNING! - " + transform.name +" => " + text);
        }
        #endregion
    }
}