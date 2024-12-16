using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace _Project._Code.System.GameStorage
{
    public class ScoreStorage : IInitializable, IDisposable, IScoreStorage
    {
        private string _highScore = "HighScore";
        private int _maxHighScoreCount = 3;

        public List<int> HighScores { get; private set; } = new List<int>();


        public void Initialize()
        {
            LoadHighScores();
        }

        public void Dispose()
        {
            SaveHighScores();
        }

        private void LoadHighScores()
        {
            var str = PlayerPrefs.GetString(_highScore);
            if (str != String.Empty)
            {
                string[] parts = str.Split(',');
                HighScores = new List<int>(Array.ConvertAll(parts, int.Parse));
            }
        }

        private void SaveHighScores()
        {
            var str = string.Join(",", HighScores);
            PlayerPrefs.SetString(_highScore, str);
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