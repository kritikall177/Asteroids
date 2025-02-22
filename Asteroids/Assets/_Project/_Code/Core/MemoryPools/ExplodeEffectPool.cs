using System;
using _Project._Code.Core.Collision.CollisionObjects.Asteroid;
using _Project._Code.Core.Effects;
using _Project._Code.Meta.Parameters;
using Zenject;

namespace _Project._Code.Core.MemoryPools
{
    public class ExplodeEffectPool : MemoryPool<SpawnParams, ExplodeEffect>, IOnExplodeInvoke
    {
        public event Action OnExplodeInvoke;
        
        protected override void OnSpawned(ExplodeEffect effect)
        {
            OnExplodeInvoke?.Invoke();
            effect.gameObject.SetActive(true);
        }
        
        protected override void Reinitialize(SpawnParams spawnParams, ExplodeEffect effect)
        {
            effect.transform.position = spawnParams.SpawnPosition;
        }
        
        protected override void OnDespawned(ExplodeEffect effect)
        {
            effect.gameObject.SetActive(false);
        }
    }
}