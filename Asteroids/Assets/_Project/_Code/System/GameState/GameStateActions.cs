using System;

namespace _Project._Code.System.GameState
{
    public class GameStateActions : IGameStateActions
    {
        public event Action OnGameStart;
        public event Action OnGameOver;

        public void StopGame()
        {
            OnGameOver?.Invoke();
        }

        public void StartGame()
        {
            OnGameStart?.Invoke();
        }
    }
}