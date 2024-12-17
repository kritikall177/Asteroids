using System;

namespace _Project._Code.Core.Gameplay.GameState
{
    public interface IGameStateActionsSubscriber
    {
        public event Action OnGameStart;
        public event Action OnGameOver;
        public event Action OnGamePause;
        public event Action OnGameResume;
    }
}