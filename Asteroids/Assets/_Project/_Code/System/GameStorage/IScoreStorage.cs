using System.Collections.Generic;

namespace _Project._Code.System.GameStorage
{
    public interface IScoreStorage
    {
        public List<int> HighScores { get; }
        public void TryAddInBestScore(int score);
    }
}