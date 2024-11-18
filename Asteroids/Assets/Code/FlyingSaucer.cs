using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Code
{
    public class FlyingSaucer : MonoBehaviour
    {
        [SerializeField] private AggroZoneSystem _aggroZone;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _SaucerSpeed = 10f;

        private SaucerPool _saucerPool;
        
        private Transform _targetTransform;
        private bool _isDestroyed;
        private bool _isEnemyDetect;

        [Inject]
        public void Construct(SaucerPool saucerPool)
        {
            _saucerPool = saucerPool;
        }

        public void FixedUpdate()
        {
            СhasingTarget();
        }

        private void СhasingTarget()
        {
            if (_isEnemyDetect)
            {
                _rigidbody2D.linearVelocity = (_targetTransform.position - transform.position).normalized * _SaucerSpeed;
            }
        }

        public void Launch(Vector2 spawnPosition)
        {
            transform.position = spawnPosition;
            _rigidbody2D.AddForce(Random.insideUnitCircle.normalized * _SaucerSpeed, ForceMode2D.Impulse);
        }
        
        public void SetTarget(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _isEnemyDetect = true;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!_isDestroyed && (collider.gameObject.CompareTag("Projectile") || collider.gameObject.CompareTag("Player")))
            {
                _isDestroyed = true;
                _saucerPool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
            _targetTransform = null;
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}