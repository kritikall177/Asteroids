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
        
        private AsteroidPool _asteroidPool;

        private bool _isDestroyed = false;
        private bool _isLittle = false;

        [Inject]
        public void Construct(AsteroidPool asteroidPool)
        {
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
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!_isDestroyed && (collider.gameObject.CompareTag("Projectile") || collider.gameObject.CompareTag("Player")))
            {
                _isDestroyed = true;
                DestroyAsteroid();
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