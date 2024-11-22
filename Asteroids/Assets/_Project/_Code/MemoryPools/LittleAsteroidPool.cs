using Code;
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

        protected override void Reinitialize(Vector2 spawnPosition, bool isLittle, Asteroid asteroid)
        {
            asteroid.transform.localScale = Vector3.one * _asteroidScaleSize;
            base.Reinitialize(spawnPosition, isLittle, asteroid);
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            DespawnAsteroid(asteroid);
        }
    }
}