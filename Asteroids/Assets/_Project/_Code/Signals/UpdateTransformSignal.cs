using UnityEngine;

namespace _Project._Code.Signals
{
    public struct UpdateTransformSignal
    {
        public Vector3 Position;
        public float Rotation;

        public UpdateTransformSignal(Vector3 position, float rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}