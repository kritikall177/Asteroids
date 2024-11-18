using System.Collections.Generic;
using Zenject;

namespace Code
{
    public class SaucerPool : MemoryPool<FlyingSaucer>
    {
        private List<FlyingSaucer> _activeSaucers = new List<FlyingSaucer>();
        
        public void DespawnAll()
        {
            var list = new List<FlyingSaucer>(_activeSaucers);
            
            foreach (var saucer in list)
            {
                Despawn(saucer);
            }
        }
        
        protected override void OnSpawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(true);
            _activeSaucers.Add(saucer);
        }

        protected override void OnDespawned(FlyingSaucer saucer)
        {
            saucer.gameObject.SetActive(false);
            _activeSaucers.Remove(saucer);
            saucer.OnDespawned();
        }
    }
}