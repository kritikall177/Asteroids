using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.DataConfig.Configs.ClassConfigs;

namespace _Project._Code.Meta.DataConfig
{
    public class GameConfigProvider : IGameConfigProvider, IAdsIDsConfig, ILaserSettingsConfig, IAsteroidScoreCount, ISaucerScoreCount,
        IAggroZoneConfig, IAsteroidPoolConfig, IBulletPoolConfig, IScreenWrapBuffer, ILittleAsteroidPoolConfig, ISaucerSpeed,
        IPlayerMovementAcceleration, IRespawnerConfig
    {
        public AdsIDsConfig AdsIDsConfig { get; private set; }
        public RespawnerConfig RespawnerConfig { get; private set; }
        public LaserSettingsConfig LaserSettingsConfig { get; private set; }
        public int AsteroidScoreCount { get; private set; }
        public int SaucerScoreCount { get; private set; }
        public float SaucerTriggerRadius { get; private set; }
        public float SaucerChasingSpeed { get; private set; }
        public float AsteroidSpeed { get; private set; }
        public int FragmentsCount { get; private set; }
        public int BulletDespawnTime { get; private set; }
        public float BulletSpeed { get; private set; }
        public float ScreenWrapBuffer { get; private set; }
        public float LittleAsteroidScaleSize { get; private set; }
        public float LittleAsteroidSpeed { get; private set; }
        public float SaucerSpeed { get; private set; }
        public float PlayerMovementAcceleration { get; private set; }
        
        public void LoadConfig(GameConfig config)
        {
            AdsIDsConfig = config.AdsIDsConfig;
            LaserSettingsConfig = config.LaserSettingsConfig;
            AsteroidScoreCount = config.AsteroidScoreCount;
            SaucerScoreCount = config.SaucerScoreCount;
            SaucerTriggerRadius = config.SaucerTriggerRadius;
            SaucerChasingSpeed = config.SaucerChasingSpeed;
            AsteroidSpeed = config.AsteroidSpeed;
            FragmentsCount = config.FragmentsCount;
            BulletDespawnTime = config.BulletDespawnTime;
            BulletSpeed = config.BulletSpeed;
            LittleAsteroidScaleSize = config.LittleAsteroidScaleSize;
            LittleAsteroidSpeed = config.LittleAsteroidSpeed;
            SaucerSpeed = config.SaucerSpeed;
            PlayerMovementAcceleration = config.PlayerMovementAcceleration;
            ScreenWrapBuffer = config.ScreenWrapBuffer;
            RespawnerConfig = config.RespawnerConfig;
        }
    }
}
