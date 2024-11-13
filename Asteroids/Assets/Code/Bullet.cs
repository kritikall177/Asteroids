using System.Collections;
using UnityEngine;
using Zenject;

namespace Code
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _despawnTime = 1f;
        
        private BulletsPool _bulletsPool;

        [Inject]
        public void Construct(BulletsPool bulletsPool)
        {
            _bulletsPool = bulletsPool;
        }

        public void Launch(Vector2 shootPosition, Vector2 shootDirection, float bulletSpeed = 1f)
        {
            transform.position = shootPosition;
            _rigidbody2D.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
            StartCoroutine(DespawnTimer());
        }

        private IEnumerator DespawnTimer()
        {
            yield return new WaitForSeconds(_despawnTime);
            _bulletsPool.Despawn(this);
        }

        public void OnDespawned()
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            StopCoroutine(DespawnTimer());
        }
        
    }
}