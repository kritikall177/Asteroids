using UnityEngine;

namespace _Project._Code.Meta.Parameters
{
    public struct SpawnParams
    {
        public Vector2 SpawnPosition { get; }

        public SpawnParams(Vector2 spawnPosition)
        {
            SpawnPosition = spawnPosition;
        }
    }
}