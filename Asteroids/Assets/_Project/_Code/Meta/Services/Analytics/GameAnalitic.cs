using System;
using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Meta.Parameters;
using Zenject;

namespace _Project._Code.Meta.Services.Analytics
{
    public class GameAnalytics : IInitializable, IDisposable, IGameAnalytics
    {
        private IAnalytics _analytics;
        private IGameStateActionsSubscriber _gameStateActions;
        
        public GameAnalytics(IAnalytics analytics, IGameStateActionsSubscriber gameStateActions)
        {
            _analytics = analytics;
            _gameStateActions = gameStateActions;
        }

        public void Initialize()
        {
            _gameStateActions.OnGameStart += OnGameStart;
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= OnGameStart;
        }

        public void SendStatistic(GameOverParams gameOverParams)
        {
            _analytics.OnGameEnd(gameOverParams);
        }

        public void OnLaserUsed()
        {
            _analytics.OnLaserUsed();
        }

        private void OnGameStart()
        {
            _analytics.OnGameStart();
        }
    }

    public interface IGameAnalytics
    {
        public void SendStatistic(GameOverParams gameOverParams);
        public void OnLaserUsed();
    }
}