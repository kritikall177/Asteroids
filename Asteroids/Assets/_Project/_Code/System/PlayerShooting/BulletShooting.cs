using System;
using System.Collections;
using _Project._Code.CollisionObjects;
using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using _Project._Code.System.GameState;
using _Project._Code.System.InputSystem;
using UnityEngine;
using Zenject;

namespace _Project._Code.System.PlayerShooting
{
    public class BulletShooting : IInitializable, IDisposable
    {
        private BulletsPool _bulletsPool;
        private IInputSystem _inputSystem;
        
        private Vector2 _direction;
        
        private Camera _mainCamera;
        private Transform _cachedTransform;

        [Inject]
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
                _bulletsPool.Spawn(new BulletParams(_cachedTransform.position, _direction));
            }
        }
    }
}
