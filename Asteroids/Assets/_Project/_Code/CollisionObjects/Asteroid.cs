using _Project._Code.CollisionComponents;
using _Project._Code.MemoryPools;
using _Project._Code.System.Score;
using UnityEngine;
using Zenject;

namespace _Project._Code.CollisionObjects
{
    public class Asteroid : MonoBehaviour, IDestructibleComponent, ITeleportableComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;


        [SerializeField] private int _scoreCount  = 40;

        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }

        private AsteroidPool _asteroidPool;
        private IAddScore _score;


        [Inject]
        public void Construct(IAddScore score)
        {
            _score = score;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.enabled && 
                (other.gameObject.TryGetComponent<IProjectileComponent>(out _) || 
                 other.gameObject.TryGetComponent<IPlayerComponent>(out _)))
            {
                _collider.enabled = false;
                _score.AddScore(_scoreCount);
                _asteroidPool.Despawn(this);
            }
        }

        public void SetPool(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }
    }
}