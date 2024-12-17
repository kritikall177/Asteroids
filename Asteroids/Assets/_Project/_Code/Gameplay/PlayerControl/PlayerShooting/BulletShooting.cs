using System;
using _Project._Code.Collision.CollisionObjects.PlayerShip;
using _Project._Code.Core.InputSystem;
using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.Gameplay.PlayerControl.PlayerShooting
{
    public class BulletShooting : IInitializable, IDisposable, IOnBulletInvoke
    {
        public event Action OnBulletInvoke;

        private BulletsPool _bulletsPool;
        private IInputSystem _inputSystem;

        private Vector2 _direction;

        private Camera _mainCamera;
        private Transform _cachedTransform;
        
        public BulletShooting(IInputSystem inputSystem, BulletsPool bulletsPool, SpaceShip spaceShip)
        {
            _inputSystem = inputSystem;
            _bulletsPool = bulletsPool;
            _cachedTransform = spaceShip.transform;
        }

        public void Initialize()
        {
            _inputSystem.OnAttackEvent += BulletAttack;
            _inputSystem.OnLookEvent += ShootDirection;

            _mainCamera = Camera.main;
        }

        public void Dispose()
        {
            _inputSystem.OnAttackEvent -= BulletAttack;
            _inputSystem.OnLookEvent -= ShootDirection;
        }


        private void ShootDirection(Vector2 position)
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(position);
            _direction = new Vector2(mousePosition.x - _cachedTransform.position.x,
                mousePosition.y - _cachedTransform.position.y).normalized;
        }

        private void BulletAttack(bool isAttack)
        {
            if (isAttack)
            {
                OnBulletInvoke?.Invoke();
                _bulletsPool.Spawn(new BulletParams(_cachedTransform.position, _direction));
            }
        }
    }

    public interface IOnBulletInvoke
    {
        public event Action OnBulletInvoke;
    }
}