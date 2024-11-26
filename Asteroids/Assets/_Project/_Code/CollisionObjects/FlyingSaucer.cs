using _Project._Code.CollisionComponents;
using _Project._Code.MemoryPools;
using _Project._Code.System.Score;
using UnityEngine;
using Zenject;

namespace _Project._Code.CollisionObjects
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

        private SaucerPool _saucerPool;
        private IAddScore _score;

        [Inject]
        public void Construct(SaucerPool saucerPool, IAddScore score)
        {
            _saucerPool = saucerPool;
            _score = score;
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
                _score.AddScore(_scoreCount);
                _saucerPool.Despawn(this);
            }
        }
    }
}
