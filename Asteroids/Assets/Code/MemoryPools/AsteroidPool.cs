using System.Collections.Generic;
using NUnit.Framework;
using Zenject;

namespace Code
{
    public class AsteroidPool : MemoryPool<Asteroid>
    {
        private List<Asteroid> _activeAsteroids = new List<Asteroid>();

        public void DespawnAll()
        {
            var list = new List<Asteroid>(_activeAsteroids);
            
            foreach (var asteroid in list)
            {
                Despawn(asteroid);
            }
        }
        
        protected override void OnSpawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(true);
            _activeAsteroids.Add(asteroid);
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            asteroid.gameObject.SetActive(false);
            _activeAsteroids.Remove(asteroid);
            asteroid.OnDespawned();
        }
    }
}