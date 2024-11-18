using System.Collections;
using UnityEngine;
using Zenject;

namespace Code
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
        
        private void EnableShoot()
        {
            _canShoot = true;
        }

        // Отключаем возможность стрелять при окончании игры
        private void DisableShoot()
        {
            _canShoot = false;
            StopAllCoroutines();
            _laserCharge = 2;
        }
        
        private void Attack(float isAttack)
        {
            if (_canShoot && isAttack > 0)
            {
                BulletAttack();
            }
        }
        
        private void HeavyAttack(float isAttack)
        {
            if (_canShoot && isAttack > 0)
            {
                LaserAttack();
            }
        }
        
        private void ShootDirection(Vector2 position)
        {
            if (!_canShoot) return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(position);
            _direction = new Vector2(mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y).normalized;
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
            
            yield return new WaitForSeconds(_laserActiveTime);
            
            _laserGameObject.SetActive(false);
        }
        
        private IEnumerator RestoreLaser()
        {
            yield return new WaitForSeconds(_laserRestoreTime);

            if (_laserCharge < _maxLaserCharge) _laserCharge += 1;
        }
        
        private void BulletAttack()
        {
            Bullet bullet = _bulletsPool.Spawn();
            bullet.Launch(transform.position, _direction);
        }
    }
}
