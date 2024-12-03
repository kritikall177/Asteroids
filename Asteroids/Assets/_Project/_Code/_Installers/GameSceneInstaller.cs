using _Project._Code.CollisionObjects;
using _Project._Code.CollisionObjects.Asteroid;
using _Project._Code.CollisionObjects.Bullet;
using _Project._Code.CollisionObjects.Saucer;
using _Project._Code.MemoryPools;
using _Project._Code.System;
using _Project._Code.System.GameState;
using _Project._Code.System.GameStats;
using _Project._Code.System.InputSystem;
using _Project._Code.System.PlayerMovement;
using _Project._Code.System.PlayerShooting;
using _Project._Code.System.Score;
using UnityEngine;
using Zenject;

namespace _Project._Code._Installers
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
            BindDependencies();
            Container.Bind<SpaceShip>().FromInstance(_spaceShip).AsSingle().NonLazy();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<PlayerMovement>().AsSingle();
            Container.BindInterfacesTo<Respawner>().AsSingle();
            Container.BindInterfacesTo<BulletShooting>().AsSingle();
            Container.BindInterfacesTo<LaserShooting>().AsSingle();
            Container.BindInterfacesTo<Score>().AsSingle();
            Container.BindInterfacesTo<GameStats>().AsSingle();
        }

        private void DeclarePools()
        {
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
            Container.BindMemoryPool<Asteroid, LittleAsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<Asteroid, AsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<FlyingSaucer, SaucerPool>().FromComponentInNewPrefab(_flyingSaucer);
        }

        private void BindDependencies()
        {
            Container.BindInterfacesTo<AsteroidDependencies>().FromNew().AsSingle();
            Container.BindInterfacesTo<SaucerDependencies>().FromNew().AsSingle();
            Container.Bind<SpaceShipDependencies>().FromNew().AsSingle();
            Container.Bind<BulletDependencies>().FromNew().AsSingle();
        }
    }
}