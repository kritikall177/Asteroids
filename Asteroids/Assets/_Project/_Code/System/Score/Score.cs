using System;
using _Project._Code.System.GameState;
using Zenject;

namespace _Project._Code.System.Score
{
    public class Score : IScore, IInitializable, IDisposable
    {
        public event Action OnScoreChanged;
        
        private  IGameStateActionsSubscriber _gameStateActions;

        private int _score;

        [Inject]
        public Score(IGameStateActionsSubscriber gameStateActions)
        {
            _gameStateActions = gameStateActions;
        }

        public void Initialize()
        {
            _gameStateActions.OnGameStart += ResetScore;
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= ResetScore;
        }

        private void ResetScore()
        {
            _score = 0;
        }

        public void AddScore(int score)
        {
            if (score > 0)
            {
                _score += score;
                OnScoreChanged?.Invoke();
            }
        }

        public int GetScore()
        {
            return _score;
        }
    }
}