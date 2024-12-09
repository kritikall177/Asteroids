using _Project._Code.CollisionComponents;
using _Project._Code.CollisionObjects.Saucer;
using _Project._Code.DataConfig.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _Project._Code.CollisionObjects
{
    public class AggroZone : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider2D;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        
        private IAggroZoneConfig _aggroZoneConfig;
        
        private bool _isEnemyDetect;
        private Transform _targetTransform;
        private Transform _cachedTransform;

        [Inject]
        public void Construct(IAggroZoneConfig aggroZoneConfig)
        {
            _aggroZoneConfig = aggroZoneConfig;
        }
        
        private void Start()
        {
            _collider2D.radius = _aggroZoneConfig.SaucerTriggerRadius;
            _cachedTransform = _flyingSaucer.transform;
        }

        private void OnDisable()
        {
            _isEnemyDetect = false;
            _targetTransform = null;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<IPlayerComponent>(out _))
            {
                _targetTransform = collider.gameObject.transform;
                _isEnemyDetect = true;
            }
        }

        public void FixedUpdate()
        {
            СhasingTarget();
        }
        
        private void СhasingTarget()
        {
            if (_isEnemyDetect)
            {
                Vector2 direction = (_targetTransform.position - _cachedTransform.position).normalized;
                _flyingSaucer.Rigidbody2D.linearVelocity = direction * _aggroZoneConfig.SaucerChasingSpeed;
            }
        }
    }
}