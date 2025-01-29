using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Core.Gameplay.Score.ScoreStorage;
using _Project._Code.Meta.DataConfig;
using _Project._Code.Meta.Services.Ads;
using _Project._Code.Meta.Services.Analytics;
using _Project._Code.Meta.Sounds;
using UnityEngine;
using Zenject;

namespace _Project._Code.Meta.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SoundBackGroundManager _soundBackGroundManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputSystem.InputSystemService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FirebaseAnalytic>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameAnalytics>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameStateActions>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AdsService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ConfigsGameInitilizer>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameConfigProvider>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreStorage>().AsSingle().NonLazy();
            Container.BindInterfacesTo<IAPService>().AsSingle().NonLazy();

            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<SoundBackGroundManager>().FromComponentInNewPrefab(_soundBackGroundManager).AsSingle().NonLazy();

            Container.BindInterfacesTo<SceneLoader>().AsSingle().NonLazy();
        }
    }
}