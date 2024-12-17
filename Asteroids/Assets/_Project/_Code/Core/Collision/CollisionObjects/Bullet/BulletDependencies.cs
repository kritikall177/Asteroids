using _Project._Code.Core.MemoryPools;

namespace _Project._Code.Core.Collision.CollisionObjects.Bullet
{
    public class BulletDependencies
    {
        private BulletsPool _bulletsPool;
        
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