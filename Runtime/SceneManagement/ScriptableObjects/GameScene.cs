using UnityEngine;

namespace Botaemic.SceneManagement
{
    [CreateAssetMenu(fileName = "GameScene", menuName = "Game Event/Game Scene")]
    public class GameScene : ScriptableObject
    {
        #region Inspector
        [SerializeField] protected string _name;
        [SerializeField] [Multiline] protected string _description;
        [SerializeField] protected string _sceneName;
        [SerializeField] protected Sprite _sprite;
        #endregion

        #region Properties
        public string Name => _name;
        public string Description => _description;
        public string SceneName => _sceneName;
        public Sprite Sprite => _sprite;
        #endregion


        //public static GameScene CreateInstance(string sceneName)
        //{
        //    var data = ScriptableObject.CreateInstance<GameScene>();
        //    data.Init(sceneName);
        //    return data;
        //}

        //public void Init(string sceneName)
        //{
        //    _name = "";
        //    _description = "";
        //    _sceneName = sceneName;
        //    _sprite = null;
        //}
    }
}