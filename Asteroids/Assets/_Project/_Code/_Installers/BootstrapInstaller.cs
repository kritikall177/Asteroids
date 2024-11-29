using _Project._Code.System.GameState;
using _Project._Code.System.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project._Code._Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameStateActions>().AsSingle().NonLazy();
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<Bootstrap>().AsSingle().NonLazy();
        }
    }
}