using Zenject;

namespace Code
{
    public class AsteroidPool : MemoryPool<Asteroid>
    {
        protected override void OnSpawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
            asteroid.OnDespawned();
        }
    }
}