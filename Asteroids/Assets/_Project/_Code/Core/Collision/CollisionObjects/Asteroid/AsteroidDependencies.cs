using System;
using _Project._Code.Core.Gameplay.Score;
using _Project._Code.Core.MemoryPools;
using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.Parameters;

namespace _Project._Code.Core.Collision.CollisionObjects.Asteroid
{
    public class AsteroidDependencies : EnemyDependencies<SpawnParams, Asteroid>, IOnAsteroidDestroyed
    {
        public event Action OnAsteroidDestroyed;


        
        public AsteroidDependencies(IAddScore scoreSystem, AsteroidPool memoryPool,
            IAsteroidScoreCount asteroidScoreCount, ExplodeEffectPool explodeEffectPool) : base(scoreSystem, memoryPool, explodeEffectPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
            Score = asteroidScoreCount.AsteroidScoreCount;
            ExplodeEffectPool = explodeEffectPool;
        }

        public override void HandleDestroyed(Asteroid item)
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