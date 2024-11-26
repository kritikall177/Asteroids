using System;

namespace _Project._Code.System.GameState
{
    public interface IGameStateActionsSubscriber
    {
        public event Action OnGameStart;
        public event Action OnGameOver;
    }
}