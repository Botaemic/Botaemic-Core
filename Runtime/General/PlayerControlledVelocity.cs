using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Botaemic.Core
{
    public class PlayerControlledVelocity : MonoBehaviour
    {
        [SerializeField] private Vector3 _velocity = Vector3.zero;
        [SerializeField] private KeyCode _keyPositive = KeyCode.D;
        [SerializeField] private KeyCode _keyNegative = KeyCode.A;

        private Rigidbody _rb;

        #region Unity Functions
        void FixedUpdate()
        {
            if (Input.GetKey(_keyPositive))
                GetComponent<Rigidbody>().velocity += _velocity;

            if (Input.GetKey(_keyNegative))
                GetComponent<Rigidbody>().velocity -= _velocity;
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
