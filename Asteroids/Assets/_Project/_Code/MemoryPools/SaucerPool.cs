using System.Collections.Generic;
using _Project._Code.CollisionObjects;
using _Project._Code.CollisionObjects.Saucer;
using _Project._Code.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class SaucerPool : MemoryPool<SpawnParams, FlyingSaucer>
    {
        private float _saucerSpeed = 10f;
        
        private List<FlyingSaucer> _activeSaucers = new List<FlyingSaucer>();

        protected override void OnSpawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(true);
            _activeSaucers.Add(saucer);
        }

        protected override void Reinitialize(SpawnParams spawnParams, FlyingSaucer saucer)
        {
            saucer.transform.position = spawnParams.SpawnPosition;
            saucer.Rigidbody2D.AddForce(Random.insideUnitCircle.normalized * _saucerSpeed, ForceMode2D.Impulse);
        }

        protected override void OnDespawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(false);
            _activeSaucers.Remove(saucer);
            saucer.Rigidbody2D.linearVelocity = Vector2.zero;
            saucer.Rigidbody2D.angularVelocity = 0f;
        }

        public void DespawnAll()
        {
            var list = new List<FlyingSaucer>(_activeSaucers);
            
            foreach (var saucer in list)
            {
                Despawn(saucer);
            }
        }
    }
}