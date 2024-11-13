using UnityEngine;
using Zenject;

namespace Code
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bullet;
        
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To<InputSystem>().FromNew().AsSingle().NonLazy();
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
        }
    }
}