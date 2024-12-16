using _Project._Code.DataConfig;
using _Project._Code.Gameplay.GameState;
using _Project._Code.Gameplay.Score.ScoreStorage;
using _Project._Code.Services.Ads;
using _Project._Code.Services.Analytics;
using Zenject;

namespace _Project._Code.Core.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputSystem.InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<FirebaseAnalytic>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameAnalytics>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameStateActions>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AdsControl>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ConfigsDataBase>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreStorage>().AsSingle().NonLazy();

            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.BindInterfacesTo<SceneLoader>().AsSingle().NonLazy();
        }
    }
}