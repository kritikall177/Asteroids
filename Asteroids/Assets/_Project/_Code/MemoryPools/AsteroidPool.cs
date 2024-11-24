using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class AsteroidPool : MemoryPool<Vector2, bool, Asteroid>
    {
        protected float AsteroidSpeed = 10f;
        protected List<Asteroid> ActiveAsteroids = new List<Asteroid>();

        private int _fragmentsCount  = 2;
        private LittleAsteroidPool _littleAsteroidPool;

        [Inject]
        public AsteroidPool(LittleAsteroidPool littleAsteroidPool)
        {
            _littleAsteroidPool = littleAsteroidPool;
        }

        protected AsteroidPool()
        {
            
        }
        
        protected override void OnCreated(Asteroid asteroid)
        {
            asteroid.SetPool(this);
        }

        protected override void Reinitialize(Vector2 spawnPosition, bool isLittle, Asteroid asteroid)
        {
            asteroid.transform.position = spawnPosition;
            asteroid.Rigidbody2D.AddForce(Random.insideUnitCircle.normalized * AsteroidSpeed, ForceMode2D.Impulse);
        }

        protected override void OnSpawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
            ActiveAsteroids.Add(asteroid);
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            for (int i = 0; i < _fragmentsCount; i++)
            {
                _littleAsteroidPool.Spawn(asteroid.transform.position, true);
            }
            
            DespawnAsteroid(asteroid);
        }

        protected void DespawnAsteroid(Asteroid asteroid)
        {
            asteroid.Rigidbody2D.linearVelocity = Vector2.zero;
            asteroid.gameObject.SetActive(false);
            ActiveAsteroids.Remove(asteroid);
        }

        public void DespawnAll()
        {
            var list = new List<Asteroid>(ActiveAsteroids);
            
            foreach (var asteroid in list)
            {
                Despawn(asteroid);
            }
            
            _littleAsteroidPool?.DespawnAll();
        }
    }
}