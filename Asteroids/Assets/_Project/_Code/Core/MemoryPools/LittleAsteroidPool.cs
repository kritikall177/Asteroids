using _Project._Code.Core.Collision.CollisionObjects.Asteroid;
using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.Parameters;
using UnityEngine;

namespace _Project._Code.Core.MemoryPools
{
    public class LittleAsteroidPool : AsteroidPool
    {
        private ILittleAsteroidPoolConfig asteroidPoolConfig;
        
        public LittleAsteroidPool(ILittleAsteroidPoolConfig poolConfig)
        {
            asteroidPoolConfig = poolConfig;
        }

        protected override void Reinitialize(SpawnParams spawnParams, Asteroid asteroid)
        {
            asteroid.transform.localScale = Vector3.one;
            asteroid.transform.position = spawnParams.SpawnPosition;
            asteroid.Rigidbody2D.AddForce(Random.insideUnitCircle.normalized * asteroidPoolConfig.LittleAsteroidSpeed,
                ForceMode2D.Impulse);
            asteroid.transform.localScale = Vector3.one * asteroidPoolConfig.LittleAsteroidScaleSize;
        }

        protected override void OnDespawned(Asteroid asteroid)
        {
            DespawnAsteroid(asteroid);
        }
    }
}