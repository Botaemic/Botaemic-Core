using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Botaemic.Utils;
using Botaemic.SceneManagement;

//TODO add multi scene loading/unloading to SceneLoader


namespace Botaemic
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        #region Inspector
        [SerializeField] private GameScene _firstLoadedScene = null;

        [Header("Loading Screen")]
        [SerializeField] public ILoadingScreen _loadingScreen = null;

        [Header("Load Event")]
        [SerializeField] private SceneLoadEvent _loadEvent = default;
        #endregion

        #region Properties

        #endregion

        #region Private variables
        private bool _isAlreadyStarted = false;

        private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();

        #endregion

        #region Unity methodes
        private void Start()
        {
            if (_isAlreadyStarted) { return; }
            _isAlreadyStarted = true;

            _loadingScreen.ShowLoadingScreen = false;

            LoadScenes(new GameScene[] { _firstLoadedScene }, null, true);
        }


        private void OnEnable()
        {
            _loadEvent.sceneLoadEvent += LoadScenes;
        }

        private void OnDisable()
        {
            _loadEvent.sceneLoadEvent -= LoadScenes;
        }
        #endregion

        #region Public Methodes
        #endregion

        #region Private Methodes
        private void LoadScenes(GameScene[] scenesToLoad, GameScene[] scenesToUnload, bool showLoadingScreen)
        {
            _scenesToLoad.AddRange(AddScenesToUnload(scenesToUnload));
            _scenesToLoad.AddRange(AddScenesToLoad(scenesToLoad));

            _loadingScreen.ShowLoadingScreen = showLoadingScreen;
            
            LoadingScenes();
            StartCoroutine(LoadingScenes());
        }

        private bool IsSceneLoaded(string sceneName)
        {
            if (SceneManager.sceneCount > 0)
            {
                for (int i = 0; i < SceneManager.sceneCount; ++i)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == sceneName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private List<AsyncOperation> AddScenesToUnload(GameScene[] scenesToUnload)
        {
            List<AsyncOperation> unloadList = new List<AsyncOperation>();
            if(scenesToUnload == null) { return unloadList; }
            foreach (GameScene scene in scenesToUnload)
            {
                // Null state is possible
                if (scene == null) { continue; }
                if(IsSceneLoaded(scene.SceneName))
                {
                    unloadList.Add(SceneManager.UnloadSceneAsync(scene.SceneName));
                }
            }

            return unloadList;
        }

        private List<AsyncOperation> AddScenesToLoad(GameScene[] scenesToLoad)
        {
            List<AsyncOperation> loadList = new List<AsyncOperation>();
            if (scenesToLoad == null) { return loadList; }
            foreach (GameScene scene in scenesToLoad)
            {
                // Null state is possible
                if (scene == null) { continue; }
                if (!IsSceneLoaded(scene.SceneName) && DoesSceneExists(scene.SceneName))
                {
                    loadList.Add(SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive));
                }
            }

            return loadList;
        }

        private bool DoesSceneExists(string name)
        {
            if (string.IsNullOrEmpty(name)) { return false; }
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var lastSlash = scenePath.LastIndexOf("/");
                string sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

                if (string.Compare(name, sceneName, true) == 0)  {  return true;  }
            }

            LogError($"Scene with the name: " + name + " does NOT exists in the buildsettings");
            return false;
        }

        private IEnumerator LoadingScenes()
        {
            float totalProgress = 0;
            while (totalProgress <= 0.9f)
            {
                totalProgress = 0;
                for (int i = 0; i < _scenesToLoad.Count; ++i)
                {
                    totalProgress += _scenesToLoad[i].progress;
                }

                _loadingScreen.Progress = totalProgress / _scenesToLoad.Count;
                yield return null;
            }
            _scenesToLoad.Clear();
            _loadingScreen.ShowLoadingScreen = false;
        }

 
   
    
        #endregion

        #region Logging
        /// <summary>
        /// Logs a message to the console
        /// </summary>
        private void Log(string text)
        {
#if UNITY_EDITOR
            Debug.Log("[SceneLoader]: " + text);
#endif
        }

        /// <summary>
        /// Logs a warning to the console
        /// </summary>
        private void LogWarning(string text)
        {
#if UNITY_EDITOR
            Debug.LogWarning("[SceneLoader]: " + text);
#endif
        }

        /// <summary>
        /// Logs a error to the console
        /// </summary>
        private void LogError(string text)
        {
#if UNITY_EDITOR
            Debug.LogError("[SceneLoader]: " + text);
#endif
        }
        #endregion
    }
}

