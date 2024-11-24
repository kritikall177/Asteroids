using System;
using _Project._Code.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Project._Code.System
{
    public class InputSystem : IInputSystem, InputSystem_Actions.IPlayerActions, IInitializable
    {
        private readonly InputSystem_Actions _inputSystemActions;
        
        private SignalBus _signalBus;
        
        public event Action<bool> OnMoveEvent;
        public event Action<Vector2> OnLookEvent;
        public event Action<bool> OnAttackEvent;
        public event Action<bool> OnHeavyAttackEvent;
        
        
        [Inject]
        public InputSystem(SignalBus signalBus)
        {
            _inputSystemActions = new InputSystem_Actions();
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartSignal>(OnGameStart);
            _signalBus.Subscribe<GameOverSignal>(OnGameOver);
            _inputSystemActions.Enable();
            _inputSystemActions.Player.SetCallbacks(this);
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