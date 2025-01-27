using _Project._Code.Core.Gameplay.Score;
using _Project._Code.Core.MemoryPools;
using _Project._Code.Meta.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Collision.CollisionObjects
{
    public abstract class EnemyDependencies<TParams, TValue> : IDependencies<TValue> where TValue : MonoBehaviour

    {
        protected IAddScore ScoreSystem;
        protected MemoryPool<TParams, TValue> MemoryPool;
        protected ExplodeEffectPool ExplodeEffectPool;

        protected int Score = 0;

        
        protected EnemyDependencies(IAddScore scoreSystem, MemoryPool<TParams, TValue> memoryPool, ExplodeEffectPool explodeEffectPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
            ExplodeEffectPool = explodeEffectPool;
        }

        public virtual void HandleDestroyed(TValue item)
        {
            ScoreSystem.AddScore(Score);
            ExplodeEffectPool.Spawn(new SpawnParams(item.transform.position));
            MemoryPool.Despawn(item);
        }
    }
}