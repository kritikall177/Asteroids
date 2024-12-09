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

        private const string AdsInitializerIDs = "AdsInitializerIDs";
        private const string LaserShootingParamsID = "LaserShootingParams";
        private const string AsteroidScoreCountID = "AsteroidScoreCount";
        private const string SaucerScoreCountID = "SaucerScoreCount";
        private const string SaucerTriggerRadiusID = "SaucerTriggerRadius";
        private const string SaucerChasingSpeedID = "SaucerChasingSpeed";
        private const string AsteroidSpeedID = "AsteroidSpeed";
        private const string BulletDespawnTimeID = "BulletDespawnTime";
        private const string BulletSpeedID = "BulletSpeed";
        private const string LittleAsteroidScaleSizeID = "LittleAsteroidScaleSize";
        private const string LittleAsteroidSpeedID = "LittleAsteroidSpeed";
        private const string SaucerSpeedID = "SaucerSpeed";
        private const string PlayerMovementAccelerationID = "PlayerMovementAcceleration";
        private const string ScreenWrapBufferID = "ScreenWrapBuffer";

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
            
            AdsIDsConfig = InitializeConfig<AdsIDsConfig>(AdsInitializerIDs);
            LaserSettingsConfig = InitializeConfig<LaserSettingsConfig>(LaserShootingParamsID);

            AsteroidScoreCount = (int)_defaultInstance.GetValue(AsteroidScoreCountID).LongValue;
            
            SaucerScoreCount = (int)_defaultInstance.GetValue(SaucerScoreCountID).LongValue;
            
            SaucerTriggerRadius = (float)_defaultInstance.GetValue(SaucerTriggerRadiusID).DoubleValue;
            SaucerChasingSpeed = (float)_defaultInstance.GetValue(SaucerChasingSpeedID).DoubleValue;
            
            AsteroidSpeed = (float)_defaultInstance.GetValue(AsteroidSpeedID).DoubleValue;
            
            BulletDespawnTime = (int)_defaultInstance.GetValue(BulletDespawnTimeID).LongValue;
            BulletSpeed = (float)_defaultInstance.GetValue(BulletSpeedID).DoubleValue;
            
            LittleAsteroidScaleSize = (float)_defaultInstance.GetValue(LittleAsteroidScaleSizeID).DoubleValue;
            LittleAsteroidSpeed = (float)_defaultInstance.GetValue(LittleAsteroidSpeedID).DoubleValue;
            
            SaucerSpeed = (float)_defaultInstance.GetValue(SaucerSpeedID).DoubleValue;
            
            PlayerMovementAcceleration = (float)_defaultInstance.GetValue(PlayerMovementAccelerationID).DoubleValue;
            
            ScreenWrapBuffer = (float)_defaultInstance.GetValue(ScreenWrapBufferID).DoubleValue;
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
