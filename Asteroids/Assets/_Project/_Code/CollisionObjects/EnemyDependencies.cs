using System;
using _Project._Code.System.Score;
using Zenject;

namespace _Project._Code.CollisionObjects
{
    public abstract class EnemyDependencies<TParams, TValue> : IDependencies<TValue>
    {
        protected IAddScore ScoreSystem;
        protected MemoryPool<TParams, TValue> MemoryPool;
        
        [Inject]
        protected EnemyDependencies(IAddScore scoreSystem, MemoryPool<TParams, TValue> memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
        }
        
        public void HandleDestroyed(TValue item, int score)
        {
            ScoreSystem.AddScore(score);
            MemoryPool.Despawn(item);
        }
    }
}