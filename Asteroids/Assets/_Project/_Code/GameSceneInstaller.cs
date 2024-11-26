using _Project._Code.CollisionObjects;
using _Project._Code.MemoryPools;
using _Project._Code.System;
using _Project._Code.System.GameState;
using _Project._Code.System.InputSystem;
using _Project._Code.System.PlayerMovement;
using _Project._Code.System.PlayerShooting;
using _Project._Code.System.Score;
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
            BindSystems();
            DeclarePools();
            Container.Bind<SpaceShip>().FromInstance(_spaceShip).AsSingle().NonLazy();
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<InputSystem>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameStateActions>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerMovement>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<Respawner>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<BulletShooting>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<LaserShooting>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesTo<Score>().FromNew().AsSingle().NonLazy();
        }

        private void DeclarePools()
        {
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
            Container.BindMemoryPool<Asteroid, LittleAsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<Asteroid, AsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<FlyingSaucer, SaucerPool>().FromComponentInNewPrefab(_flyingSaucer);
        }
    }
}