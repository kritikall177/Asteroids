using System;
using _Project._Code.Services.Analytics;
using Zenject;

namespace _Project._Code.Core.Installers
{
    public class SceneLoader : IInitializable, IDisposable, ISceneLoad
    {
        private ZenjectSceneLoader _sceneLoader;
        private IFirebaseConfigUpdated _firebaseConfigUpdated;

        [Inject]
        public SceneLoader(ZenjectSceneLoader sceneLoader, IFirebaseConfigUpdated firebaseConfigUpdated)
        {
            _sceneLoader = sceneLoader;
            _firebaseConfigUpdated = firebaseConfigUpdated;
        }

        public void Initialize()
        {
            _firebaseConfigUpdated.OnFirebaseConfigUpdated += LoadMenuScene;
        }

        public void Dispose()
        {
            _firebaseConfigUpdated.OnFirebaseConfigUpdated -= LoadMenuScene;
        }

        public void LoadMenuScene()
        {
            _sceneLoader.LoadScene("MenuScene");
        }

        public void LoadGameScene()
        {
            _sceneLoader.LoadScene("GameScene");
        }
    }

    public interface ISceneLoad
    {
        public void LoadMenuScene();
        public void LoadGameScene();
    }
}