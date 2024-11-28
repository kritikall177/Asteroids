using _Project._Code.MemoryPools;
using _Project._Code.Parameters;
using _Project._Code.System.Score;
using Zenject;

namespace _Project._Code.CollisionObjects.Saucer
{
    public class SaucerDependencies : EnemyDependencies<SpawnParams, Saucer.FlyingSaucer>
    {
        [Inject]
        public SaucerDependencies(IAddScore scoreSystem, SaucerPool memoryPool) : base(scoreSystem, memoryPool)
        {
            ScoreSystem = scoreSystem;
            MemoryPool = memoryPool;
        }
    }
}