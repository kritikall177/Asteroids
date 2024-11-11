using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _minSpeed = 0f;
        [SerializeField] private float _accelerationRate = 1f;
        [SerializeField] private float _decelerationRate = 1f;
        
        private IInputSystem _inputSystem;
        
        private float _currentSpeed = 0f;
        private Vector3 _moveDir = Vector3.zero;
        private Vector3 _currentMoveDir = Vector3.zero;
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
            Debug.Log(_rotateDir);
        }

        private void MoveVector(Vector2 vector)
        {
            // Обновляем направление движения, если получаем ненулевое значение
            _moveDir = transform.TransformDirection(new Vector3(0f, vector.y > 0 ? 1f : 0f, 0f));
            if (_moveDir != Vector3.zero)
                _currentMoveDir = _moveDir;
        }

        private void HandleMovement()
        {
            // Ускоряем объект, если нажата кнопка движения
            if (_moveDir.y > 0)
            {
                _currentSpeed += _accelerationRate * Time.deltaTime;
            }
            else
            {
                // Плавно снижаем скорость при отпускании кнопки
                _currentSpeed -= _decelerationRate * Time.deltaTime;
            }

            // Ограничиваем текущую скорость
            _currentSpeed = Mathf.Clamp(_currentSpeed, _minSpeed, _maxSpeed);

            // Используем _currentMoveDir для продолжения движения по инерции
            transform.position += _currentMoveDir * _currentSpeed * Time.deltaTime;

            // Останавливаем объект, если его скорость близка к нулю
            if (_currentSpeed <= _minSpeed)
                _currentMoveDir = Vector3.zero;
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
