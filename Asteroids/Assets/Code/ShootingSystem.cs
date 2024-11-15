using System;
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

        private Vector2 _direction;

        [Inject]
        public void Construct(IInputSystem inputSystem, BulletsPool bulletsPool)
        {
            _inputSystem = inputSystem;
            _inputSystem.OnAttackEvent += Attack;
            _inputSystem.OnHeavyAttackEvent += HeavyAttack;
            _inputSystem.OnLookEvent += ShootDirection;
            _bulletsPool = bulletsPool;
        }

        private void Attack(float isAttack)
        {
            if (isAttack > 0) BulletAttack();
        }

        private void HeavyAttack(float isAttack)
        {
            if (isAttack > 0) LaserAttack();
        }
        
        private void ShootDirection(Vector2 position)
        {
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

        private IEnumerator  RestoreLaser()
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