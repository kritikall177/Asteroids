using Code.Signals;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float _littleAsteroidScaleSize = 0.5f;
        [SerializeField] private float _asteroidSpeed = 10f;
        [SerializeField] private float _littleAsteroidSpeed = 13f;
        [SerializeField] private int _fragmentsCount  = 2;
        [SerializeField] private int _scoreCount  = 40;
        
        private AsteroidPool _asteroidPool;
        private SignalBus _signalBus;

        private bool _isDestroyed;
        private bool _isLittle;

        [Inject]
        public void Construct(AsteroidPool asteroidPool, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _asteroidPool = asteroidPool;
        }

        public void Launch(Vector2 spawnPosition, bool isLittle = false)
        {
            _isDestroyed = false;
            _isLittle = isLittle;
            transform.position = spawnPosition;
            float asteroidSpeed = _asteroidSpeed;
            if (isLittle)
            {
                transform.localScale = Vector3.one * _littleAsteroidScaleSize;
                asteroidSpeed = _littleAsteroidSpeed;
                _isLittle = true;
            }
            
            _rigidbody2D.AddForce(Random.insideUnitCircle.normalized * asteroidSpeed, ForceMode2D.Impulse);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (_isDestroyed)
            {
                case false when other.gameObject.CompareTag("Projectile"):
                    _isDestroyed = true;
                    _signalBus.Fire(new AddScoreSignal(_scoreCount));
                    DestroyAsteroid();
                    break;
                case false when other.gameObject.CompareTag("Player"):
                    _isDestroyed = true;
                    _signalBus.Fire(new AddScoreSignal(_scoreCount));
                    _asteroidPool.Despawn(this);
                    break;
            }
        }

        private void DestroyAsteroid()
        {
            if (!_isLittle)
            {
                for (int i = 0; i < _fragmentsCount; i++)
                {
                    var asteroid =_asteroidPool.Spawn();
                    asteroid.Launch(transform.position, true);
                }
            }
            
            _asteroidPool.Despawn(this);
        }
        
        public void OnDespawned()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            transform.localScale = Vector3.one;
        }   
    }
}