using Zenject;

namespace Code
{
    public class SaucerPool : MemoryPool<FlyingSaucer>
    {
        protected override void OnSpawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(true);
        }

        protected override void OnDespawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(false);
            saucer.OnDespawned();
        }
    }
}