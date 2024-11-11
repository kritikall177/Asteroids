using Zenject;

namespace Code
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To<InputSystem>().FromNew().AsSingle().NonLazy();
        }
    }
}