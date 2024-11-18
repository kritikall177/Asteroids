using System;
using Code.Signals;
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
        [SerializeField] private int _scoreCount  = 80;

        private SaucerPool _saucerPool;
        private SignalBus _signalBus;
        
        private Transform _targetTransform;
        private bool _isDestroyed;
        private bool _isEnemyDetect;

        [Inject]
        public void Construct(SaucerPool saucerPool, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _saucerPool = saucerPool;
        }

        public void FixedUpdate()
        {
            СhasingTarget();
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            
            if (!_isDestroyed && (other.gameObject.CompareTag("Projectile") || other.gameObject.CompareTag("Player")))
            {
                _isDestroyed = true;
                _signalBus.Fire(new AddScoreSignal(_scoreCount));
                _saucerPool.Despawn(this);
            }
        }

        private void СhasingTarget()
        {
            if (_isEnemyDetect) 
                _rigidbody2D.linearVelocity = (_targetTransform.position - transform.position).normalized * _SaucerSpeed;
        }

        public void OnDespawned()
        {
            _isEnemyDetect = false;
            _targetTransform = null;
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    }
}