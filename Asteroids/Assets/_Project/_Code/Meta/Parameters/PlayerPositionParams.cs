using UnityEngine;

namespace _Project._Code.Meta.Parameters
{
    public struct PlayerPositionParams
    {
        public Vector3 Position;
        public float Rotation;

        public PlayerPositionParams(Vector3 position, float rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}