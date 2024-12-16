using System.Collections.Generic;

namespace _Project._Code.Gameplay.Score.ScoreStorage
{
    public interface IScoreStorage
    {
        public List<int> HighScores { get; }
        public void TryAddInBestScore(int score);
    }
}