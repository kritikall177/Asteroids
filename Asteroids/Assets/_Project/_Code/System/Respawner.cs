using System;
using System.Collections;
using System.Collections.Generic;
using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using _Project._Code.System.GameState;
using UnityEngine;
using Zenject;

namespace _Project._Code.System
{
    public class Respawner : IInitializable, IDisposable
    {
        private AsteroidPool _asteroidPool;
        private SaucerPool _saucerPool;
        private IGameStateActionsSubscriber _gameStateActions;
        private AsyncProcessor _asyncProcessor;
        
        private int _respawnAsteroidTime = 5;
        private int _respawnSaucerTime = 10;
        private float _spawnRange = 4f;
        private int _maxAsteroidCount = 5;
        private int _maxSoucerCount = 2;
        
        private Camera _mainCamera;
        private List<Vector2> _spawnPosition = new List<Vector2>();

        [Inject]
        public Respawner(AsteroidPool asteroidPool, SaucerPool saucerPool, IGameStateActionsSubscriber gameStateActions,
            AsyncProcessor asyncProcessor)
        {
            _asteroidPool = asteroidPool;
            _saucerPool = saucerPool;
            _gameStateActions = gameStateActions;
            _asyncProcessor = asyncProcessor;
        }

        public void Initialize()
        {
            _gameStateActions.OnGameStart += EnableSpawn;
            _gameStateActions.OnGameOver += DisableSpawn;
            
            _mainCamera = Camera.main;
            
            SetupSpawnPoints();
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= EnableSpawn;
            _gameStateActions.OnGameOver -= DisableSpawn;
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
            _asteroidPool.Spawn(GetSpawnParams());
        }

        private void SaucerRespawn()
        {
            if (_saucerPool.NumActive >= _maxSoucerCount) return;
            _saucerPool.Spawn(GetSpawnParams());
        }

        private SpawnParams GetSpawnParams()
        {
            return new SpawnParams(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)]);
        }
    }
}
