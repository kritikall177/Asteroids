namespace _Project._Code.Core.Collision.CollisionObjects
{
    public interface IDependencies<in TValue>
    {
        public void HandleDestroyed(TValue item);
    }
}