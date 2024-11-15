using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code
{
    public class AsteroidRespawnSystem : MonoBehaviour
    {
        [SerializeField] private int _respawnTime = 5;
        [SerializeField] private float _spawnRange = 2f;
        
        private AsteroidPool _asteroidPool;

        private List<Vector2> _spawnPosition = new List<Vector2>();


        [Inject]
        public void Construct(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
            
            Vector2 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            
            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _spawnRange, screenBottomLeft.y + _spawnRange));
            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _spawnRange, screenTopRight.y - _spawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _spawnRange, screenTopRight.y - _spawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _spawnRange, screenBottomLeft.y + _spawnRange));

            AsteroidSpawn();
            StartCoroutine(StartRespawn());
        }

        private IEnumerator StartRespawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(_respawnTime);
                
                AsteroidSpawn();
            }
        }

        private void AsteroidSpawn()
        {
            var asteroid = _asteroidPool.Spawn();
            asteroid.Launch(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)]);
        }

        public void OnDestroy()
        {
            StopCoroutine(StartRespawn());
        }
    }
}
