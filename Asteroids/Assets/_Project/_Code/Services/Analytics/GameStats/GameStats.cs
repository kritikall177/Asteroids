using System;
using _Project._Code.Collision.CollisionObjects.Asteroid;
using _Project._Code.Collision.CollisionObjects.Saucer;
using _Project._Code.Gameplay.GameState;
using _Project._Code.Gameplay.PlayerControl.PlayerShooting;
using _Project._Code.Parameters;
using Zenject;

namespace _Project._Code.Services.Analytics.GameStats
{
    public class GameStats : IInitializable, IDisposable
    {
        private IGameStateActionsSubscriber _gameStateActions;
        private IGameAnalytics _gameAnalytics;
        private IOnLaserInvoke _onLaserInvoke;
        private IOnBulletInvoke _onBulletInvoke;
        private IOnAsteroidDestroyed _onAsteroidDestroy;
        private IOnSaucerDestroyed _onSaucerDestroy;


        private int _totalShots;
        private int _laserUses;
        private int _destroyedAsteroids;
        private int _destroyedUFOs;

        [Inject]
        public GameStats(IGameStateActionsSubscriber gameStateActions, IGameAnalytics gameAnalytics,
            IOnLaserInvoke onLaserInvoke, IOnBulletInvoke onBulletInvoke, IOnAsteroidDestroyed onAsteroidDestroy,
            IOnSaucerDestroyed onSaucerDestroy)
        {
            _gameStateActions = gameStateActions;
            _gameAnalytics = gameAnalytics;
            _onLaserInvoke = onLaserInvoke;
            _onBulletInvoke = onBulletInvoke;
            _onAsteroidDestroy = onAsteroidDestroy;
            _onSaucerDestroy = onSaucerDestroy;
        }


        public void Initialize()
        {
            _gameStateActions.OnGameStart += ResetStats;
            _gameStateActions.OnGameOver += SendGameStats;
            _onLaserInvoke.OnLaserInvoke += OnLaserInvoke;
            _onBulletInvoke.OnBulletInvoke += OnBulletInvoke;
            _onAsteroidDestroy.OnAsteroidDestroyed += OnAsteroidDestroyOnOnEnemyDestroy;
            _onSaucerDestroy.OnSaucerDestroyed += OnSaucerDestroyOnOnEnemyDestroy;
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= ResetStats;
            _gameStateActions.OnGameOver -= SendGameStats;
            _onLaserInvoke.OnLaserInvoke -= OnLaserInvoke;
            _onBulletInvoke.OnBulletInvoke -= OnBulletInvoke;
            _onAsteroidDestroy.OnAsteroidDestroyed -= OnAsteroidDestroyOnOnEnemyDestroy;
            _onSaucerDestroy.OnSaucerDestroyed -= OnSaucerDestroyOnOnEnemyDestroy;
        }

        private void SendGameStats()
        {
            _gameAnalytics.SendStatistic(new GameOverParams(_totalShots, _laserUses, _destroyedAsteroids,
                _destroyedUFOs));
        }

        private void OnSaucerDestroyOnOnEnemyDestroy()
        {
            _destroyedAsteroids++;
        }

        private void OnAsteroidDestroyOnOnEnemyDestroy()
        {
            _destroyedUFOs++;
        }

        private void OnBulletInvoke()
        {
            _totalShots++;
        }

        private void OnLaserInvoke()
        {
            _laserUses++;
            _gameAnalytics.OnLaserUsed();
        }

        private void ResetStats()
        {
            _totalShots = 0;
            _laserUses = 0;
            _destroyedAsteroids = 0;
            _destroyedUFOs = 0;
        }
    }
}