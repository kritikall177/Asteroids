using _Project._Code.Core.Collision.CollisionObjects.Asteroid;
using _Project._Code.Core.Collision.CollisionObjects.Bullet;
using _Project._Code.Core.Collision.CollisionObjects.PlayerShip;
using _Project._Code.Core.Collision.CollisionObjects.Saucer;
using _Project._Code.Core.Effects;
using _Project._Code.Core.Gameplay.GameState.GamePause;
using _Project._Code.Core.Gameplay.PlayerControl.PlayerMovement;
using _Project._Code.Core.Gameplay.PlayerControl.PlayerShooting;
using _Project._Code.Core.Gameplay.Respawner;
using _Project._Code.Core.Gameplay.Score;
using _Project._Code.Core.MemoryPools;
using _Project._Code.Meta.Services.Analytics.GameStats;
using _Project._Code.Meta.UI.GameSceneUI;
using UnityEngine;
using Zenject;

namespace _Project._Code.Meta.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Asteroid _asteroid;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        [SerializeField] private SpaceShip _spaceShip;
        [SerializeField] private ExplodeEffect _explodeEffect;
        [SerializeField] private UIRetryOrQuitPanel uiRetryOrQuitPanel;

        public override void InstallBindings()
        {
            BindSystems();
            DeclarePools();
            BindDependencies();
            Container.Bind<SpaceShip>().FromInstance(_spaceShip).AsSingle().NonLazy();
            Container.Bind<UIRetryOrQuitPanel>().FromInstance(uiRetryOrQuitPanel).AsSingle().NonLazy();
        }

        private void BindSystems()
        {
            Container.BindInterfacesTo<PlayerMovement>().AsSingle();
            Container.BindInterfacesTo<Respawner>().AsSingle();
            Container.BindInterfacesTo<BulletShooting>().AsSingle();
            Container.BindInterfacesTo<LaserShooting>().AsSingle();
            Container.BindInterfacesTo<Score>().AsSingle();
            Container.BindInterfacesTo<GameStats>().AsSingle();
            Container.BindInterfacesTo<GamePauseHandler>().AsSingle();
        }

        private void DeclarePools()
        {
            Container.BindMemoryPool<Bullet, BulletsPool>().FromComponentInNewPrefab(_bullet);
            Container.BindMemoryPool<Asteroid, LittleAsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<Asteroid, AsteroidPool>().FromComponentInNewPrefab(_asteroid);
            Container.BindMemoryPool<FlyingSaucer, SaucerPool>().FromComponentInNewPrefab(_flyingSaucer);
            Container.BindMemoryPool<ExplodeEffect, ExplodeEffectPool>().FromComponentInNewPrefab(_explodeEffect);
        }

        private void BindDependencies()
        {
            Container.BindInterfacesTo<AsteroidDependencies>().FromNew().AsSingle();
            Container.BindInterfacesTo<SaucerDependencies>().FromNew().AsSingle();
            Container.Bind<SpaceShipDependencies>().FromNew().AsSingle();
            Container.Bind<BulletDependencies>().FromNew().AsSingle();
            Container.Bind<IOnExplodeInvoke>().To<ExplodeEffectPool>().FromResolve();
        }
    }
}