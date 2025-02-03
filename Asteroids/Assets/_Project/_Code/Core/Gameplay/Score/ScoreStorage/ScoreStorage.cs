using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gameplay.Score.ScoreStorage
{
    public class ScoreStorage : IScoreStorage
    {
        public string HighScore { get; } = "HighScore";
        public string Time { get; } = "Time";
        
        private int _maxHighScoreCount = 3;

        public List<int> HighScores { get; private set; } = new List<int>();
        public string SaveTime { get; private set; }

        public void LoadCloudHighScores(string score, string time)
        {
            if (score != String.Empty)
            {
                string[] parts = score.Split(',');
                HighScores = new List<int>(Array.ConvertAll(parts, int.Parse));
                SaveTime = time;
            }
        }
        
        public void LoadLocalHighScores()
        {
            var str = PlayerPrefs.GetString(HighScore);
            SaveTime = PlayerPrefs.GetString(Time);
            if (str != String.Empty)
            {
                string[] parts = str.Split(',');
                HighScores = new List<int>(Array.ConvertAll(parts, int.Parse));
            }
        }
        
        public string GetScoreString()
        {
            return string.Join(",", HighScores);
        }

        public void SaveLocalHighScores()
        {
            var str = GetScoreString();
            PlayerPrefs.SetString(HighScore, str);
            PlayerPrefs.SetString(Time, SaveTime);
            PlayerPrefs.Save();
        }

        public void TryAddInBestScore(int score)
        {
            if (HighScores.Count < _maxHighScoreCount)
            {
                HighScores.Add(score);
            }
            else
            {
                HighScores.Sort();
                if (score > HighScores[0])
                {
                    HighScores[0] = score;
                }
            }

            HighScores.Sort();
        }
    }
}