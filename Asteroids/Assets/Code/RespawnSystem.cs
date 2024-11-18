using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code
{
    public class RespawnSystem : MonoBehaviour
    {
        [SerializeField] private int _respawnAsteroidTime = 5;
        [SerializeField] private int _respawnSaucerTime = 10;
        [SerializeField] private float _spawnRange = 2f;
        [SerializeField] private int _maxAsteroidCount = 5;
        [SerializeField] private int _maxSoucerCount = 2;
        
        private AsteroidPool _asteroidPool;
        private SaucerPool _saucerPool;

        private List<Vector2> _spawnPosition = new List<Vector2>();


        [Inject]
        public void Construct(AsteroidPool asteroidPool, SaucerPool saucerPool)
        {
            _asteroidPool = asteroidPool;
            _saucerPool = saucerPool;
            
            Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            
            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _spawnRange, screenBottomLeft.y + _spawnRange));
            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _spawnRange, screenTopRight.y - _spawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _spawnRange, screenTopRight.y - _spawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _spawnRange, screenBottomLeft.y + _spawnRange));

            AsteroidSpawn();
            
            StartCoroutine(StartRespawn(AsteroidSpawn, _respawnAsteroidTime));
            StartCoroutine(StartRespawn(SaucerRespawn, _respawnSaucerTime));
        }
        
        private IEnumerator StartRespawn(Action spawnAction, float respawnDelay)
        {
            while (true)
            {
                yield return new WaitForSeconds(respawnDelay);

                spawnAction?.Invoke();
            }
        }

        private void AsteroidSpawn()
        {
            if(_asteroidPool.NumActive >= _maxAsteroidCount) return;
            var asteroid = _asteroidPool.Spawn();
            asteroid.Launch(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)]);
        }
        
        private void SaucerRespawn()
        {
            if(_saucerPool.NumActive >= _maxSoucerCount) return;
            var saucer = _saucerPool.Spawn();
            saucer.Launch(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)]);
        }

        public void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
