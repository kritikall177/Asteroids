using _Project._Code.CollisionComponents;
using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using UnityEngine;
using Zenject;

namespace _Project._Code
{
    public class Asteroid : MonoBehaviour, IDestructibleComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;


        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }

        [SerializeField] private int _scoreCount  = 40;

        private AsteroidPool _asteroidPool;
        private SignalBus _signalBus;


        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.enabled && other.gameObject.TryGetComponent<IProjectileComponent>(out _))
            {
                _collider.enabled = false;
                _signalBus.Fire(new AddScoreSignal(_scoreCount));
                _asteroidPool.Despawn(this);
            }
            else if (other.gameObject.TryGetComponent<IPlayerComponent>(out _))
            {
                _collider.enabled = false;
                _signalBus.Fire(new AddScoreSignal(_scoreCount));
                _asteroidPool.Despawn(this);
                
            }
        }

        public void SetPool(AsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }
    }
}