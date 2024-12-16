using System;
using _Project._Code.DataConfig.Configs;
using _Project._Code.DataConfig.Configs.ClassConfigs;
using _Project._Code.Services.Analytics;
using Firebase.RemoteConfig;
using UnityEngine;
using Zenject;

namespace _Project._Code.DataConfig
{
    public class ConfigsGameInitilizer : IInitializable, IDisposable
    {
        private const string GAME_CONFIG_ID = "GameConfig";

        private IFirebaseConfigUpdated _firebaseConfigUpdated;
        private FirebaseRemoteConfig _defaultInstance;
        private IGameConfigProvider _gameConfig;

        public ConfigsGameInitilizer(IFirebaseConfigUpdated firebaseConfigUpdated, IGameConfigProvider gameConfig)
        {
            _firebaseConfigUpdated = firebaseConfigUpdated;
            _gameConfig = gameConfig;
        }

        public void Initialize()
        {
            _firebaseConfigUpdated.OnFirebaseConfigUpdated += ConfigUpdated;
        }

        public void Dispose()
        {
            _firebaseConfigUpdated.OnFirebaseConfigUpdated -= ConfigUpdated;
        }

        public void ConfigUpdated()
        {
            _defaultInstance = FirebaseRemoteConfig.DefaultInstance;
            _gameConfig.LoadConfig(InitializeConfig<GameConfig>(GAME_CONFIG_ID));
        }

        private T InitializeConfig<T>(string firebaseID) where T : class, new()
        {
            var json = _defaultInstance.GetValue(firebaseID).StringValue;
            if (!string.IsNullOrEmpty(json))
            {
                return JsonUtility.FromJson<T>(json);
            }

            return new T();
        }
    }

}