using _Project._Code.Core.Gameplay.Score;
using Zenject;

namespace _Project._Code.Core.Collision.CollisionObjects
{
    public abstract class EnemyDependencies<TParams, TValue> : IDependencies<TValue>
    {
        protected IAddScore ScoreSystem;
        protected MemoryPool<TParams, TValue> MemoryPool;

        protected int Score = 0;

        
        protected EnemyDependencies(IAddScore scoreSystem, MemoryPool<TParams, TValue> memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
        }

        public virtual void HandleDestroyed(TValue item)
        {
            ScoreSystem.AddScore(Score);
            MemoryPool.Despawn(item);
        }
    }
}