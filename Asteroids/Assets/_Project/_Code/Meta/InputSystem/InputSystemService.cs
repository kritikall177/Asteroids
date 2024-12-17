using System;
using _Project._Code.Core.Gameplay.GameState;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project._Code.Meta.InputSystem
{
    public class InputSystemService : IInputSystem, InputSystem_Actions.IPlayerActions, IInitializable, IDisposable
    {
        private readonly InputSystem_Actions _inputSystemActions;

        private IGameStateActionsSubscriber _gameStateActions;

        public event Action<bool> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
        public event Action<bool> OnAttackEvent;
        public event Action<bool> OnHeavyAttackEvent;

        
        public InputSystemService(IGameStateActionsSubscriber gameStateActions)
        {
            _inputSystemActions = new InputSystem_Actions();
            _gameStateActions = gameStateActions;
        }

        public void Initialize()
        {
            _gameStateActions.OnGameStart += OnGameStart;
            _gameStateActions.OnGameOver += OnGameOver;
            _inputSystemActions.Player.SetCallbacks(this);
        }

        public void Dispose()
        {
            _gameStateActions.OnGameStart -= OnGameStart;
            _gameStateActions.OnGameOver -= OnGameOver;
        }

        private void OnGameStart()
        {
            _inputSystemActions.Player.Enable();
        }

        private void OnGameOver()
        {
            _inputSystemActions.Player.Disable();
        }


        public void OnMove(InputAction.CallbackContext context) =>
            OnMoveEvent?.Invoke(context.ReadValueAsButton());

        public void OnLook(InputAction.CallbackContext context) =>
            OnLookEvent?.Invoke(context.ReadValue<Vector2>());

        public void OnAttack(InputAction.CallbackContext context) =>
            OnAttackEvent?.Invoke(context.ReadValueAsButton());

        public void OnHeavyAttack(InputAction.CallbackContext context) =>
            OnHeavyAttackEvent?.Invoke(context.ReadValueAsButton());
    }
}