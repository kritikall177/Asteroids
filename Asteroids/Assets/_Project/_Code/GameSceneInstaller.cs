using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using _Project._Code.System;
using UnityEngine;
using Zenject;

namespace _Project._Code
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Asteroid _asteroid;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        [SerializeField] private SpaceShip _spaceShip;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            DeclareSignals();

            Container.BindInterfacesTo<InputSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<SpaceShip>().FromInstance(_spaceShip).AsSingle().NonLazy();
            Container.BindInterfacesTo<MovementSystem>().FromNew().AsSingle().NonLazy();
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
            Container.BindInterfacesTo<RespawnSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<ShootingSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreSystem>().FromNew().AsSingle().NonLazy();
            
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
            Container.DeclareSignal<UpdateScoreUI>();
        }
    }
}