using System;
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
        public SaucerDependencies(IAddScore scoreSystem, SaucerPool memoryPool) : base(scoreSystem, memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
        }
        
        public new void HandleDestroyed(FlyingSaucer item, int score)
        {
            OnSaucerDestroyed?.Invoke();
            base.HandleDestroyed(item, score);
        }
    }

    public interface IOnSaucerDestroyed
    {
        public event Action OnSaucerDestroyed;
    }
}