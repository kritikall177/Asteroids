using UnityEngine;

namespace Code.Signals
{
    public class UpdateTransformSignal
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