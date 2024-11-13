using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float _acceleration = 10f;
        
        private IInputSystem _inputSystem;
        
        private bool _isMoving;
        private Vector2 _lookDir;

        [Inject]
        public void Construct(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputSystem.OnMoveEvent += MoveVector;
            _inputSystem.OnLookEvent += RotationVector;
        }
        
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        private void RotationVector(Vector2 position)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(position);
            _lookDir = new Vector2(mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y).normalized;
        }

        private void MoveVector(Vector2 vector)
        {
            _isMoving = vector.y > 0;
        }

        private void HandleMovement()
        {
            if (_isMoving)
            {
                _rigidbody2D.AddForce(_lookDir * _acceleration);
            }
        }

        private void HandleRotation()
        {
            transform.up = _lookDir;
        }

        private void OnDestroy()
        {
            _inputSystem.OnMoveEvent -= MoveVector;
            _inputSystem.OnLookEvent -= RotationVector;
        }
    }
}
