﻿using _Project._Code.Collision.CollisionComponents;
using UnityEngine;
using Zenject;

namespace _Project._Code.Collision.CollisionObjects.Asteroid
{
    public class Asteroid : MonoBehaviour, IDestructibleComponent, ITeleportableComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;

        public Rigidbody2D Rigidbody2D
        {
            get => _rigidbody2D;
            private set => _rigidbody2D = value;
        }

        private IDependencies<Asteroid> _dependencies;


        [Inject]
        public void Construct(IDependencies<Asteroid> dependencies)
        {
            _dependencies = dependencies;
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
                _dependencies.HandleDestroyed(this);
            }
        }
    }
}