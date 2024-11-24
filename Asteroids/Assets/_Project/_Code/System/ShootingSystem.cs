using System.Collections;
using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using Code;
using UnityEngine;
using Zenject;

namespace _Project._Code.System
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _laserGameObject;
        [SerializeField] private int _maxLaserCharge = 2;
        [SerializeField] private int _laserCharge = 2;
        [SerializeField] private float _laserActiveTime = 0.5f;
        [SerializeField] private int _laserRestoreTime = 20;

        private BulletsPool _bulletsPool;
        private IInputSystem _inputSystem;
        private SignalBus _signalBus;

        private Vector2 _direction;
        private bool _canShoot = true;
        
        private Camera _mainCamera;
        private Transform _cachedTransform;

        [Inject]
        public void Construct(IInputSystem inputSystem, BulletsPool bulletsPool, SignalBus signalBus)
        {
            _inputSystem = inputSystem;
            _inputSystem.OnAttackEvent += Attack;
            _inputSystem.OnHeavyAttackEvent += HeavyAttack;
            _inputSystem.OnLookEvent += ShootDirection;

            _bulletsPool = bulletsPool;
            _signalBus = signalBus;

            _signalBus.Subscribe<GameStartSignal>(EnableShoot);
            _signalBus.Subscribe<GameOverSignal>(DisableShoot);
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _cachedTransform = transform;
        }

        private void OnDestroy()
        {
            _inputSystem.OnAttackEvent -= Attack;
            _inputSystem.OnHeavyAttackEvent -= HeavyAttack;
            _inputSystem.OnLookEvent -= ShootDirection;
        }

        private void EnableShoot()
        {
            _canShoot = true;
            _signalBus.Fire(new UpdateLaserCountSignal(_laserCharge));
        }

        private void DisableShoot()
        {
            _canShoot = false;
            StopAllCoroutines();
            _laserCharge = 2;
        }

        private void Attack(bool isAttack)
        {
            if (_canShoot && isAttack)
            {
                BulletAttack();
            }
        }

        private void HeavyAttack(bool isAttack)
        {
            if (_canShoot && isAttack)
            {
                LaserAttack();
            }
        }

        private void ShootDirection(Vector2 position)
        {
            if (!_canShoot) return;

            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(position);
            _direction = new Vector2(mousePosition.x - _cachedTransform.position.x,
                mousePosition.y - _cachedTransform.position.y).normalized;
        }

        private void LaserAttack()
        {
            if (_laserCharge > 0 && !_laserGameObject.activeSelf)
            {
                StartCoroutine(ActivateLaser());
                StartCoroutine(RestoreLaser());
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
