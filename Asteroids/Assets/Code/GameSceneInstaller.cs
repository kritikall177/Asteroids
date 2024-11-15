using UnityEngine;
using Zenject;

namespace Code
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Asteroid _asteroid;
        [SerializeField] private AsteroidRespawnSystem _asteroidRespawnSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<IInputSystem>().To<InputSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<AsteroidRespawnSystem>().FromInstance(_asteroidRespawnSystem).AsSingle().NonLazy();
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
            Container.BindMemoryPool<Asteroid, AsteroidPool>().FromComponentInNewPrefab(_asteroid);
        }
    }
}