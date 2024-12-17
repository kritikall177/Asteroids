using _Project._Code.Collision.CollisionObjects.Asteroid;
using _Project._Code.DataConfig.Configs;
using _Project._Code.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.MemoryPools
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