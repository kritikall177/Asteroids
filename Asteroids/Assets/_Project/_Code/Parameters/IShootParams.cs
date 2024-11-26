using UnityEngine;

namespace _Project._Code.Parameters
{
    public interface IShootParams : ISpawnParams
    {
        Vector2 ShootDirection { get; }
    }
}