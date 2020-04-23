using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Botaemic.Core.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {

        #region Instance
        private static SceneLoader instance;
        public static SceneLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SceneLoader>();
                    if (instance == null)
                    {
                        instance = new GameObject("Spawned LevelLoader", typeof(SceneLoader)).GetComponent<SceneLoader>();
                    }
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        #endregion
        [SerializeField] private GameObject _loadingScreen = null;
        [SerializeField] private Text _loadingText = null;


        private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
        private float _totalLoadingProgression = 0f;

        #region Public Accessors
    
        #endregion

        #region Unity Functions
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(this);
            }
        }
        #endregion

        #region Public Functions
        public void LoadSceneAdditive(int sceneIndex)
        {
            if (SceneManager.GetSceneByBuildIndex(sceneIndex).IsValid())
            {
                _scenesLoading.Clear();
                _loadingScreen?.gameObject.SetActive(true);
                _scenesLoading.Add(SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive));

                StartCoroutine(GetSceneLoadProgress());
            }
            else
            {
                LogWarning("SceneLoader: Invalid sceneIndex passed!");
            }
        }
        #endregion

        #region Private Functions
        private IEnumerator GetSceneLoadProgress()
        {
            for (int i = 0; i < _scenesLoading.Count; i++)
            {
                while (!_scenesLoading[i].isDone)
                {
                    _totalLoadingProgression = 0;
                    foreach (AsyncOperation operation in _scenesLoading)
                    {
                        _totalLoadingProgression += operation.progress;
                    }

                    _totalLoadingProgression = (_totalLoadingProgression / _scenesLoading.Count) * 100f;

                    if (_loadingText != null)
                    {
                        _loadingText.text = "Loading: " + Mathf.Abs(_totalLoadingProgression) + "%";
                    }

                    yield return null;
                }
            }

            _loadingScreen?.gameObject.SetActive(false);
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