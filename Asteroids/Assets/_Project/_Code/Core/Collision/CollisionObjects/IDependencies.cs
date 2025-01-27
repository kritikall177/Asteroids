using UnityEngine;

namespace _Project._Code.Core.Collision.CollisionObjects
{
    public interface IDependencies<in TValue> where TValue : MonoBehaviour
    {
        public void HandleDestroyed(TValue item);
    }
}