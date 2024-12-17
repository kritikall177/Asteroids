using System;
using _Project._Code.Core.Gameplay.Score;
using _Project._Code.Core.MemoryPools;
using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.Parameters;

namespace _Project._Code.Core.Collision.CollisionObjects.Saucer
{
    public class SaucerDependencies : EnemyDependencies<SpawnParams, FlyingSaucer>, IOnSaucerDestroyed
    {
        public event Action OnSaucerDestroyed;

        
        public SaucerDependencies(IAddScore scoreSystem, SaucerPool memoryPool, ISaucerScoreCount saucerScoreCount) :
            base(scoreSystem, memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
            Score = saucerScoreCount.SaucerScoreCount;
        }

        public override void HandleDestroyed(FlyingSaucer item)
        {
            OnSaucerDestroyed?.Invoke();
            base.HandleDestroyed(item);
        }
    }

    public interface IOnSaucerDestroyed
    {
        public event Action OnSaucerDestroyed;
    }
}