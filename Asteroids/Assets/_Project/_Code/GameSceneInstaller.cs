using _Project._Code.MemoryPools;
using Code.Signals;
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
            DeclareSignals();

            Container.Bind<IInputSystem>().To<InputSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<RespawnSystem>().FromInstance(respawnSystem).AsSingle().NonLazy();
            
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
            Container.BindMemoryPool<Asteroid, LittleAsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<Asteroid, AsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<FlyingSaucer, SaucerPool>().FromComponentInNewPrefab(_flyingSaucer);
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<GameStartSignal>();
            Container.DeclareSignal<AddScoreSignal>();
            Container.DeclareSignal<UpdateTransformSignal>();
            Container.DeclareSignal<UpdateLaserCountSignal>();
        }
    }
}