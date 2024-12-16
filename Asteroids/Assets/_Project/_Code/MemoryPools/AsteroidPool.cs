using System.Collections.Generic;
using _Project._Code.CollisionObjects;
using _Project._Code.CollisionObjects.Asteroid;
using _Project._Code.DataConfig.Configs;
using _Project._Code.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class AsteroidPool : MemoryPool<SpawnParams, Asteroid>
    {
        protected List<Asteroid> ActiveAsteroids = new List<Asteroid>();

        private IAsteroidSpeed _asteroidSpeed;
        
        private int _fragmentsCount  = 2;
        private LittleAsteroidPool _littleAsteroidPool;

        [Inject]
        public AsteroidPool(LittleAsteroidPool littleAsteroidPool, IAsteroidSpeed asteroidSpeed)
        {
            _littleAsteroidPool = littleAsteroidPool;
            _asteroidSpeed = asteroidSpeed;
        }

        protected AsteroidPool()
        {
            
        }

        protected override void OnSpawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
            ActiveAsteroids.Add(asteroid);
        }

        protected override void Reinitialize(SpawnParams spawnParams, Asteroid asteroid)
        {
            asteroid.transform.localScale = Vector3.one;
            asteroid.transform.position = spawnParams.SpawnPosition;
            asteroid.Rigidbody2D.AddForce(Random.insideUnitCircle.normalized * _asteroidSpeed.AsteroidSpeed, ForceMode2D.Impulse);
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            if (asteroid.transform.localScale == Vector3.one)
            {
                for (int i = 0; i < _fragmentsCount; i++)
                {
                    _littleAsteroidPool.Spawn(new SpawnParams(asteroid.transform.position));
                }
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