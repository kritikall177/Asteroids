using System;
using _Project._Code.System.Score;
using Zenject;

namespace _Project._Code.CollisionObjects
{
    public abstract class EnemyDependencies<TParams, TValue> : IDependencies<TValue>
    {
        protected IAddScore ScoreSystem;
        protected MemoryPool<TParams, TValue> MemoryPool;
        
        protected int Score = 0;
        
        [Inject]
        protected EnemyDependencies(IAddScore scoreSystem, MemoryPool<TParams, TValue> memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
        }
        
        public void HandleDestroyed(TValue item)
        {
            ScoreSystem.AddScore(Score);
            MemoryPool.Despawn(item);
        }
    }
}