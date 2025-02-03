using System.Collections.Generic;

namespace _Project._Code.Core.Gameplay.Score.ScoreStorage
{
    public interface IScoreStorage
    {
        public string HighScore { get; }
        public string Time { get; }
        
        public List<int> HighScores { get; }
        public string SaveTime { get; }
        public void TryAddInBestScore(int score);

        public void LoadCloudHighScores(string score, string time);
        public string GetScoreString();
        public void LoadLocalHighScores();

        public void SaveLocalHighScores();
    }
}