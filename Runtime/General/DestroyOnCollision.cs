using UnityEngine;


namespace Botaemic.Core
{
    public class DestroyOnCollision : MonoBehaviour
    {
        [SerializeField] private string _tag = string.Empty;
        [SerializeField] private bool _destroySelf = false;
        [SerializeField] private bool _destroyOther = false;


        #region Unity Functions
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(_tag))
            {
                if (_destroySelf)
                    Destroy(this.gameObject);
                if (_destroyOther)
                    Destroy(collision.gameObject);
            }
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