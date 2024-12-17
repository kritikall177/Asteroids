using System;
using UnityEngine;
using Zenject;

namespace _Project._Code.Gameplay.GameState.GamePause
{
    public class GamePauseHandler : IInitializable, IDisposable
    {
        private IGameStateActionsSubscriber _gameStateActionsSubscriber;
        
        public GamePauseHandler(IGameStateActionsSubscriber gameStateActionsSubscriber)
        {
            _gameStateActionsSubscriber = gameStateActionsSubscriber;
        }


        public void Initialize()
        {
            _gameStateActionsSubscriber.OnGamePause += GamePause;
            _gameStateActionsSubscriber.OnGameResume += GameResume;
        }

        public void Dispose()
        {
            _gameStateActionsSubscriber.OnGamePause -= GamePause;
            _gameStateActionsSubscriber.OnGameResume -= GameResume;
        }

        private void GamePause()
        {
            Debug.Log("GamePause");
            Time.timeScale = 0f;
        }

        private void GameResume()
        {
            Debug.Log("GameResume");
            Time.timeScale = 1f;
        }
    }
}