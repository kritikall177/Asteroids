using System.Collections;
using _Project._Code.Collision.CollisionObjects.Bullet;
using _Project._Code.DataConfig.Configs;
using _Project._Code.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
{
    public class BulletsPool : MemoryPool<BulletParams, Bullet>
    {
        private IBulletPoolConfig _config;

        [Inject]
        public BulletsPool(IBulletPoolConfig config)
        {
            _config = config;
        }

        protected override void OnSpawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        protected override void Reinitialize(BulletParams bulletParams, Bullet bullet)
        {
            bullet.transform.position = bulletParams.SpawnPosition;
            bullet.Rigidbody2D.AddForce(bulletParams.ShootDirection * _config.BulletSpeed, ForceMode2D.Impulse);
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
            yield return new WaitForSeconds(_config.BulletDespawnTime);
            Despawn(bullet);
        }
    }
}