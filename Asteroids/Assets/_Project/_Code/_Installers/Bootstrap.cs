using UnityEngine.SceneManagement;
using Zenject;

namespace _Project._Code._Installers
{
    public class Bootstrap : IInitializable
    {
        private ZenjectSceneLoader _sceneLoader;

        [Inject]
        public Bootstrap(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }


        public void Initialize()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            _sceneLoader.LoadScene("GameScene");
        }
    }
}