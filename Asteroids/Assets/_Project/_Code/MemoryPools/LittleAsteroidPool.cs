using _Project._Code.CollisionObjects;
using _Project._Code.CollisionObjects.Asteroid;
using _Project._Code.Parameters;
using UnityEngine;

namespace _Project._Code.MemoryPools
{
    public class LittleAsteroidPool : AsteroidPool
    {
        protected new float AsteroidSpeed = 13f;
        private readonly float _asteroidScaleSize = 0.5f;

        public LittleAsteroidPool()
        {
        }

        protected override void Reinitialize(SpawnParams spawnParams, Asteroid asteroid)
        {
            base.Reinitialize(spawnParams, asteroid);
            asteroid.transform.localScale = Vector3.one * _asteroidScaleSize;
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            DespawnAsteroid(asteroid);
        }
    }
}