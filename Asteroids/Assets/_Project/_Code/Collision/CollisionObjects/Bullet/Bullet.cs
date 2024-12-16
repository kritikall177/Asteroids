using _Project._Code.Collision.CollisionComponents;
using UnityEngine;
using Zenject;

namespace _Project._Code.Collision.CollisionObjects.Bullet
{
    public class Bullet : MonoBehaviour, IProjectileComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;

        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }

        private BulletDependencies _bulletDependencies;

        [Inject]
        public void Construct(BulletDependencies bulletDependencies)
        {
            _bulletDependencies = bulletDependencies;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.enabled && other.gameObject.TryGetComponent<IDestructibleComponent>(out _))
            {
                _collider.enabled = false;
                _bulletDependencies.HandleDestroyed(this);
            }
        }
    }
}