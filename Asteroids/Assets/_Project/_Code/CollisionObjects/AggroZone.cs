using _Project._Code.CollisionComponents;
using UnityEngine;

namespace _Project._Code.CollisionObjects
{
    public class AggroZone : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D _collider2D;
        [SerializeField] private FlyingSaucer _flyingSaucer;
        [SerializeField] private float _triggerRadius = 3f;
        [SerializeField] private float _saucerSpeed = 10f;
        
        private bool _isEnemyDetect;
        private Transform _targetTransform;
        private Transform _cachedTransform;

        private void Start()
        {
            _collider2D.radius = _triggerRadius;
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
                _flyingSaucer.Rigidbody2D.linearVelocity = direction * _saucerSpeed;
            }
        }
    }
}