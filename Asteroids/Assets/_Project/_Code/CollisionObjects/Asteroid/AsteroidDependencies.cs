using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using _Project._Code.System.Score;
using Zenject;

namespace _Project._Code.CollisionObjects.Asteroid
{
    public class AsteroidDependencies : EnemyDependencies<SpawnParams, Asteroid>
    {
        [Inject]
        public AsteroidDependencies(IAddScore scoreSystem, AsteroidPool memoryPool) : base(scoreSystem, memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
        }
    }
}