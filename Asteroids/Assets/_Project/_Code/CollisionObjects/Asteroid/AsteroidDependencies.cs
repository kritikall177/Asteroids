using System;
using _Project._Code._DataConfig.Configs;
using _Project._Code.DataConfig.Configs;
using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using _Project._Code.System.Score;
using Zenject;

namespace _Project._Code.CollisionObjects.Asteroid
{
    public class AsteroidDependencies : EnemyDependencies<SpawnParams, Asteroid>, IOnAsteroidDestroyed
    {
        public event Action OnAsteroidDestroyed;
        
        
        [Inject]
        public AsteroidDependencies(IAddScore scoreSystem, AsteroidPool memoryPool, IAsteroidScoreCount asteroidScoreCount) : base(scoreSystem, memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
            Score = asteroidScoreCount.AsteroidScoreCount;
        }

        public new void HandleDestroyed(Asteroid item)
        {
            OnAsteroidDestroyed?.Invoke();
            base.HandleDestroyed(item);
        }
    }

    public interface IOnAsteroidDestroyed
    {
        public event Action OnAsteroidDestroyed;
    }
}