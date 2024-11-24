using Zenject;

namespace _Project._Code.MemoryPools
{
    public class BulletsPool : MemoryPool<Bullet>
    {
        
        protected override void OnSpawned(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Bullet bullet)
        {
            bullet.OnDespawned();
            bullet.gameObject.SetActive(false);
        }
    }
}