﻿using System;
using _Project._Code.Core.Collision.CollisionObjects.PlayerShip;
using _Project._Code.Core.Gameplay.GameState;
using _Project._Code.Meta.DataConfig.Configs;
using _Project._Code.Meta.InputSystem;
using _Project._Code.Meta.Parameters;
using UnityEngine;
using Zenject;

namespace _Project._Code.Core.Gameplay.PlayerControl.PlayerMovement
{
    public class PlayerMovement : IInitializable, IFixedTickable, IDisposable, IPlayerPositionChange
    {
        public event Action<PlayerPositionParams> OnPositionChange;

        private IInputSystem _inputSystem;
        private IGameStateActionsSubscriber _gameStateActions;
        private IPlayerMovementAcceleration _playerMovementAcceleration;

        private bool _isPlaying = true;
        private bool _isMoving;
        private Vector2 _lookDir;

        private Camera _mainCamera;
        private Transform _cachedTransform;
        private Rigidbody2D _cachedrigidbody2D;
        
        public PlayerMovement(IInputSystem inputSystem, IGameStateActionsSubscriber gameStateActions, SpaceShip ship,
            IPlayerMovementAcceleration playerMovementAcceleration)
        {
            _inputSystem = inputSystem;
            _gameStateActions = gameStateActions;
            _cachedTransform = ship.transform;
            _cachedrigidbody2D = ship.Rigidbody2D;
            _playerMovementAcceleration = playerMovementAcceleration;
        }

        public void Initialize()
        {
            _inputSystem.OnMoveEvent += MoveVector;
            _inputSystem.OnLookEvent += RotationVector;

            _gameStateActions.OnGameStart += StartMoving;
            _gameStateActions.OnGameOver += StopMoving;

            _mainCamera = Camera.main;
        }

        public void Dispose()
        {
            _inputSystem.OnMoveEvent -= MoveVector;
            _inputSystem.OnLookEvent -= RotationVector;

            _gameStateActions.OnGameStart -= StartMoving;
            _gameStateActions.OnGameOver -= StopMoving;
        }

        public void FixedTick()
        {
            if (!_isPlaying) return;
            HandleMovement();
            HandleRotation();
            SendData();
        }

        private void SendData()
        {
            OnPositionChange?.Invoke(
                new PlayerPositionParams(_cachedTransform.position, _cachedTransform.eulerAngles.z));
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
                _cachedrigidbody2D.AddForce(_lookDir * _playerMovementAcceleration.PlayerMovementAcceleration);
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
            _cachedrigidbody2D.linearVelocity = Vector2.zero;
            _cachedrigidbody2D.angularVelocity = 0f;
        }
    }
}