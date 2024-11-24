using _Project._Code.Signals;
using UnityEngine;
using Zenject;

namespace _Project._Code.System
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _acceleration = 10f;

        private IInputSystem _inputSystem;
        private SignalBus _signalBus;
        
        private bool _isPlaying = true;
        private bool _isMoving;
        private Vector2 _lookDir;
        
        private Camera _mainCamera;
        private Transform _cachedTransform;

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

        private void Awake()
        {
            _mainCamera = Camera.main;
            _cachedTransform = transform;
        }

        private void OnDestroy()
        {
            _inputSystem.OnMoveEvent -= MoveVector;
            _inputSystem.OnLookEvent -= RotationVector;
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
            _signalBus.Fire(new UpdateTransformSignal(_cachedTransform.position, _cachedTransform.eulerAngles.z));
        }

        private void RotationVector(Vector2 position)
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(position);
            _lookDir = new Vector2(mousePosition.x - _cachedTransform.position.x,
                mousePosition.y - _cachedTransform.position.y).normalized;
        }

        private void MoveVector(bool isMoving)
        {
            _isMoving = isMoving;
        }

        private void HandleMovement()
        {
            if (_isMoving) 
                _rigidbody2D.AddForce(_lookDir * _acceleration);
        }

        private void HandleRotation()
        {
            _cachedTransform.up = _lookDir;
        }

        private void StartMoving()
        {
            _isPlaying = true;
        }

        private void StopMoving()
        {
            _isPlaying = false;
            _cachedTransform.position = Vector3.zero;
            _cachedTransform.eulerAngles = Vector3.zero;
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
    }
}
