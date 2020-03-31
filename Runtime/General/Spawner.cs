using UnityEngine;

namespace Botaemic.Core
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] GameObject _prefab = null;
        [SerializeField] Vector3 _facingDirection = Vector3.zero;
        [SerializeField] float _timeIntervals = 3f;
        [SerializeField] Vector3 _spawnPosJitter = Vector3.zero;

        private float _timer = 0;


        #region Unity Functions

        private void Start()
        {
            _timer = _timeIntervals;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = _timeIntervals;

                Vector3 v3SpawnPos = transform.position;
                v3SpawnPos += Vector3.right * _spawnPosJitter.x * (Random.value - 0.5f);
                v3SpawnPos += Vector3.forward * _spawnPosJitter.z * (Random.value - 0.5f);
                v3SpawnPos += Vector3.up * _spawnPosJitter.y * (Random.value - 0.5f);

                Instantiate(_prefab, v3SpawnPos, Quaternion.LookRotation(_facingDirection, Vector3.up));
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
