using System;

namespace _Project._Code.Core.Gameplay.GameState
{
    public class GameStateActions : IGameStateActionsSubscriber, IGameStateActionsInvoker
    {
        public event Action OnGameStart;
        public event Action OnGamePause;
        public event Action OnGameResume;
        public event Action OnGameOver;

        public void PauseGame()
        {
            OnGamePause?.Invoke();
        }

        public void ResumeGame()
        {
            OnGameResume?.Invoke();
        }

        public void GameOver()
        {
            OnGameOver?.Invoke();
        }

        public void StartGame()
        {
            OnGameStart?.Invoke();
        }
    }
}