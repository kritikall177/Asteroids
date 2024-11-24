using System.Collections;
using _Project._Code.MemoryPools;
using UnityEngine;
using Zenject;

namespace _Project._Code
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _despawnTime = 1f;
        [SerializeField] private float _bulletSpeed = 10f;
        
        private BulletsPool _bulletsPool;
        
        private bool _isDestroyed;

        [Inject]
        public void Construct(BulletsPool bulletsPool)
        {
            _bulletsPool = bulletsPool;
        }

        public void Launch(Vector2 shootPosition, Vector2 shootDirection)
        {
            _isDestroyed = false;
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
            if (!_isDestroyed && other.gameObject.CompareTag("Destructible"))
            {
                _isDestroyed = true;
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