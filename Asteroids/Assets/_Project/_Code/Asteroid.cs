using _Project._Code.MemoryPools;
using _Project._Code.Signals;
using UnityEngine;
using Zenject;

namespace _Project._Code
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }
        
        [SerializeField] private int _scoreCount  = 40;
        
        private AsteroidPool _asteroidPool;
        private SignalBus _signalBus;

        private bool _isDestroyed;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _isDestroyed = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isDestroyed && other.gameObject.CompareTag("Projectile"))
            {
                _isDestroyed = true;
                _signalBus.Fire(new AddScoreSignal(_scoreCount));
                _asteroidPool.Despawn(this);
            }
            else if (!_isDestroyed && other.gameObject.CompareTag("Player"))
            {
                _isDestroyed = true;
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