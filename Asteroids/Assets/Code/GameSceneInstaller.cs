using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Asteroid _asteroid;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        [FormerlySerializedAs("_asteroidRespawnSystem")] [SerializeField] private RespawnSystem respawnSystem;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<GameStartSignal>();
            
            Container.Bind<IInputSystem>().To<InputSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<RespawnSystem>().FromInstance(respawnSystem).AsSingle().NonLazy();
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
            Container.BindMemoryPool<Asteroid, AsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<FlyingSaucer, SaucerPool>().FromComponentInNewPrefab(_flyingSaucer);
            Container.Bind<GameManager>().FromNew().AsSingle().NonLazy();
        }
    }
}