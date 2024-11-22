using System;
using _Project._Code.MemoryPools;
using Code.Factories;
using Code.Signals;
using UnityEngine;
using Zenject;

namespace Code
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
        public void Construct(AsteroidPool asteroidPool, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _asteroidPool = asteroidPool;
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
    }
}