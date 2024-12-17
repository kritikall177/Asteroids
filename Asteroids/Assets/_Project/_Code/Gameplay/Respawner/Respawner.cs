using System;
using System.Collections;
using System.Collections.Generic;
using _Project._Code.Core;
using _Project._Code.DataConfig;
using _Project._Code.DataConfig.Configs.ClassConfigs;
using _Project._Code.Gameplay.GameState;
using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.Gameplay.Respawner
{
    public class Respawner : IInitializable, IDisposable
    {
        private AsteroidPool _asteroidPool;
        private SaucerPool _saucerPool;
        private IGameStateActionsSubscriber _gameStateActions;
        private AsyncProcessor _asyncProcessor;
        private RespawnerConfig _respawnerConfig;

        private Camera _mainCamera;
        private List<Vector2> _spawnPosition = new List<Vector2>();
        
        public Respawner(AsteroidPool asteroidPool, SaucerPool saucerPool, IGameStateActionsSubscriber gameStateActions,
            AsyncProcessor asyncProcessor, IRespawnerConfig respawnerConfig)
        {
            _asteroidPool = asteroidPool;
            _saucerPool = saucerPool;
            _gameStateActions = gameStateActions;
            _asyncProcessor = asyncProcessor;
            _respawnerConfig = respawnerConfig.RespawnerConfig;
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

            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _respawnerConfig.SpawnRange, screenBottomLeft.y + _respawnerConfig.SpawnRange));
            _spawnPosition.Add(new Vector2(screenBottomLeft.x + _respawnerConfig.SpawnRange, screenTopRight.y - _respawnerConfig.SpawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _respawnerConfig.SpawnRange, screenTopRight.y - _respawnerConfig.SpawnRange));
            _spawnPosition.Add(new Vector2(screenTopRight.x - _respawnerConfig.SpawnRange, screenBottomLeft.y + _respawnerConfig.SpawnRange));
        }

        private void EnableSpawn()
        {
            AsteroidSpawn();
            _asyncProcessor.StartCoroutine(StartRespawn(AsteroidSpawn, _respawnerConfig.RespawnAsteroidTime));
            _asyncProcessor.StartCoroutine(StartRespawn(SaucerRespawn, _respawnerConfig.RespawnSaucerTime));
        }

        private IEnumerator StartRespawn(Action spawnAction, float respawnDelay)
        {
            yield return new WaitForSeconds(respawnDelay);
            spawnAction?.Invoke();
            _asyncProcessor.StartCoroutine(StartRespawn(spawnAction, respawnDelay));
        }

        private void AsteroidSpawn()
        {
            if (_asteroidPool.NumActive >= _respawnerConfig.MaxAsteroidCount) return;
            _asteroidPool.Spawn(GetSpawnParams());
        }

        private void SaucerRespawn()
        {
            if (_saucerPool.NumActive >= _respawnerConfig.MaxSoucerCount) return;
            _saucerPool.Spawn(GetSpawnParams());
        }

        private SpawnParams GetSpawnParams()
        {
            return new SpawnParams(_spawnPosition[UnityEngine.Random.Range(0, _spawnPosition.Count)]);
        }
    }
}