using System;
using System.Collections;
using System.Collections.Generic;
using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using UnityEngine;
using Zenject;

namespace _Project._Code.System
{
    public class RespawnSystem : IInitializable, IDisposable
    {
        private AsteroidPool _asteroidPool;
        private SaucerPool _saucerPool;
        private SignalBus _signalBus;
        private AsyncProcessor _asyncProcessor;
        
        private int _respawnAsteroidTime = 5;
        private int _respawnSaucerTime = 10;
        private float _spawnRange = 4f;
        private int _maxAsteroidCount = 5;
        private int _maxSoucerCount = 2;
        
        private Camera _mainCamera;
        private List<Vector2> _spawnPosition = new List<Vector2>();

        [Inject]
        public RespawnSystem(AsteroidPool asteroidPool, SaucerPool saucerPool, SignalBus signalBus, AsyncProcessor asyncProcessor)
        {
            _asteroidPool = asteroidPool;
            _saucerPool = saucerPool;
            _signalBus = signalBus;
            _asyncProcessor = asyncProcessor;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameOverSignal>(DisableSpawn);
            _signalBus.Subscribe<GameStartSignal>(EnableSpawn);
            
            _mainCamera = Camera.main;
            
            SetupSpawnPoints();
        }

        public void Dispose()
        {
            _asyncProcessor.StopAllCoroutines();
        }

        private void DisableSpawn()
        {
            _asyncProcessor.StopAllCoroutines();
            _asteroidPool.DespawnAll();
            _saucerPool.DespawnAll();
        }

        private void SetupSpawnPoints()
        {
            Vector2 screenBottomLeft = _mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 screenTopRight = _mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _spawnRange, screenBottomLeft.y + _spawnRange));
            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _spawnRange, screenTopRight.y - _spawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _spawnRange, screenTopRight.y - _spawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _spawnRange, screenBottomLeft.y + _spawnRange));
        }

        private void EnableSpawn()
        {
            AsteroidSpawn();
            _asyncProcessor.StartCoroutine(StartRespawn(AsteroidSpawn, _respawnAsteroidTime));
            _asyncProcessor.StartCoroutine(StartRespawn(SaucerRespawn, _respawnSaucerTime));
        }

        private IEnumerator StartRespawn(Action spawnAction, float respawnDelay)
        {
            yield return new WaitForSeconds(respawnDelay);
            spawnAction?.Invoke();
            _asyncProcessor.StartCoroutine(StartRespawn(spawnAction, respawnDelay));
        }

        private void AsteroidSpawn()
        {
            if (_asteroidPool.NumActive >= _maxAsteroidCount) return;
            _asteroidPool.Spawn(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)], false);
        }

        private void SaucerRespawn()
        {
            if (_saucerPool.NumActive >= _maxSoucerCount) return;
            _saucerPool.Spawn(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)]);
        }
    }
}
