using System.Collections;
using _Project._Code.MemoryPools;
using UnityEngine;
using Zenject;

namespace _Project._Code
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _despawnTime = 1f;
        [SerializeField] private float _bulletSpeed = 10f;
        
        private BulletsPool _bulletsPool;

        [Inject]
        public void Construct(BulletsPool bulletsPool)
        {
            _bulletsPool = bulletsPool;
        }

        public void Launch(Vector2 shootPosition, Vector2 shootDirection)
        {
            _collider.enabled = true;
            transform.position = shootPosition;
            _rigidbody2D.AddForce(shootDirection * _bulletSpeed, ForceMode2D.Impulse);
            StartCoroutine(DespawnTimer());
        }

        private IEnumerator DespawnTimer()
        {
            yield return new WaitForSeconds(_despawnTime);
            _bulletsPool.Despawn(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collider.enabled && other.gameObject.CompareTag("Destructible"))
            {
                _collider.enabled = false;
                _bulletsPool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            StopCoroutine(DespawnTimer());
        }
        
    }
}