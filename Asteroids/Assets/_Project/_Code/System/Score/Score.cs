using System;

namespace _Project._Code.System.Score
{
    public class Score : IScore
    {
        private int _score;
        public event Action OnScoreChanged;

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