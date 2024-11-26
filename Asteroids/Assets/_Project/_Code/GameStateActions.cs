using System;

namespace _Project._Code
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

    public interface IGameStateActionsSubscriber
    {
        public event Action OnGameStart;
        public event Action OnGameOver;
    }
    
    public interface IGameStateActionsInvoker
    {
        public void StopGame();
        public void StartGame();
    }

    public interface IGameStateActions : IGameStateActionsSubscriber, IGameStateActionsInvoker
    {
        
    }
}