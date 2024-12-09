using System;
using _Project._Code._DataConfig.Configs;
using _Project._Code.DataConfig.Configs;
using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using _Project._Code.System.Score;
using Zenject;

namespace _Project._Code.CollisionObjects.Saucer
{
    public class SaucerDependencies : EnemyDependencies<SpawnParams, FlyingSaucer>, IOnSaucerDestroyed
    {
        public event Action OnSaucerDestroyed;
        
        [Inject]
        public SaucerDependencies(IAddScore scoreSystem, SaucerPool memoryPool, ISaucerScoreCount saucerScoreCount) : base(scoreSystem, memoryPool)
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