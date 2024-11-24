using System;
using System.Collections;
using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using UnityEngine;
using Zenject;

namespace _Project._Code.System
{
    public class ShootingSystem : IInitializable, IDisposable
    {
        private BulletsPool _bulletsPool;
        private IInputSystem _inputSystem;
        private SignalBus _signalBus;
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
        public ShootingSystem(IInputSystem inputSystem, BulletsPool bulletsPool, SignalBus signalBus,
            AsyncProcessor asyncProcessor, SpaceShip spaceShip)
        {
            _inputSystem = inputSystem;
            _bulletsPool = bulletsPool;
            _signalBus = signalBus;
            _asyncProcessor = asyncProcessor;

            _cachedTransform = spaceShip.transform;
            _laserGameObject = spaceShip.LaserGameObject;
        }

        public void Initialize()
        {
            _inputSystem.OnAttackEvent += Attack;
            _inputSystem.OnHeavyAttackEvent += HeavyAttack;
            _inputSystem.OnLookEvent += ShootDirection;
            
            _signalBus.Subscribe<GameStartSignal>(EnableShoot);
            _signalBus.Subscribe<GameOverSignal>(DisableShoot);
            
            _mainCamera = Camera.main;
        }

        public void Dispose()
        {
            _inputSystem.OnAttackEvent -= Attack;
            _inputSystem.OnHeavyAttackEvent -= HeavyAttack;
            _inputSystem.OnLookEvent -= ShootDirection;
        }

        private void EnableShoot()
        {
            _signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));
        }

        private void DisableShoot()
        {
            _asyncProcessor.StopAllCoroutines();
            _laserGameObject.SetActive(false);
            _laserCharge = 2;
        }

        private void Attack(bool isAttack)
        {
            if (isAttack)
            {
                BulletAttack();
            }
        }

        private void HeavyAttack(bool isAttack)
        {
            if (isAttack)
            {
                LaserAttack();
            }
        }

        private void ShootDirection(Vector2 position)
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(position);
            _direction = new Vector2(mousePosition.x - _cachedTransform.position.x,
                mousePosition.y - _cachedTransform.position.y).normalized;
        }

        private void LaserAttack()
        {
            if (_laserCharge > 0 && !_laserGameObject.activeSelf)
            {
                _asyncProcessor.StartCoroutine(ActivateLaser());
                _asyncProcessor.StartCoroutine(RestoreLaser());
            }
        }

        private IEnumerator ActivateLaser()
        {
            _laserGameObject.SetActive(true);
            _laserCharge -= 1;
            _signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));

            yield return new WaitForSeconds(_laserActiveTime);

            _laserGameObject.SetActive(false);
        }

        private IEnumerator RestoreLaser()
        {
            yield return new WaitForSeconds(_laserRestoreTime);

            if (_laserCharge < _maxLaserCharge)
            {
                _laserCharge += 1;
                _signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));
            }
        }

        private void BulletAttack()
        {
            Bullet bullet = _bulletsPool.Spawn();
            bullet.Launch(_cachedTransform.position, _direction);
        }
    }
}
