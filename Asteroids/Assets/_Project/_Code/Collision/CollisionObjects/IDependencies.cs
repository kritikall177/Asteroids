namespace _Project._Code.Collision.CollisionObjects
{
    public interface IDependencies<in TValue>
    {
        public void HandleDestroyed(TValue item);
    }
}