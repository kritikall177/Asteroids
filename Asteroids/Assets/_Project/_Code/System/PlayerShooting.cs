using System;
using System.Collections;
using _Project._Code.CollisionObjects;
using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using _Project._Code.SpawnParameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.System
{
    public class PlayerShooting : IInitializable, IDisposable
    {
        private BulletsPool _bulletsPool;
        private IInputSystem _inputSystem;
        private IGameStateActionsSubscriber _gameStateActions;
        private AsyncProcessor _asyncProcessor;
        
        private GameObject _laserGameObject;
        private int _maxLaserCharge = 2;
        private int _laserCharge = 2;
        private float _laserActiveTime = 0.5f;
        private int _laserRestoreTime = 20;


        private Vector2 _direction;
        
        private Camera _mainCamera;
        private Transform _cachedTransform;

        [Inject]
        public PlayerShooting(IInputSystem inputSystem, BulletsPool bulletsPool, IGameStateActionsSubscriber gameStateActions,
            AsyncProcessor asyncProcessor, SpaceShip spaceShip)
        {
            _inputSystem = inputSystem;
            _bulletsPool = bulletsPool;
            _gameStateActions = gameStateActions;
            _asyncProcessor = asyncProcessor;

            _cachedTransform = spaceShip.transform;
            _laserGameObject = spaceShip.LaserGameObject;
        }

        public void Initialize()
        {
            _inputSystem.OnAttackEvent += BulletAttack;
            _inputSystem.OnHeavyAttackEvent += LaserAttack;
            _inputSystem.OnLookEvent += ShootDirection;
            
            _gameStateActions.OnGameStart += EnableShoot;
            _gameStateActions.OnGameOver += DisableShoot;
            
            _mainCamera = Camera.main;
        }

        public void Dispose()
        {
            _inputSystem.OnAttackEvent -= BulletAttack;
            _inputSystem.OnHeavyAttackEvent -= LaserAttack;
            _inputSystem.OnLookEvent -= ShootDirection;
            
            _gameStateActions.OnGameStart -= EnableShoot;
            _gameStateActions.OnGameOver -= DisableShoot;
        }

        private void EnableShoot()
        {
            //_signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));
        }

        private void DisableShoot()
        {
            _asyncProcessor.StopAllCoroutines();
            _laserGameObject.SetActive(false);
            _laserCharge = 2;
        }

        private void ShootDirection(Vector2 position)
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(position);
            _direction = new Vector2(mousePosition.x - _cachedTransform.position.x,
                mousePosition.y - _cachedTransform.position.y).normalized;
        }

        private void LaserAttack(bool isAttack)
        {
            if (isAttack && _laserCharge > 0 && !_laserGameObject.activeSelf)
            {
                _asyncProcessor.StartCoroutine(ActivateLaser());
                _asyncProcessor.StartCoroutine(RestoreLaser());
            }
        }

        private IEnumerator ActivateLaser()
        {
            _laserGameObject.SetActive(true);
            _laserCharge -= 1;
            //_signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));

            yield return new WaitForSeconds(_laserActiveTime);

            _laserGameObject.SetActive(false);
        }

        private IEnumerator RestoreLaser()
        {
            yield return new WaitForSeconds(_laserRestoreTime);

            if (_laserCharge < _maxLaserCharge)
            {
                _laserCharge += 1;
                //_signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));
            }
        }

        private void BulletAttack(bool isAttack)
        {
            if (isAttack)
            {
                _bulletsPool.Spawn(new BulletParams(_cachedTransform.position, _direction));
            }
        }
    }
}
