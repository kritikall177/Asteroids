namespace _Project._Code.CollisionObjects
{
    public interface IDependencies<in TValue>
    {
        public void HandleDestroyed(TValue item);
    }
}