using System;
using _Project._Code.System.Analytics;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project._Code._Installers
{
    public class Bootstrap : IInitializable, IDisposable
    {
        private ZenjectSceneLoader _sceneLoader;
        private IFirebaseConfigUpdated _firebaseConfigUpdated;

        [Inject]
        public Bootstrap(ZenjectSceneLoader sceneLoader, IFirebaseConfigUpdated firebaseConfigUpdated)
        {
            _sceneLoader = sceneLoader;
            _firebaseConfigUpdated = firebaseConfigUpdated;
        }

        public void Initialize()
        {
            _firebaseConfigUpdated.OnFirebaseConfigUpdated += LoadGame;
        }

        public void Dispose()
        {
            _firebaseConfigUpdated.OnFirebaseConfigUpdated -= LoadGame;
        }

        private void LoadGame()
        {
            _sceneLoader.LoadScene("GameScene");
        }
    }
}