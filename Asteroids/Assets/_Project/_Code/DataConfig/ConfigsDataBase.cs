using System;
using _Project._Code._DataConfig.Configs;
using _Project._Code.DataConfig;
using _Project._Code.DataConfig.Configs;
using _Project._Code.System.Analytics;
using Firebase.RemoteConfig;
using UnityEngine;
using Zenject;

namespace _Project._Code._DataConfig
{
    public class ConfigsDataBase : IAdsIDsConfig, ILaserSettingsConfig, IAsteroidScoreCount, ISaucerScoreCount,
        IAggroZoneConfig, IAsteroidSpeed, IBulletPoolConfig, IScreenWrapBuffer, ILittleAsteroidPoolConfig, ISaucerSpeed,
        IPlayerMovementAcceleration, IInitializable, IDisposable
    {
        public AdsIDsConfig AdsIDsConfig { get; private set; }
        public LaserSettingsConfig LaserSettingsConfig { get; private set; }

        public int AsteroidScoreCount { get; private set; }
        
        public int SaucerScoreCount { get; private set; }
        
        public float SaucerTriggerRadius { get; private set; }
        public float SaucerChasingSpeed { get; private set; }
        
        public float AsteroidSpeed { get; private set; }
        
        public int BulletDespawnTime { get; private set; }
        public float BulletSpeed { get; private set; }
        
        public float LittleAsteroidScaleSize { get; private set; }
        public float LittleAsteroidSpeed { get; private set; }
        
        public float SaucerSpeed { get; private set; }
        
        public float PlayerMovementAcceleration { get; private set; }
        
        public float ScreenWrapBuffer { get; private set; }

        private const string ADS_INITIALIZER_IDS = "AdsInitializerIDs";
        private const string LASER_SHOOTING_PARAMS_ID = "LaserShootingParams";
        private const string ASTEROID_SCORE_COUNT_ID = "AsteroidScoreCount";
        private const string SAUCER_SCORE_COUNT_ID = "SaucerScoreCount";
        private const string SAUCER_TRIGGER_RADIUS_ID = "SaucerTriggerRadius";
        private const string SAUCER_CHASING_SPEED_ID = "SaucerChasingSpeed";
        private const string ASTEROID_SPEED_ID = "AsteroidSpeed";
        private const string BULLET_DESPAWN_TIME_ID = "BulletDespawnTime";
        private const string BULLET_SPEED_ID = "BulletSpeed";
        private const string LITTLE_ASTEROID_SCALE_SIZE_ID = "LittleAsteroidScaleSize";
        private const string LITTLE_ASTEROID_SPEED_ID = "LittleAsteroidSpeed";
        private const string SAUCER_SPEED_ID = "SaucerSpeed";
        private const string PLAYER_MOVEMENT_ACCELERATION_ID = "PlayerMovementAcceleration";
        private const string SCREEN_WRAP_BUFFER_ID = "ScreenWrapBuffer";


        private IFirebaseConfigUpdated _firebaseConfigUpdated;
        
        private FirebaseRemoteConfig _defaultInstance;

        public ConfigsDataBase(IFirebaseConfigUpdated firebaseConfigUpdated)
        {
            _firebaseConfigUpdated = firebaseConfigUpdated;
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

            AdsIDsConfig = InitializeConfig<AdsIDsConfig>(ADS_INITIALIZER_IDS);
            LaserSettingsConfig = InitializeConfig<LaserSettingsConfig>(LASER_SHOOTING_PARAMS_ID);

            AsteroidScoreCount = (int)_defaultInstance.GetValue(ASTEROID_SCORE_COUNT_ID).LongValue;

            SaucerScoreCount = (int)_defaultInstance.GetValue(SAUCER_SCORE_COUNT_ID).LongValue;

            SaucerTriggerRadius = (float)_defaultInstance.GetValue(SAUCER_TRIGGER_RADIUS_ID).DoubleValue;
            SaucerChasingSpeed = (float)_defaultInstance.GetValue(SAUCER_CHASING_SPEED_ID).DoubleValue;

            AsteroidSpeed = (float)_defaultInstance.GetValue(ASTEROID_SPEED_ID).DoubleValue;

            BulletDespawnTime = (int)_defaultInstance.GetValue(BULLET_DESPAWN_TIME_ID).LongValue;
            BulletSpeed = (float)_defaultInstance.GetValue(BULLET_SPEED_ID).DoubleValue;

            LittleAsteroidScaleSize = (float)_defaultInstance.GetValue(LITTLE_ASTEROID_SCALE_SIZE_ID).DoubleValue;
            LittleAsteroidSpeed = (float)_defaultInstance.GetValue(LITTLE_ASTEROID_SPEED_ID).DoubleValue;

            SaucerSpeed = (float)_defaultInstance.GetValue(SAUCER_SPEED_ID).DoubleValue;

            PlayerMovementAcceleration = (float)_defaultInstance.GetValue(PLAYER_MOVEMENT_ACCELERATION_ID).DoubleValue;

            ScreenWrapBuffer = (float)_defaultInstance.GetValue(SCREEN_WRAP_BUFFER_ID).DoubleValue;
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
