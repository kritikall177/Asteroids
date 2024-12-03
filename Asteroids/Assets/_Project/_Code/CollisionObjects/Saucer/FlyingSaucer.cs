using _Project._Code.CollisionComponents;
using UnityEngine;
using Zenject;

namespace _Project._Code.CollisionObjects.Saucer
{
    public class FlyingSaucer : MonoBehaviour, IDestructibleComponent, ITeleportableComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;

        [SerializeField] private int _scoreCount = 80;

        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }

        private IDependencies<FlyingSaucer> _dependencies;

        [Inject]
        public void Construct(IDependencies<FlyingSaucer> dependencies)
        {
            _dependencies = dependencies;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }
        

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.enabled && (other.gameObject.TryGetComponent<IProjectileComponent>(out _) ||
                                      other.gameObject.TryGetComponent<IPlayerComponent>(out _)))
            {
                _collider.enabled = false;
                _dependencies.HandleDestroyed(this, _scoreCount);
            }
        }
    }
}
