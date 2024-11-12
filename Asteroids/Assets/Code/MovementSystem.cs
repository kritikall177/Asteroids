using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _screenWrapBuffer = 50f;
        
        private IInputSystem _inputSystem;
        
        private bool _isMoving;
        private Vector2 _lookDir;
        private float _screenWidth;
        private float _screenHeight;

        [Inject]
        public void Construct(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputSystem.OnMoveEvent += MoveVector;
            _inputSystem.OnLookEvent += RotationVector;
            _screenWidth = Screen.width / _screenWrapBuffer;
            _screenHeight = Screen.height / _screenWrapBuffer;
        }
        
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
            ScreenWarping();
        }

        private void RotationVector(Vector2 position)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(position);
            _lookDir = new Vector2(mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y);
        }

        private void MoveVector(Vector2 vector)
        {
            _isMoving = vector.y > 0;
        }

        private void HandleMovement()
        {
            if (_isMoving) _rigidbody2D.AddForce(_lookDir);
        }

        private void HandleRotation()
        {
            transform.up = _lookDir;
        }

        //потом будет глобал скрипт для всех объектов 
        private void ScreenWarping()
        {
            Vector2 newPos = transform.position;
            if (transform.position.y >= _screenHeight)
            {
                newPos.y = -_screenHeight;
            }
            if (transform.position.y <= -_screenHeight)
            {
                newPos.y = _screenHeight;
            }
            if (transform.position.x >= _screenWidth)
            {
                newPos.x = -_screenWidth;
            }
            if (transform.position.x <= -_screenWidth)
            {
                newPos.x = _screenWidth;
            }

            transform.position = newPos;
        }

        private void OnDestroy()
        {
            _inputSystem.OnMoveEvent -= MoveVector;
            _inputSystem.OnLookEvent -= RotationVector;
        }
    }
}
