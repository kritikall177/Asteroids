using System.Collections.Generic;
using Code;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class AsteroidPool : MemoryPool<Vector2, bool, Asteroid>
    {
        private float _littleAsteroidScaleSize = 0.5f;
        private float _asteroidSpeed = 10f;
        private float _littleAsteroidSpeed = 13f;
        private int _fragmentsCount  = 2;
        
        private List<Asteroid> _activeAsteroids = new List<Asteroid>();

        protected override void Reinitialize(Vector2 spawnPosition, bool isLittle, Asteroid asteroid)
        {
            asteroid.transform.position = spawnPosition;
            float asteroidSpeed = _asteroidSpeed;
            
            if (isLittle)
            {
                asteroid.transform.localScale = Vector3.one * _littleAsteroidScaleSize;
                asteroidSpeed = _littleAsteroidSpeed;
            }
            
            asteroid.Rigidbody2D.AddForce(Random.insideUnitCircle.normalized * asteroidSpeed, ForceMode2D.Impulse);
        }
        
        public void DespawnAll()
        {
            var list = new List<Asteroid>(_activeAsteroids);
            
            foreach (var asteroid in list)
            {
                Despawn(asteroid);
            }
        }
        
        protected override void OnSpawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
            _activeAsteroids.Add(asteroid);
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            if (asteroid.transform.localScale.x != _littleAsteroidScaleSize)
            {
                for (int i = 0; i < _fragmentsCount; i++)
                {
                    Spawn(asteroid.transform.position, true);
                }
            }
            //Я хуй знает как это фиксить
            //asteroid.gameObject.SetActive(false);
            //asteroid.Rigidbody2D.linearVelocity = Vector2.zero;
            //asteroid.transform.localScale = Vector3.one;
            _activeAsteroids.Remove(asteroid);
        }
    }
}