using System;
using _Project._Code.Parameters;
using _Project._Code.System.Analytics;
using _Project._Code.System.GameState;
using _Project._Code.System.PlayerShooting;
using Zenject;

namespace _Project._Code.System.GameStats
{
    public class GameStats : IInitializable, IDisposable
    {
        private IGameStateActionsSubscriber _gameStateActions;
        private IGameAnalytics _gameAnalytics;
        private IOnLaserInvoke _onLaserInvoke;
        private IOnBulletInvoke _onBulletInvoke;

        private int _totalShots;
        private int _laserUses;
        private int _destroyedAsteroids;
        private int _destroyedUFOs;

        [Inject]
        public GameStats(IGameStateActionsSubscriber gameStateActions, IGameAnalytics gameAnalytics,
            IOnLaserInvoke onLaserInvoke, IOnBulletInvoke onBulletInvoke)
        {
            _gameStateActions = gameStateActions;
            _gameAnalytics = gameAnalytics;
            _onLaserInvoke = onLaserInvoke;
            _onBulletInvoke = onBulletInvoke;
        }


        public void Initialize()
        {
            _gameStateActions.OnGameStart += ResetStats;
            _gameStateActions.OnGameOver += SendGameStats;
            _onLaserInvoke.OnLaserInvoke += OnLaserInvoke;
            _onBulletInvoke.OnBulletInvoke += OnBulletInvoke;
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= ResetStats;
            _gameStateActions.OnGameOver -= SendGameStats;
            _onLaserInvoke.OnLaserInvoke -= OnLaserInvoke;
            _onBulletInvoke.OnBulletInvoke -= OnBulletInvoke;
        }

        private void SendGameStats()
        {
            _gameAnalytics.SendStatistic(new GameOverParams(_totalShots, _laserUses, _destroyedAsteroids, _destroyedUFOs));
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