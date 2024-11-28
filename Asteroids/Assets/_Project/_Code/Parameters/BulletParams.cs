using UnityEngine;

namespace _Project._Code.Parameters
{
    public struct BulletParams
    {
        public Vector2 SpawnPosition { get; }

        public Vector2 ShootDirection { get; }

        public BulletParams(Vector2 spawnPosition, Vector2 shootDirection)
        {
            SpawnPosition = spawnPosition;
            ShootDirection = shootDirection;
        }
    }
}