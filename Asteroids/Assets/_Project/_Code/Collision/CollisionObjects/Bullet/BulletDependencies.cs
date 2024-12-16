using _Project._Code.MemoryPools;
using Zenject;

namespace _Project._Code.Collision.CollisionObjects.Bullet
{
    public class BulletDependencies
    {
        private BulletsPool _bulletsPool;

        [Inject]
        public BulletDependencies(BulletsPool bulletsPool)
        {
            _bulletsPool = bulletsPool;
        }

        public void HandleDestroyed(Bullet bullet)
        {
            _bulletsPool.Despawn(bullet);
        }
    }
}