using System.Collections.Generic;
using _Project._Code.CollisionObjects;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class SaucerPool : MemoryPool<Vector2, FlyingSaucer>
    {
        private float _saucerSpeed = 10f;
        
        private List<FlyingSaucer> _activeSaucers = new List<FlyingSaucer>();

        protected override void OnSpawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(true);
            _activeSaucers.Add(saucer);
        }

        protected override void Reinitialize(Vector2 spawnPosition, FlyingSaucer saucer)
        {
            saucer.transform.position = spawnPosition;
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