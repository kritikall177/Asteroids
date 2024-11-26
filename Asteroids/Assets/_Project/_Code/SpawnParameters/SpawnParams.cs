using UnityEngine;

namespace _Project._Code.SpawnParameters
{
    public struct SpawnParams : ISpawnParams
    {
        public Vector2 SpawnPosition { get; }

        public SpawnParams(Vector2 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
    }
}