using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code
{
    public class MovementSystem : MonoBehaviour
    {
        [Header("Move")]
        [SerializeField] private float _moveSpeed = 10f;

        
        private IInputSystem _inputSystem;
        
        private Vector3 _moveDir = Vector3.zero;
        private Vector3 _rotateDir;

        [Inject]
        public void Construct(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _inputSystem.OnMoveEvent += MoveVector;
            _inputSystem.OnLookEvent += RotationVector;
        }
        
        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        private void RotationVector(Vector2 position)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(position);
            _rotateDir = new Vector2(mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y);
        }

        private void MoveVector(Vector2 vector) => 
            _moveDir = transform.TransformDirection(new Vector3(0f, vector.y > 0 ? vector.y : 0f, 0f)) *
                       (_moveSpeed * Time.deltaTime);

        private void HandleMovement()
        {
            transform.position += _moveDir;
        }

        private void HandleRotation()
        {
            transform.up = _rotateDir;
        }

        private void OnDestroy()
        {
            _inputSystem.OnMoveEvent -= MoveVector;
            _inputSystem.OnLookEvent -= RotationVector;
        }
    }
}