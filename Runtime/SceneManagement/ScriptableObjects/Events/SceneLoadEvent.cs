using UnityEngine;
using UnityEngine.Events;

namespace Botaemic.SceneManagement
{
    [CreateAssetMenu(fileName = "LoadGameEvent", menuName = "Game Event/Load")]
    public class SceneLoadEvent : ScriptableObject
    {
        public UnityAction<GameScene[], GameScene[], bool> sceneLoadEvent;
        public void RaiseEvent(GameScene[] scenesToLoad, GameScene[] scenesToUnload = null, bool displayLoadingScreen = false)
        {
            sceneLoadEvent.Invoke(scenesToLoad, scenesToUnload, displayLoadingScreen);
        }
    }
}