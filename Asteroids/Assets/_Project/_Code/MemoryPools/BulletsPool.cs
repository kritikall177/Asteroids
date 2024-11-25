using System.Collections;
using _Project._Code.CollisionObjects;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class BulletsPool : MemoryPool<Vector2, Vector2, Bullet>
    {
        private float _bulletSpeed = 20f;
        private float _despawnTime = 1f;
        
        protected override void OnSpawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        protected override void Reinitialize(Vector2 shootPosition, Vector2 shootDirection, Bullet bullet)
        {
            bullet.transform.position = shootPosition;
            bullet.Rigidbody2D.AddForce(shootDirection * _bulletSpeed, ForceMode2D.Impulse);
            bullet.StartCoroutine(DespawnTimer(bullet));
        }


        protected override void OnDespawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.Rigidbody2D.linearVelocity = Vector2.zero;
            bullet.StopCoroutine(DespawnTimer(bullet));
        }

        private IEnumerator DespawnTimer(Bullet bullet)
        {
            yield return new WaitForSeconds(_despawnTime);
            Despawn(bullet);
        }
    }
}