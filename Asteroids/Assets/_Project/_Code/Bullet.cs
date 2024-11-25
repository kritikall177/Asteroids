using System;
using System.Collections;
using _Project._Code.MemoryPools;
using UnityEngine;
using Zenject;

namespace _Project._Code
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;
        
        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }
        
        private BulletsPool _bulletsPool;

        [Inject]
        public void Construct(BulletsPool bulletsPool)
        {
            _bulletsPool = bulletsPool;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.enabled && other.gameObject.CompareTag("Destructible"))
            {
                _collider.enabled = false;
                _bulletsPool.Despawn(this);
            }
        }
    }
}