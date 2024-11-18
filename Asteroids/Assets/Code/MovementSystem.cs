using System;
using Code.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Code
{
    // Здесь тоже можно было обойтись обычным ITickable или перенсти часть логики в SpaceShip, но я поздновато об этом подумал
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        [SerializeField] private float _acceleration = 10f;
        
        private IInputSystem _inputSystem;
        private SignalBus _signalBus;
        
        private bool _isPlaying = true;
        private bool _isMoving;
        private Vector2 _lookDir;

        [Inject]
        public void Construct(IInputSystem inputSystem, SignalBus signalBus)
        {
            _inputSystem = inputSystem;
            _inputSystem.OnMoveEvent += MoveVector;
            _inputSystem.OnLookEvent += RotationVector;
            
            _signalBus = signalBus;
            _signalBus.Subscribe<GameStartSignal>(StartMoving);
            _signalBus.Subscribe<GameOverSignal>(StopMoving);
        }
        
        private void FixedUpdate()
        {
            if (!_isPlaying) return;
            HandleMovement();
            HandleRotation();
            SendData();
        }

        private void SendData()
        {
            _signalBus.Fire(new UpdateTransformSignal(transform.position, transform.eulerAngles.z));
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
                _rigidbody2D.AddForce(_lookDir * _acceleration);
        }

        private void HandleRotation()
        {
            transform.up = _lookDir;
        }

        private void StartMoving()
        {
            _isPlaying = true;
        }
        
        private void StopMoving()
        {
            _isPlaying = false;
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }

        private void OnDestroy()
        {
            _inputSystem.OnMoveEvent -= MoveVector;
            _inputSystem.OnLookEvent -= RotationVector;
        }
    }
}
