using UnityEngine;

namespace _Project._Code.SpawnParameters
{
    public interface IShootParams : ISpawnParams
    {
        Vector2 ShootDirection { get; }
    }
}