using System.Collections.Generic;
using System.Threading.Tasks;
using _Project._Code.Core.Gameplay.Score.ScoreStorage;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;

namespace _Project._Code.Meta.Services
{
    public class CloudDataControl : ICloudDataControl
    {
        private IScoreStorage _scoreStorage;

        public CloudDataControl(IScoreStorage scoreStorage)
        {
            _scoreStorage = scoreStorage;
        }

        public async void LoadData()
        {
            Dictionary<string, Item> serverData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>() { _scoreStorage.HighScore, _scoreStorage.Time });

            if (serverData.ContainsKey(_scoreStorage.HighScore) && serverData.ContainsKey(_scoreStorage.Time))
            {
                var score = serverData[_scoreStorage.HighScore].Value.GetAsString();
                var time = serverData[_scoreStorage.Time].Value.GetAsString();
                _scoreStorage.LoadCloudHighScores(score, time);
            }
            else
            {
                _scoreStorage.LoadLocalHighScores();
                Debug.Log("No Cloud High Score Loaded");
            }
        }
        
        public async Task SaveData()
        {
            _scoreStorage.SaveLocalHighScores();
            var data = new Dictionary<string, object>() { { _scoreStorage.HighScore, _scoreStorage.GetScoreString() }, 
                {_scoreStorage.Time, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} };
            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }
    }

    public interface ICloudDataControl
    {
        public void LoadData();
        public Task SaveData();
    }
}